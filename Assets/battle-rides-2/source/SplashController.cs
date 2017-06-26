using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour {

	private void Update () {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene(1);
        }
	}
}
