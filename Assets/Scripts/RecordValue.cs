using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordValue : MonoBehaviour
{

    public string set_level;
    public string set_minute;
    public string set_second;
    public string set_score;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("BestTimeValue") != null && GameObject.Find("BestLevelValue") != null && GameObject.Find("BestScoreValue") != null)
        {
            Text sBestTime = GameObject.Find("BestTimeValue").GetComponent<Text>();
            Text sBestLevel = GameObject.Find("BestLevelValue").GetComponent<Text>();
            Text sBestScore = GameObject.Find("BestScoreValue").GetComponent<Text>();

            if (PlayerPrefs.HasKey("score"))
            {
                sBestTime.text = PlayerPrefs.GetString("minute") + ":" + PlayerPrefs.GetString("second");

                sBestLevel.text = PlayerPrefs.GetString("level");

                sBestScore.text = PlayerPrefs.GetString("score");
            }


        }
    }
}
