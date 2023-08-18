using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public int PlayerPublicOpinion = 0;
    public int PlayerHealth = 0;
    public int PlayerMental = 0;
    public int PlayerMoney = 0;
    public int PlayerCorrectCount = 0;
    public int PlayerIncorrectCount = 0;


    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
