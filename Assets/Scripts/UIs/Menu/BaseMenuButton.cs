using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseMenuButton : BaseButton {
    TextMeshPro text;
    protected override void Start() {
        base.Start();
        text = GetComponentInChildren<TextMeshPro>();
    }

    protected override void OnMouseOver() {
        base.OnMouseOver();
        text.color = Color.black;
    }

    protected override void OnMouseExit() {
        base.OnMouseExit();
        text.color = Color.white;
    }
}
