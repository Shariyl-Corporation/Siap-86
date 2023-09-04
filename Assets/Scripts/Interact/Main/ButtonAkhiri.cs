using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAkhiri : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();
        AudioManager.Instance.PlaySFX(AudioManager.sfx.select);
        Debug.Log("Button clicked");
        
        dialogueManager.OnClickAkhiri();
    }
}
