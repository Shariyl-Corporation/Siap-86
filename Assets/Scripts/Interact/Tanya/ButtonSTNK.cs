using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSTNK : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        // if (!isAsked) {
        //     SetAsked();
            audioManager.Click();
            dialogueManager.OnClickSTNK();
        // }
    }
}



