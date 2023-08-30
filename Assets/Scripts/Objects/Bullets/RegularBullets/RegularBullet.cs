using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RegularBullet", order = 1)]
public class RegularBullet : Bullet {
    [SerializeField] private GameObject bulletPrefab;

    public override GameObject GetPrefab() {
        return bulletPrefab;
    }
    
    
}