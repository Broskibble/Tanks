using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    [SerializeField] private RegularBullet shot;
    [SerializeField] private RegularBullet bigShot;
    [SerializeField] private RegularBullet drill;
    [SerializeField] private RegularBullet driver;
    [SerializeField] private BuilderBullet block;
    [SerializeField] private BuilderBullet wall;
    [SerializeField] private BuilderBullet tower;
    [SerializeField] private BuilderBullet hill;
    [SerializeField] private GameObject cube;

    public Bullet getShot() {
        return shot;
    }

    public Bullet getBigShot() {
        return bigShot;
    }

    public Bullet getDrill() {
        return drill;
    }

    public Bullet getDriver() {
        return driver;
    }

    public Bullet getBlock() {
        return block;
    }   

    public Bullet getWall() {
        return wall;
    }

    public Bullet getTower() {
        return tower;
    }

    public Bullet getHill() {
        return hill;
    }

    public Bullet getRegularBullet() {
        return shot;
    }

    public Bullet getBuilderBullet() {
        return block;
    }

    public Bullet getBullet(String bulletName) {
        switch (bulletName) {
            case "shot":
                return shot;
            case "bigShot":
                return bigShot;
            case "drill":
                return drill;
            case "driver":
                return driver;
            case "block":
                return block;
            case "wall":
                return wall;
            case "tower":
                return tower;
            case "hill":
                return hill;
            default:
                return null;
        }
    }

    public GameObject getCube() {
        return cube;
    }

}
