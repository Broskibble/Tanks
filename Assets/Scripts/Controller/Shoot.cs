using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Aim aim;
    
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (player.canShootBulletOne()) {
                player.shootBulletOne();
                Fire(player.getBulletOne());
            }
        }
        
        if (Input.GetButtonDown("Fire2")) {
            if (player.canShootBulletTwo()) {
                player.shootBulletTwo();
            }
        }
    }

    void Fire(Bullet bullet) {
        Quaternion faceTarget = Quaternion.LookRotation(aim.getTarget().transform.position - aim.getFirePoint().transform.position);
        GameObject bulletInstance = Instantiate(bullet.getPrefab(), aim.getFirePoint().transform.position, faceTarget);
        bulletInstance.GetComponent<Rigidbody>().velocity = transform.forward * bullet.getSpeed() * 8;
    }
}
