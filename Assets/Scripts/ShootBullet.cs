using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {
    [SerializeField] private PlayerState playerState;
    
    void Update() {
        if (Input.GetButton("Fire1")) {
            if (playerState.canShootBulletOne()) {
                playerState.shootBulletOne();
                Fire(playerState.getBulletOne());
            }
        }
        
        if (Input.GetButton("Fire2")) {
            if (playerState.canShootBulletTwo()) {
                playerState.shootBulletTwo();
                Fire(playerState.getBulletTwo());
            }
        }
    }

    void Fire(GameObject bullet) {
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
    }
}
