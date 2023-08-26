using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSTNK : BaseInterrogateButton {
    void OnMouseDown() {
        Debug.Log("Chat clicked");
        if (!isAsked) {
            spriteRenderer.sprite = askedSprite;
            audioManager.Click();
            StartCoroutine(
                dialogueManager.AskDocumentSTNK(
                    WorldControl.Instance.ActiveCar.driver.hasSTNK
                ));
        }
    }
}
