using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVerdict : BaseButton
{
    void OnMouseDown() {
        Debug.Log("Chat clicked");
        if (!isAsked) {
            spriteRenderer.sprite = askedSprite;
            audioManager.Click();
            dialogueManager.StrikeConversation();
        }
    }
}
