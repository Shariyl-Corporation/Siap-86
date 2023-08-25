using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Play : BaseMenuButton {
    void OnMouseDown() {
        MainMenu.Instance.OnClickPlay();
    }
}