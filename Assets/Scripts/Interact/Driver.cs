using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour{
    public bool hasKTP;
    public bool hasSIM;
    public bool hasSTNK;

    public bool isKTPValid;
    public bool isSIMValid;

    public bool hasDoneTerobosLampuMerah;
    public bool hasDoneSpeedLimit;
    public bool isDrunk;

    public int Age;

    public bool CheckIsVisiblyGuilty() {
        return hasDoneTerobosLampuMerah || hasDoneSpeedLimit || isDrunk;
    }
}