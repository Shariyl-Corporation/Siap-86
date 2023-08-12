using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSIM: BaseButton
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
