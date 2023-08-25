using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Quit : BaseMenuButton {
    void OnMouseDown() {
        MainMenu.Instance.OnClickQuit();
    }
}