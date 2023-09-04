using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLepas : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();
        
        AudioManager.Instance.PlaySFX(AudioManager.sfx.select);
        dialogueManager.OnClickLepas();
    }
}
