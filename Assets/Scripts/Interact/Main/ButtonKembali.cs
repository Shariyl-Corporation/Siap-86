using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonKembali : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;
        OnMouseExit();
        
        AudioManager.Instance.PlaySFX(AudioManager.sfx.select);
        dialogueManager.OnClickKembali();
    }
}
