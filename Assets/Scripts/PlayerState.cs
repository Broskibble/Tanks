using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    private int health = 100;
    private Bullet bulletOne;
    private Bullet bulletTwo;
    private float ammo;

    void Start() {
        bulletOne = Null;
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
