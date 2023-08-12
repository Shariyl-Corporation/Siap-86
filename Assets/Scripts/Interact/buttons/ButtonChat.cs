using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChat : BaseButton
{
    void OnMouseDown() {
        Debug.Log("Chat clicked");
        if (!isAsked) {
            spriteRenderer.sprite = askedSprite;
            audioManager.Click();
            StartCoroutine(
                dialogueManager.StrikeConversation());
        }
    }
}
