using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "LogicData", menuName = "ScriptableObjects/LogicData")]
public class TileLogicData : ScriptableObject
{
    [SerializeField] private TileBase[] tiles;
    

}
