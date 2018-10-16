using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSound : MonoBehaviour {

    public Toggle soundToggle;
    public static bool soundEnabled;
	// Use this for initialization
	void Start () {
        soundToggle.onValueChanged.AddListener((value) => SoundToggleEvent(value));
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
            soundToggle.isOn = true;
        }
        else
        {
            if (PlayerPrefs.GetInt("sound") == 1)
            {
                soundToggle.isOn = true;

            }
            else
            {
                soundToggle.isOn = false;

            }
        }
        soundEnabled = soundToggle.isOn;

        Debug.Log("sss" + soundEnabled);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SoundToggleEvent(bool value) {
        soundEnabled = value;
        int setValue = soundEnabled ? 1 : 0;
        PlayerPrefs.SetInt("sound", setValue);

        if (value) {
            Debug.Log("S" + soundEnabled);
            SoundManage.recover("NCamera");
            soundToggle.isOn = true;
        }
        else
        {
            Debug.Log("S" + soundEnabled);
            SoundManage.mute("NCamera");
            soundToggle.isOn = false;
        }

    }
}
