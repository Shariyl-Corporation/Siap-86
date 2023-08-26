using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTanya : BaseInterrogateButton {
    void OnMouseDown() {
        Debug.Log("Chat clicked");
        audioManager.Click();
        StartCoroutine(
            dialogueManager.BasicChat());
    }
}
