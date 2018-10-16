using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scores : MonoBehaviour
{
    public static int score = 0;
    public static bool isEnded;
    public int times;
    public static int life = 1;
    public int bscoref;
    public float lastBlueTime;
    public float lastGoldenTime;
    public bool blueFlag;
    public bool goldenFlag;

    public float lastRedTime;
    public float lastGreenTime;
    public bool greenFlag;
    public bool redFlag;
    public static float curTime;

    public static string string_second;

    public static string string_minute;


    public static GameObject magic;

    public static GameObject win;

    public static int bestScore;


    public static float timeStart;
    // Use this for initialization


    void Start()
    {
        Debug.Log("begin");

        times = 2;
        //score = 0;
       // life = 1;
        isEnded = false;
        //timeStart=Time.time;

        magic = GameObject.Find("Magic");
        win = GameObject.Find("WinOrLose");

       if(PlayerPrefs.HasKey("score")){
           bestScore=int.Parse(PlayerPrefs.GetString("score"));
       }
       else{
           bestScore=0;
       }

       
    }

    // Update is called once per frame
    void Update()
    {

        curTime = Time.time;
        timeStart = PanelManager.timeStart;
        if (curTime - timeStart > 0)
        {
            int second = (int)(curTime - timeStart) % 60;
            int minute = (int)(curTime - timeStart) / 60;
            string_second = "" + second;
            string_minute = "" + minute;
            if (minute < 10)
            {
                string_minute = "0" + minute;
            }
         //   Debug.Log("Minute" + string_minute);

            if (second < 10)
            {
                string_second = "0" + second;

            }
        //    Debug.Log("Second" + string_second);
            if (GameObject.Find("CurTimeValue") != null)
            {
                Text Ttext = GameObject.Find("CurTimeValue").GetComponent<Text>();

                Ttext.text = string_minute + ":" + string_second;
            }

        }
        if (blueFlag)
        {
            if (curTime - lastBlueTime >= 10)
            {
                lastBlueTime = curTime;
                Generator.slowspeed = 1;
                blueFlag = false;
                Debug.Log("slow over");
                magic.GetComponent<Text>().text = "";

            }
        }
        if (greenFlag)
        {
            if (curTime - lastGreenTime >= 10)
            {
                lastGreenTime = curTime;
                greenFlag = false;
                magic.GetComponent<Text>().text = "";

            }
        }
        if (redFlag)
        {
            if (curTime - lastRedTime >= 10)
            {
                lastBlueTime = curTime;
                redFlag = false;
                magic.GetComponent<Text>().text = "";

            }
        }
        if (goldenFlag)
        {
            if (curTime - lastGoldenTime >= 10)
            {
                lastGoldenTime = curTime;
                times = 2;
                goldenFlag = false;
                GameObject[] obstacle = GameObject.FindGameObjectsWithTag("obstacle");
                for (int i = 0; i < obstacle.Length; i++)
                {
                    Rigidbody rb = obstacle[i].GetComponent<Rigidbody>();
                    rb.velocity = rb.velocity * 2.0f;
                }
                Debug.Log("double over");
                magic.GetComponent<Text>().text = "";

            }
        }

        if(score>bestScore&&!isEnded){
                        win.GetComponent<Text>().text = "New Best Scores";

        }
        if (score >= 100)
        {
            win.GetComponent<Text>().text = "You Win";
        }


    }

    void OnTriggerEnter(Collider collider)
    {


        if (collider.tag.Equals("wall") && this.gameObject.tag.Equals("obstacle"))
        {
            
            Destroy(this.gameObject);
        }

        if (collider.tag.Equals("obstacle") && this.gameObject.tag.Equals("wall"))
        {
            Destroy(this.gameObject);
        }

        if (collider.tag.Equals("target") && this.gameObject.tag.Equals("Player"))
        {
            SoundManage.PlaySound("getPoint", "NCamera");
            score = score + times;
            DisplayScore(score);
            Destroy(collider.gameObject);
           
            // GameObject.Find("NCamera").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/getPoint");
            // GameObject.Find("NCamera").GetComponent<AudioSource>().Play
            
        }

        if (collider.tag.Equals("red") && this.gameObject.tag.Equals("Player"))
        {
            SoundManage.PlaySound("magic", "NCamera");
            lastRedTime = Time.time;
            redFlag = true;
            GameObject[] obstacle = GameObject.FindGameObjectsWithTag("obstacle");
            for (int i = 0; i < obstacle.Length; i++)
            {
                Destroy(obstacle[i]);
            }
            Destroy(collider.gameObject);
            magic.GetComponent<Text>().text = "Red Magic\nClear Obstacles!";

            Debug.Log("clear");
        }

        if (collider.tag.Equals("golden") && this.gameObject.tag.Equals("Player"))
        {
            SoundManage.PlaySound("magic", "NCamera");

            lastGoldenTime = Time.time;
            goldenFlag = true;
            times = 4;
            Destroy(collider.gameObject);
            magic.GetComponent<Text>().text = "Golden Magic\nDobule Score!";

            Debug.Log("dobule");
        }

        if (collider.tag.Equals("green") && this.gameObject.tag.Equals("Player"))
        {
            SoundManage.PlaySound("magic", "NCamera");

            lastGreenTime = Time.time;
            greenFlag = true;
            life++;
            DisplayHP(life);
            Destroy(collider.gameObject);
            magic.GetComponent<Text>().text = "Green Magic\nHP UP!";

            Debug.Log("+1 " + life);

        }

        if (collider.tag.Equals("blue") && this.gameObject.tag.Equals("Player"))
        {
            SoundManage.PlaySound("magic", "NCamera");

            lastBlueTime = Time.time;
            blueFlag = true;
            Generator.slowspeed = 0.5f;
            GameObject[] obstacle = GameObject.FindGameObjectsWithTag("obstacle");
            for (int i = 0; i < obstacle.Length; i++)
            {
                Rigidbody rb = obstacle[i].GetComponent<Rigidbody>();
                rb.velocity = rb.velocity * 0.5f;
            }
            Destroy(collider.gameObject);
            magic.GetComponent<Text>().text = "Blue Magic\nSlow Down!";

            Debug.Log("slow");
        }

        if (collider.tag.Equals("obstacle") && this.gameObject.tag.Equals("Player"))
        {
            if (life <= 1)
            {
                life--;
                //DisplayHP(life);

                Debug.Log("end");
                isEnded = true;
                SoundManage.PlaySound("lose", "NCamera");

                win.GetComponent<Text>().text = "You Dead!";
                Time.timeScale = 0;
            }
            else
            {

                life--;
                DisplayHP(life);
                Destroy(collider.gameObject);
                Debug.Log("-1 " + life);
            }

        }

    }

    void DisplayScore(float score_val)
    {



        if (GameObject.Find("CurrScoreValue") != null)
        {
            Text Ttext = GameObject.Find("CurrScoreValue").GetComponent<Text>();
            Ttext.text = "" + score_val;
        }

    }

    void DisplayHP(float hp_val)
    {

        if (GameObject.Find("HPValue") != null)
        {
            Text Ttext = GameObject.Find("HPValue").GetComponent<Text>();
            Ttext.text = "" + hp_val;
        }


    }
}
