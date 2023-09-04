using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonP288A2 : BaseInterrogateButtonSanksi {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        // if (!isAsked) {
        //     SetAsked();
            AudioManager.Instance.PlaySFX(AudioManager.sfx.select);
            ToggleState();
            dialogueManager.OnClickP288A2();
        // }
    }

}
