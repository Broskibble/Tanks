using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [SerializeField] private Player player;
    
    void Update() {
        if (Input.GetButton("Fire1")) {
            if (player.canShootBulletOne()) {
                player.shootBulletOne();
                // Fire(playerState.getBulletOne());
            }
        }
        
        if (Input.GetButton("Fire2")) {
            if (player.canShootBulletTwo()) {
                player.shootBulletTwo();
                // Fire(playerState.getBulletTwo());
            }
        }
    }

    void Fire(GameObject bullet) {
        GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.up * 10;
    }
}
