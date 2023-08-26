using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGanti: BaseInterrogateButton {
    void OnMouseDown() {
        Debug.Log("Button clicked");
        audioManager.Click();
        
        dialogueManager.SwapCard(); 
    }
}
