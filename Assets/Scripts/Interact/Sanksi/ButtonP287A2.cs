using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonP287A2 : BaseInterrogateButtonSanksi {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();

        // if (!isAsked) {
        //     SetAsked();
            AudioManager.Instance.PlaySFX(AudioManager.sfx.select);
            ToggleState();
            dialogueManager.OnClickP287A2();
        // }
    }

}
