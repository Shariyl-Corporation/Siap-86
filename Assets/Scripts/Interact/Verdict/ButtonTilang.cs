using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTilang : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();
        
        audioManager.Click();
        dialogueManager.OnClickTilang();
    }
}
