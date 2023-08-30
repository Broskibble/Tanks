using UnityEngine;

public enum BulletType { Regular, Builder, Mechanical };


public abstract class Bullet : ScriptableObject {
    [SerializeField, Range(0, 5)] private float speed;
    [SerializeField, Range(0, 3)] protected int damage;
    [SerializeField, Range(0, 5)] private float recoil;
    [SerializeField, Range(0, 3)] private int cost;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private GameObject explosionEffect;
    private GameObject target;

    public float GetSpeed() {
        return speed;
    }

    public int GetDamage() {
        return damage;
    }

    public float GetRecoil() {
        return recoil;
    }

    public int GetCost() {
        return cost;
    }

    public BulletType GetBulletType() {
        return bulletType;
    }

    public GameObject GetExplosionEffect() {
        return explosionEffect;
    }

    public abstract GameObject GetPrefab();

}
