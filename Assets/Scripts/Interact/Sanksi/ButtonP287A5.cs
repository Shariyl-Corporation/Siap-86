using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonP287A5 : BaseInterrogateButtonSanksi {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        // if (!isAsked) {
        //     SetAsked();
            audioManager.Click();
            ToggleState();
            dialogueManager.OnClickP287A5();
        // }
    }

}
