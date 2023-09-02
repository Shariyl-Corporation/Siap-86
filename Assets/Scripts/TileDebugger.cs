using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.InputSystem;


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
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var noZ = new Vector3(mousePos.x, mousePos.y);
        Vector3Int currentCell = grid.WorldToCell(noZ);
        
        string debugText = "Mouse at cell: " + currentCell.x + " " + currentCell.y + "\n" +
                            "World Position: " + mousePos.x + " " + mousePos.y + "\n";
                                
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) {
            GameObject gohit = hit.collider.gameObject;
            debugText += gohit.name + "\n";
            if (gohit.GetComponent<Car>() != null) {
                Car car = gohit.GetComponent<Car>();
                debugText += "IsMundur: " + car.isMundur + "\n";
                debugText += "IsTilang: " + car.isTilang + "\n";
                debugText += "IsWantToStraight: " + car.isWantToStraight + "\n";
                debugText += "Age: " + car.driver.Age + "\n";
                debugText += "HasKTP: " + car.driver.hasKTP + "\n";
                debugText += "HasSIM: " + car.driver.hasSIM + "\n";
                debugText += "HasSTNK: " + car.driver.hasSTNK + "\n";
                debugText += "HasDoneSpeedLimit: " + car.driver.hasDoneSpeedLimit + "\n";
                debugText += "HasDoneTerobosLampuMerah: " + car.driver.hasDoneTerobosLampuMerah + "\n";
                debugText += "IsDrunk: " + car.driver.isDrunk + "\n";
            }
        }

        tileDebugMessage.text = debugText;
    }
}
