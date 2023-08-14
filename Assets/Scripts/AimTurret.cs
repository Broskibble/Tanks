using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AimTurret : MonoBehaviour {
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject target;
    [SerializeField] GameObject fire_point;
    [SerializeField] GameObject player;
    PlayerState playerState;
    private Vector3[] points;
    private bool isSpecialBullet = false;

    void Start() {
        mainCamera = Camera.main;
        playerState = player.GetComponent<PlayerState>();
    }

    void Update() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(1)) {
            isSpecialBullet = true;
        }
        else {
            isSpecialBullet = false;
        }
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            //Debug.Log(hit.point);
            UpdateTarget(hit);
            Vector3 fixed_position = new(hit.point.x, transform.position.y, hit.point.z);
            Vector3 turretDirection = fixed_position - transform.position;
            float desiredAngle = Mathf.Atan2(turretDirection.x, turretDirection.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, desiredAngle, transform.eulerAngles.z);
        }
    }

    private void UpdateTarget(RaycastHit hit) {
        target.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
        points = BulletTrajectory(100, fire_point.transform.position, target.transform.position);  
    }

    public Vector3[] BulletTrajectory(int iterations, Vector3 startPoint, Vector3 endPoint) {
        Vector3[] points = new Vector3[iterations];
        
        points[0] = startPoint;
        if (isSpecialBullet) {
            
        }
        float length = 0.1f;
        for (int i = 1; i < iterations; i++) {
            points[i] = Vector3.Lerp(startPoint, endPoint, Vector3.Magnitude(startPoint - endPoint) * length * i / Vector3.Magnitude(startPoint - endPoint));
        }
        return points;
    }

    public static bool BulletRaycast(Vector3 startPoint, Vector3 velocity, out RaycastHit hit, int iterations = 100, float timeStep = 0.01f) {
        for (int i = 1; i < iterations; i++) {
            Debug.DrawRay(startPoint + velocity * timeStep * i, velocity, Color.red, 0.1f);
            Vector3 point = startPoint + velocity * timeStep * i;
            if (Physics.Raycast(point, velocity, out hit, velocity.magnitude * timeStep)) {
                return true;
            }
            velocity += Physics.gravity * timeStep;
        }
        hit = new RaycastHit();
        return false;
    }

    void OnDrawGizmos() {
        if (points != null) {
            for (int i = 0; i < points.Length - 1; i++) {
                Gizmos.DrawSphere(points[i], 0.1f);
            }
        }
    }

}
