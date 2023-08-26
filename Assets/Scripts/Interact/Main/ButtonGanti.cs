using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGanti: BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        dialogueManager.allowAction = false;

        Debug.Log("Button clicked");
        audioManager.Click();
        
        dialogueManager.OnClickGanti();
    }
}
