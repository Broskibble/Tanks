using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject fire_point;

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.direction);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            Debug.Log(hit.point);
            UpdateTarget(hit);
            Vector3 fixed_position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 turretDirection = fixed_position - transform.position;
            float desiredAngle = Mathf.Atan2(turretDirection.x, turretDirection.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, desiredAngle + 90f, transform.eulerAngles.z);
        }

    }

    private void UpdateTarget(RaycastHit hit) {
        target.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
        BulletTrajectory(100, fire_point.transform.position, target.transform.position, 0.01f);
    }

    public static Vector3[] BulletTrajectory(int iterations, Vector3 startPoint, Vector3 endPoint, float timeStep) {
        Vector3[] points = new Vector3[iterations];
        points[0] = startPoint;
        Vector3 velocity = (endPoint - startPoint) / timeStep;
        for (int i = 1; i < iterations; i++) {
            points[i] = points[i - 1] + velocity * timeStep;
            velocity += Physics.gravity * timeStep;
            Debug.DrawRay(points[i], velocity, Color.red, 0.1f);
            if (points[i].y < endPoint.y) {
                break;
            }
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

    private Vector3[] BulletTrajectory(Vector3 startPoint, Vector3 endPoint, Vector3 velocity, int iterations = 100) {
        // Vector3 point = startPoint + velocity * timeStep;
        Vector3[] points = new Vector3[100];
        points[0] = startPoint;
        for (int i = 1; i < iterations; i++) {
            // point += velocity * timeStep;
            // points[i] = point;
            // Ray ray = new Ray(points[i-1], )
            // if (point.y < endPoint.y) {
            //     break;
            // }
        }
        return null;
    }

}
