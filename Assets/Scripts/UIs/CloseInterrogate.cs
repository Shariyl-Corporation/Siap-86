using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInterrogate : MonoBehaviour
{
    public Animator interrogateAnimator;
    // Start is called before the first frame update
    void OnMouseDown() {
        interrogateAnimator.SetBool("Close", true);
    }
}
