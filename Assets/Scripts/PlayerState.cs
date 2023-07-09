using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

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

    public GameObject getBulletOne() {
        return null;
    }

    public GameObject getBulletTwo() {
        return null;
    }
}
