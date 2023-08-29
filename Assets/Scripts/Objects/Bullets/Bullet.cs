using UnityEngine;

public enum BulletType { Regular, Builder, Mechanical };


public abstract class Bullet : ScriptableObject {
    [SerializeField, Range(0, 5)] private float speed;
    [SerializeField, Range(0, 3)] private int damage;
    [SerializeField, Range(0, 5)] private float recoil;
    [SerializeField, Range(0, 3)] private int cost;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private GameObject explosionEffect;
    private GameObject target;

    public float getSpeed() {
        return speed;
    }

    public int getDamage() {
        return damage;
    }

    public float getRecoil() {
        return recoil;
    }

    public int getCost() {
        return cost;
    }

    public BulletType getBulletType() {
        return bulletType;
    }

    public GameObject getExplosionEffect() {
        return explosionEffect;
    }

    public abstract GameObject getPrefab();


    protected void DamageGround(GameObject obj) {
        //GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        //obj.GetComponent<Ground>().TakeDamage(damage);
    }

    protected void DamagePlayer(GameObject obj) {
        //GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        //obj.GetComponent<Player>().TakeDamage(damage);
    }
}
