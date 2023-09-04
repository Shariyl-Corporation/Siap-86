using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTilang : BaseInterrogateButton {
    void OnMouseDown() {
        if (dialogueManager.chose()){
            if (!dialogueManager.allowAction) return;
            OnMouseExit();
            
            AudioManager.Instance.PlaySFX(AudioManager.sfx.select);
            dialogueManager.OnClickTilang();

        }
    }

    protected override void OnMouseOver() {
        if (dialogueManager.chose()) {
            base.OnMouseOver();
        }
    }

}
