using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public Button playButton;
    public Button quitButton;

	void Start () {
		playButton.onClick.AddListener(goToGame);
        quitButton.onClick.AddListener(quit);
	}

	void goToGame(){
		Debug.Log ("You have clicked the button!");
        SceneManager.LoadScene("Testing_AI", LoadSceneMode.Single);
	}

    void quit() {
        Application.Quit();
    }
}