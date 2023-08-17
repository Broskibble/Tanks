using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MechanicalBullet", order = 3)]
public class MechanicalBullet : Bullet {
    

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            
        }
    }
}
