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
    
    void Start() {
        bulletOne = dataManager.getShot();
        bulletTwo = null;
        ammo = 100;
    }

    public bool canShootBulletOne() {
        return true;
    }

    public bool canShootBulletTwo() {
        return true;
    }

    public void shootBulletOne() {
        Debug.Log("shoot bullet one");
    }

    public void shootBulletTwo() {
        Debug.Log("shoot bullet two");
    }

    public Bullet getBulletOne() {
        return bulletOne;
    }

    public Bullet getBulletTwo() {
        return bulletTwo;
    }
}
