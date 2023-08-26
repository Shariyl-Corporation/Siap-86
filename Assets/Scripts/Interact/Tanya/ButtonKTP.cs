using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKTP : BaseInterrogateButton {
    void OnMouseDown() {
        Debug.Log("Chat clicked");
        if (!isAsked) {
            spriteRenderer.sprite = askedSprite;
            audioManager.Click();
            StartCoroutine(
                dialogueManager.AskDocumentKTP(
                    WorldControl.Instance.ActiveCar.driver.hasKTP
                ));
        }
    }

}
