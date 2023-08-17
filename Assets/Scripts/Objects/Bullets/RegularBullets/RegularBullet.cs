using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RegularBullet", order = 1)]
public class RegularBullet : Bullet {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            DamageGround(collision.gameObject);
            //Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player") {
            DamagePlayer(collision.gameObject);
        }


    }
    
    
    
}