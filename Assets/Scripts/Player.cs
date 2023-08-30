using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

#nullable enable
public class Player : MonoBehaviour {
    private int health = 3;
    private Bullet bulletOne;
    private Bullet? bulletTwo;
    private float ammo;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private Aim aim;
    [SerializeField] private float reloadSpeed = 0.5f;
    [SerializeField] private Rigidbody rb;

    void Start() {
        rb = GetComponentInChildren<Rigidbody>();
        bulletOne = dataManager.getShot();
        bulletTwo = dataManager.getBigShot();
        ammo = 3;
    }
    
    void Update() {
        //continuously increase ammo over time, clamped at 3
        ammo = Mathf.Clamp(ammo + Time.deltaTime * reloadSpeed, 0, 3);
        //Debug.Log(ammo);
    }

    public bool canShootBulletOne() {
        if (ammo >= bulletOne.GetCost()) {
            return true;
        }
        return false;
    }

    public bool canShootBulletTwo() {
        if (ammo >= bulletTwo?.GetCost()) {
            return true;
        }
        return false;
    }

    public void shootBulletOne() {
        Debug.Log("shoot bullet one");
        ammo -= bulletOne.GetCost();
        // FIXME: force doesn't work right
        // Vector3 force = (transform.position - aim.getTarget().transform.position) * 50;
        // force.y = Mathf.Clamp(force.y, 0, 100);
        // rb.AddForce(force);
    }

    public void shootBulletTwo() {
        Debug.Log("shoot bullet two");
        ammo -= bulletTwo?.GetCost() ?? 0;
    }

    public Bullet getBulletOne() {
        return bulletOne;
    }

    public Bullet getBulletTwo() {
        return bulletTwo != null ? bulletTwo : bulletOne;
    }
}
