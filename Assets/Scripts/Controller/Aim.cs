using System.Linq.Expressions;
using UnityEngine;

public class Aim : MonoBehaviour {
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject target;
    [SerializeField] GameObject fire_point;
    [SerializeField] Player player;
    [SerializeField] GameZone gameZone;
    private Transform[] points;
    [SerializeField] GameObject dotPrefab;
    private GameObject dotsParent;
    private bool specialTrajectory = false;

    private void Awake() {
        gameZone = FindObjectOfType<GameZone>();
    }

    void Start() {
        mainCamera = Camera.main;
        // Hide();
        points = new Transform[50];
        GameObject dotsParent = new GameObject("Dots");
        for (int i = 0; i < points.Length; i++) {
            points[i] = Instantiate(dotPrefab, dotsParent.transform).transform;
        }
    }

    public void Show() {
        dotsParent.SetActive(true);
    }

    public void Hide() {
        dotsParent.SetActive(false);
    }

    public GameObject getFirePoint() {
        return fire_point;
    }

    public GameObject getTarget() {
        return target;
    }

    void Update() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(1)) {
            specialTrajectory = true;
        }
        else {
            specialTrajectory = false;
        }
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, layerMask)) {
            GameObject block = hit.collider.gameObject;
            while (gameZone.GetLayer(block.transform) > gameZone.GetLayer(player.transform)) {
                block = gameZone.GetLower(block);
                if (block == null) {
                    break;
                }
            }
            Vector3 objectPosition = block.transform.position + hit.normal * 0.51f;
            UpdateTarget(objectPosition, hit);
            Vector3 fixed_position = new(objectPosition.x, transform.position.y, objectPosition.z);
            Vector3 turretDirection = fixed_position - transform.position;
            float desiredAngle = Mathf.Atan2(turretDirection.x, turretDirection.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, desiredAngle, transform.eulerAngles.z);
        }
    }

    private void UpdateTarget(Vector3 objectPosition, RaycastHit hit) {
        UpdateTrajectory(fire_point.transform.position, objectPosition);
    }

    public void UpdateTrajectory(Vector3 startPoint, Vector3 endPoint) {
        float speed;
        if (specialTrajectory && player.getBulletTwo() != null) {
            speed = player.getBulletTwo().GetSpeed();
        }
        else {
            speed = player.getBulletOne().GetSpeed();
        }

        if (Physics.Raycast(startPoint, endPoint - startPoint, out RaycastHit hit, 100.0f, LayerMask.GetMask("Ground")))
        {
            endPoint = hit.collider.gameObject.transform.position + hit.normal * 0.51f;
        }
        points[0].position = startPoint;
        bool inactivateRest = false;
        for (int i = 1; i < points.Length; i++) {
            if (inactivateRest) {
                points[i].gameObject.SetActive(false);
                continue;
            }

            points[i].gameObject.SetActive(true);
            points[i].position = Vector3.MoveTowards(points[i - 1].position, endPoint, speed);
            if (points[i].position == endPoint) {
                inactivateRest = true;
            }
        }
        if (!inactivateRest) { // if end point is not reached, set target to last point
            target.transform.position = points[^1].position;
        }
        else {
            //Debug.Log(hit.normal + " for target");
            target.transform.position = endPoint;
            target.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }
    }

    // void OnDrawGizmos() {
    //     if (points != null) {
    //         for (int i = 0; i < points.Length - 1; i++) {
    //             Gizmos.DrawSphere(points[i].position, 0.1f);
    //         }
    //     }
    // }

}
