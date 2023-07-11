using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileDebugger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Grid grid;
    [SerializeField] private Text tileDebugMessage;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = (mainCamera.ScreenToWorldPoint(Input.mousePosition));
        var noZ = new Vector3(mousePos.x, mousePos.y);
        Vector3Int currentCell = grid.WorldToCell(noZ);
        tileDebugMessage.text = "Mouse at cell: " + currentCell.x + " " + currentCell.y + "\n" +
                                "World Position: " + mousePos.x + " " + mousePos.y;
    }
}
