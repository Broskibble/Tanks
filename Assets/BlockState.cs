using UnityEngine;

public class BlockState : MonoBehaviour
{
    private int health = 2;
    private GameZone gameZone;
    private bool isFalling = false;

    private void Awake() {
        GameManager gm = GameManager.instance;
        gameZone = gm.GetGameZone();
    }

    private void Update() {
        if (isFalling) {
            
        }
    }

    public void DamageBlock(int damage) {
        if (health > 0) {
            health -= damage;
            if (health <= 0) {
                DestroyBlock();
            }
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.white, (float)health / 100);
        }
        
    }

    private void DestroyBlock() {
        Destroy(gameObject);
        if (gameZone.GetUpper(gameObject) != null) {
            gameZone.GetUpper(gameObject).GetComponent<BlockState>().BeginFalling();
        }
    }

    private void BeginFalling() {
        isFalling = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0));
    }
}
