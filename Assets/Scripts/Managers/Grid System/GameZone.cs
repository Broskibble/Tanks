using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameZone : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    private List<List<List<GameObject>>> cubemap;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ScriptableObject cube;
    private Vector3 mapSize;
    [Range(0, 1)] public float outlinePercent;
    Plane plane = new Plane(Vector3.up, Vector3.zero);
    private GameObject blockPrefab;
    public bool autoUpdate = false;
    private GameObject origin;
    private GameObject deathZone;

    private void Awake() {
        cubemap = new List<List<List<GameObject>>>();
        mainCamera = Camera.main;
        origin = transform.Find("Origin").gameObject;
        deathZone = transform.Find("Death Zone").gameObject;

    }
    private void Start() {
        blockPrefab = dataManager.getCube();
        
        GenerateMap();
    }

    public void GenerateMap() {
        var sizes = GameManager.instance.Size.Split('x');
        mapSize = new Vector3(int.Parse(sizes[0]), 5, int.Parse(sizes[1]));

        origin.transform.position = new Vector3(-mapSize.x / 2 + 0.5f, 0.5f, -mapSize.y + 0.5f);
        deathZone.GetComponent<BoxCollider>().size = new Vector3(mapSize.x, 1, mapSize.z);
        deathZone.transform.position = new Vector3(0, -0.5f, 0);
        
        
        // Clear existing map
        ClearMap();


        // Create the cubemap
        for (int layer = 0; layer < mapSize.y; layer++) {
            cubemap.Add(new List<List<GameObject>>());
            GameObject gridLayer = new GameObject(string.Format("Layer {0}", layer));
            gridLayer.transform.SetParent(origin.transform, false);
            for (int column = 0; column < mapSize.x; column++) {
                cubemap[layer].Add(new List<GameObject>());
                for (int row = 0; row < mapSize.z; row++) {
                    GameObject cube = Instantiate(blockPrefab, new Vector3(column, layer, row), Quaternion.identity);
                    cubemap[layer][column].Add(cube);
                    cube.transform.SetParent(gridLayer.transform, false);
                    cube.name = string.Format("Cube {0} {1} {2}", layer, column, row);
                    cube.transform.localScale = Vector3.one * (1 - outlinePercent);
                    if (!(layer == 0 || layer == 1 || (layer == 2 && column == 0) || (layer == 3 && column == 0 && row == 5))) {
                        cube.SetActive(false);
                    }
                }
            }
        }

        

        // move players to top of the map
        foreach (Player player in GameManager.instance.GetPlayers()) {
            player.transform.position = new Vector3(0, mapSize.y + 1, 0);
        }
    }

    private void ClearMap() {
        // Clear existing cubemap and layer GameObjects
        foreach (var layerList in cubemap) {
            foreach (var columnList in layerList) {
                foreach (var cube in columnList) {
                    Destroy(cube);
                }
                columnList.Clear();
            }
        }
        cubemap.Clear();

        // Destroy the existing layer GameObjects
        foreach (Transform child in transform) {
            if (child.name.Contains("Layer")) {
                Destroy(child.gameObject);
            }
        }
    }

    private void FixedUpdate() {

        //TODO: rotate gamezone
        // RaycastHit hit;
        // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // if (Physics.Raycast(ray, out hit, 100.0f, 7)) {
        //     //rotate gamezone
            
        // }
        // else {
        //     // rotate gamezone back to origin

        // }
        
    }

    private void OnDrawGizmos() {
        if (cubemap != null) {
            foreach (var layer in cubemap) {
                foreach (var column in layer) {
                    foreach (var cube in column) {
                        if (cube.activeInHierarchy == true) {
                            Gizmos.color = Color.green;
                        }
                        else {
                            Gizmos.color = Color.red;
                        }
                        Gizmos.DrawWireCube(cube.transform.position, Vector3.one);
                    }
                }
            }
        }
    }

    public GameObject GetLower(GameObject cube) {
        int[] coords = GetCoords(cube);
        if (coords[0] == 0) {
            return null;
        }
        return cubemap[coords[0] - 1][coords[1]][coords[2]];
    }

    public GameObject GetUpper(GameObject cube) {
        int[] coords = GetCoords(cube);
        if (coords[0] == mapSize.y - 1) {
            return null;
        }
        return cubemap[coords[0] + 1][coords[1]][coords[2]];
    }

    public int[] GetCoords(GameObject cube) {
        int[] coords = new int[3];
        string[] name = cube.name.Split(' ');
        coords[0] = int.Parse(name[1]);
        coords[1] = int.Parse(name[2]);
        coords[2] = int.Parse(name[3]);
        return coords;
    }

    public int GetLayer(Transform transform) {
        return (int) math.floor(transform.position.y);
    }
}