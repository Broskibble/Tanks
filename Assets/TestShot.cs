using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShot : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 1;
    [SerializeField] private float recoil = 1;
    [SerializeField] private int cost = 1;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.CompareTag("Block")) {
            collision.gameObject.GetComponent<BlockState>().DamageBlock(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player")) {
            //DamagePlayer(collision.gameObject);
        }


    }

    public void SetUp(int damage, float speed, float recoil, int cost, GameObject explosionEffect, GameObject bulletPrefab, GameObject target) {
        this.damage = damage;
        this.speed = speed;
        this.recoil = recoil;
        this.cost = cost;
        this.explosionEffect = explosionEffect;
        this.bulletPrefab = bulletPrefab;
        this.target = target;
    }
}
