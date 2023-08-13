using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSTNK : BaseButton
{
    void OnMouseDown() {
        Debug.Log("Chat clicked");
        if (!isAsked) {
            spriteRenderer.sprite = askedSprite;
            audioManager.Click();
            StartCoroutine(
                dialogueManager.AskDocumentSTNK(
                    GlobalControl.Instance.ActiveCar.driver.hasSTNK
                ));
        }
    }
}
