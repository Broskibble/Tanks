using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string Size = "15x10";
    private List<Player> players;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        players = new List<Player>();
        foreach (Player player in FindObjectsOfType<Player>()) {
            players.Add(player);
        }
    }

    public void AddPlayer(Player player) {
        players.Add(player);
    }

    public void RemovePlayer(Player player) {
        players.Remove(player);
    }

    public List<Player> GetPlayers() {
        return players;
    }

    public void SetSize(string size) {
        Size = size;
    }
}
