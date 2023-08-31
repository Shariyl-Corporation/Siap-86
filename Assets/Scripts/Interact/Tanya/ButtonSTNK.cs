using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSTNK : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        audioManager.Click();
        dialogueManager.OnClickSTNK();
    }
}



