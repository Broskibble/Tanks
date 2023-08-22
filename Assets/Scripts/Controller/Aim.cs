using UnityEngine;

public class Aim : MonoBehaviour {
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject target;
    [SerializeField] GameObject fire_point;
    [SerializeField] Player player;
    private Transform[] points;
    [SerializeField] GameObject dotPrefab;
    private GameObject dotsParent;
    private bool specialTrajectory = false;

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
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            UpdateTarget(hit);
            Vector3 fixed_position = new(hit.point.x, transform.position.y, hit.point.z);
            Vector3 turretDirection = fixed_position - transform.position;
            float desiredAngle = Mathf.Atan2(turretDirection.x, turretDirection.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, desiredAngle, transform.eulerAngles.z);
        }
    }

    private void UpdateTarget(RaycastHit hit) {
        target.transform.position = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
        target.transform.rotation = Quaternion.LookRotation(-hit.normal);
        UpdateTrajectory(fire_point.transform.position, hit.point);
    }

    public void UpdateTrajectory(Vector3 startPoint, Vector3 endPoint) {
        float speed;
        if (specialTrajectory && player.getBulletTwo() != null) {
            speed = player.getBulletTwo().getSpeed();
        }
        else {
            speed = player.getBulletOne().getSpeed();
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
            else if (Physics.Raycast(points[i - 1].position, points[i].position - points[i - 1].position, out RaycastHit hit, Vector3.Magnitude(points[i].position - points[i - 1].position))) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Bullets")) {
                    continue;
                }
                target.transform.position = hit.point + hit.normal * 0.01f;
                target.transform.rotation = Quaternion.LookRotation(-hit.normal);
                inactivateRest = true;
            }
        }
        if (!inactivateRest) { // if end point is not reached, set target to last point
            target.transform.position = points[^1].position;
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
