using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptHolder : MonoBehaviour {

	// Use this for initialization
	public int startValue = 0;

	void Awake () {
		DontDestroyOnLoad(this.gameObject);
	}

	public void LoadTutorial(int value) {
		startValue = value;
		SceneManager.LoadScene("Tutorial");
	}
}
