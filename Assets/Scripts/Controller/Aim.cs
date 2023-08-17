using UnityEngine;

public class Aim : MonoBehaviour {
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject target;
    [SerializeField] GameObject fire_point;
    [SerializeField] Player player;
    private Transform[] points;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    private bool specialTrajectory = false;

    void Start() {
        mainCamera = Camera.main;
        // Hide();
        points = new Transform[50];
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
        UpdateTrajectory(fire_point.transform.position, hit.transform.position);
        // for (int i = 0; i < points.Length - 1; i++) {
        //     if (Physics.Raycast(points[i], points[i + 1] - points[i], out RaycastHit hit2, Vector3.Magnitude(points[i + 1] - points[i]))) {
        //         target.transform.position = hit2.point;
        //         target.transform.rotation = Quaternion.LookRotation(hit2.normal);
        //         break;
        //     }
        // }
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
        for (int i = 1; i < points.Length; i++) {
            points[i].position = Vector3.MoveTowards(points[i - 1].position, endPoint, speed * Time.deltaTime * i);
            //points[i] = Vector3.Lerp(startPoint, endPoint, Vector3.Magnitude(startPoint - endPoint) * speed * i / Vector3.Magnitude(startPoint - endPoint));
        }
    }

    void OnDrawGizmos() {
        if (points != null) {
            for (int i = 0; i < points.Length - 1; i++) {
                Gizmos.DrawSphere(points[i].position, 0.1f);
            }
        }
    }

}
