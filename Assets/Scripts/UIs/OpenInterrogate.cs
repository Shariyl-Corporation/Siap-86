using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInterrogate : MonoBehaviour
{
    public Animator interrogateAnimator;

    void OnMouseDown() {
        Debug.Log("open");
        interrogateAnimator.SetBool("Open", true);
    }
    
}
