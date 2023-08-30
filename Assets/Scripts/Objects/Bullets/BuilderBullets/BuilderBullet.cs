using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuilderBullet", order = 2)]
public class BuilderBullet : Bullet {
    public GameObject blockPrefab;
    public int[] blockPattern;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            
        }
    }

    public override GameObject GetPrefab() {
        return blockPrefab;
    }
}
