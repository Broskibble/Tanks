using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    enum TurretState { Horizontal, Up, Down };
    private TurretState turretState = TurretState.Horizontal;

    void Start() {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            Vector3 fixed_position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 turretDirection = fixed_position - transform.position;
            float desiredAngle = Mathf.Atan2(turretDirection.x, turretDirection.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, desiredAngle, transform.eulerAngles.z);
            Debug.Log("turretDirection: " + turretDirection);
        }

        // if space bar is held, rotate turret upwards
        if (Input.GetKey(KeyCode.Space)) {
            transform.eulerAngles = new Vector3(45, transform.eulerAngles.y, transform.eulerAngles.z);
            turretState = TurretState.Up;
        }

        // if shift is held, rotate turret downwards
        if (Input.GetKey(KeyCode.LeftShift)) {
            transform.eulerAngles = new Vector3(-45, transform.eulerAngles.y, transform.eulerAngles.z);
            turretState = TurretState.Down;
        }

        // if neither space nor shift is held, rotate turret to horizontal
        if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)) {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            turretState = TurretState.Horizontal;
        }
    }

}
