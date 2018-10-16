using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update () {
        if (SetSound.soundEnabled)
        {
            SoundManage.recover("NCamera");

        }
        else
        {
            SoundManage.mute("NCamera");

        }

    }
}
