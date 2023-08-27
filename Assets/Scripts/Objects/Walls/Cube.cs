using UnityEngine;

public class Cube : ScriptableObject
{
    // thought -- blocks with different weights/heaths?
    private bool isOccupied { get; set; }
    private bool isBlock { get; set; }
    private bool blockIsFalling { get; set; }

    [SerializeField] private GameObject block;

}
