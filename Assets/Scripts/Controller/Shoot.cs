using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Aim aim;
    
    void Update() {
        if (Input.GetButtonUp("Fire1")) {
            if (player.canShootBulletOne()) {
                player.shootBulletOne();
                Fire(player.getBulletOne());
            }
        }
        
        if (Input.GetButtonUp("Fire2")) {
            if (player.canShootBulletTwo()) {
                player.shootBulletTwo();
            }
        }
    }

    void Fire(Bullet bullet) {
        Quaternion faceTarget = Quaternion.LookRotation(aim.getTarget().transform.position - aim.getFirePoint().transform.position);
        GameObject bulletInstance = Instantiate(bullet.GetPrefab(), aim.getFirePoint().transform.position, faceTarget);
        bulletInstance.GetComponent<TestShot>().SetUp(bullet.GetDamage(), bullet.GetSpeed(), bullet.GetRecoil(), bullet.GetCost(), bullet.GetExplosionEffect(), bullet.GetPrefab(), aim.getTarget());
        bulletInstance.GetComponent<Rigidbody>().velocity = transform.forward * bullet.GetSpeed() * 8;
    }
}
