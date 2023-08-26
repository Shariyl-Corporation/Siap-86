using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLepas : BaseInterrogateButton {
    void OnMouseDown() {
        audioManager.Click();
        dialogueManager.OnClickGanti();
    }
}
