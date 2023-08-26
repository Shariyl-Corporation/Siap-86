using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAkhiri : BaseInterrogateButton {
    void OnMouseDown() {
        if (!dialogueManager.allowAction) return;

        Debug.Log("Button clicked");
        audioManager.Click();

        StartCoroutine(
            dialogueManager.GiveVerdict(
                WorldControl.Instance.ActiveCar.driver.hasKTP
            ));
    }
}
