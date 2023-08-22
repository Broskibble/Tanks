using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameZone : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    private List<List<List<GameObject>>> cubemap;

    private void Awake() {
        cubemap = new List<List<List<GameObject>>>();
    }
    private void Start() {
        GameObject blockPrefab = dataManager.getCube();
        var sizes = GameManager.instance.Size.Split('x');
        for (int layer = 0; layer < 5; layer++) {
            cubemap.Add(new List<List<GameObject>>());
            GameObject gridLayer = new(string.Format("Layer {0}", layer));
            gridLayer.transform.parent = gameObject.transform;
            for (int column = 0; column < int.Parse(sizes[0]); column++) {
                cubemap[layer].Add(new List<GameObject>());
                for (int row = 0; row < int.Parse(sizes[1]); row++) {
                    cubemap[layer][column].Add(Instantiate(blockPrefab, new Vector3(column, layer, row), Quaternion.identity, gridLayer.transform));
                    if (!(layer == 0 || layer == 1)) {
                        cubemap[layer][column][row].SetActive(false);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos() {
        if (cubemap != null) {
            foreach (var layer in cubemap) {
                foreach (var column in layer) {
                    foreach (var cube in column) {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireCube(cube.transform.position, Vector3.one);
                    }
                }
            }
        }
    }
}
