using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSIM : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        // if (!isAsked) {
        //     SetAsked();
            audioManager.Click();
            dialogueManager.OnClickSIM();
        // }
    }
}
