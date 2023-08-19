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
    [SerializeField] private float reloadSpeed = 0.5f;

    void Start() {
        bulletOne = dataManager.getShot();
        bulletTwo = dataManager.getBigShot();
        ammo = 3;
    }
    
    void Update() {
        //continuously increase ammo over time, clamped at 3
        ammo = Mathf.Clamp(ammo + Time.deltaTime * reloadSpeed, 0, 3);
        Debug.Log(ammo);
    }

    public bool canShootBulletOne() {
        if (ammo >= bulletOne.getCost()) {
            return true;
        }
        return false;
    }

    public bool canShootBulletTwo() {
        if (ammo >= bulletTwo?.getCost()) {
            return true;
        }
        return false;
    }

    public void shootBulletOne() {
        Debug.Log("shoot bullet one");
        ammo -= bulletOne.getCost();
    }

    public void shootBulletTwo() {
        Debug.Log("shoot bullet two");
        ammo -= bulletTwo?.getCost() ?? 0;
    }

    public Bullet getBulletOne() {
        return bulletOne;
    }

    public Bullet getBulletTwo() {
        return bulletTwo;
    }
}
