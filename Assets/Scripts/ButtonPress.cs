using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour {
	public void ButtonPressed(string newScene) {
		SceneManager.LoadScene(newScene);
	}
	
	public void QuitGamePressed() {
		Application.Quit();
	}
	
}
