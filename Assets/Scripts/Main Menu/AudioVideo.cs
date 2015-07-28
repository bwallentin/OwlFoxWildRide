using UnityEngine;
using System.Collections;

public class AudioVideo : MonoBehaviour {

    public Camera GameCamera;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("Game Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Game Volume");
        }

        if (PlayerPrefs.HasKey("Game FOV"))
        {
            GameCamera.fieldOfView = PlayerPrefs.GetFloat("Game FOV");
        }
	}
}
