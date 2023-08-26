using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTilang : BaseInterrogateButton {
    void OnMouseDown() {
        Debug.Log("Chat clicked");
            audioManager.Click();
            StartCoroutine(
                dialogueManager.BasicChat());
    }
}
