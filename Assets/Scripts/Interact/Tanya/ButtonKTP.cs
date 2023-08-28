using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKTP : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        audioManager.Click();
        dialogueManager.OnClickKTP();
    }

}
