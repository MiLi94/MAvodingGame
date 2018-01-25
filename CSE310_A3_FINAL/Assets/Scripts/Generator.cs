using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public SpriteRenderer image;
    public Sprite[] sprites;
    private float lastTime;
    public static int level;
    public static float slowspeed;
    public GameObject obstacle;
    public GameObject target;
    public GameObject blueMagic;
    public GameObject redMagic;
    public GameObject greenMagic;
    public GameObject goldenMagic;
    public Vector3[] Opos;
    public Vector3[] Tpos;
    public int magicCount;
    public int targetCount;
    // Use this for initialization

    public Button backButton;
    void Start()
    {
        //SoundManage.PlayMusic("background", "NCamera");
        image = GameObject.FindGameObjectWithTag("bg").GetComponent<SpriteRenderer>();
        level = 2;
        DisplayLevel(level - 1);
        slowspeed = 1;
        Opos = new Vector3[]{
            new Vector3(-61.5f, 10, -11.5f), new Vector3(-61.5f, 0, -11.5f), new Vector3(-61.5f, -10, -11.5f),
        new Vector3(61.5f, 10, -11.5f), new Vector3(61.5f, 0, -11.5f), new Vector3(61.5f, -10, -11.5f),
        new Vector3(-10, 40,-11.5f), new Vector3(0, 40,-11.5f), new Vector3(10, 40,-11.5f),
              new Vector3(-10, -40,-11.5f), new Vector3(0, -40,-11.5f), new Vector3( 10, -40,-11.5f)};
        Tpos = new Vector3[]
        {
             new Vector3(-10.0f, 10.0f, 0), new Vector3(0, 10.0f, 0), new Vector3(10.0f, 10.0f, 0),
              new Vector3(-10.0f, 0, 0), new Vector3(0, 0, 0), new Vector3(10.0f, 0, 0),
               new Vector3(-10.0f, -10.0f, 0), new Vector3(0, -10.0f, 0), new Vector3(10.0f, -10.0f, 0),
        };

        // Time.timeScale = 0;
        lastTime = Time.time;

        InvokeRepeating("NewObstacle", 0, 3);
        InvokeRepeating("NewTarget", 1, 3);
        InvokeRepeating("NewMagic", 10, 10);

        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (scores.score <= 20)
        {
            level = 2;
            image.sprite = sprites[0];

            //   Debug.Log("level 1");
        }
        else if (scores.score <= 50)
        {
            level = 3;
            image.sprite = sprites[1];

            //   Debug.Log("level 2");
        }
        else if (scores.score <= 100)
        {
            level = 4;
            image.sprite = sprites[2];

            //   Debug.Log("level 3");
        }
        DisplayLevel(level - 1);

    }

    void NewTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        if (targets.Length == 0)
        {
            int playerpos = PlayerPos();
            int i = Random.Range(0, Tpos.Length);
            while (i == targetCount || i == playerpos)
            {
                i = Random.Range(0, Tpos.Length);
            }
            targetCount = i;
            GameObject prefab = Instantiate(target) as GameObject;
            prefab.transform.Translate(Tpos[i]);
          //  prefab.transform.Rotate(new Vector3(0, 0, 45));
        }
    }

    void NewMagic()
    {
        GameObject[] bluemagic = GameObject.FindGameObjectsWithTag("blue");
        GameObject[] redmagic = GameObject.FindGameObjectsWithTag("red");
        GameObject[] greenmagic = GameObject.FindGameObjectsWithTag("green");
        GameObject[] goldenmagic = GameObject.FindGameObjectsWithTag("golden");
        if (bluemagic.Length == 0 && redmagic.Length == 0 && greenmagic.Length == 0 && goldenmagic.Length == 0)
        {
            int i = Random.Range(1, 5);
            while (i == magicCount)
            {
                i = Random.Range(1, 5);
            }
            magicCount = i;
            int playerpos = PlayerPos();

            switch (i)
            {
                case 1:
                    {

                        GameObject prefab = Instantiate(blueMagic) as GameObject;
                        int j = Random.Range(0, Tpos.Length);
                        while (j == targetCount || j == playerpos)
                        {
                            j = Random.Range(0, Tpos.Length);
                        }
                        prefab.transform.Translate(Tpos[j]);
                //        prefab.transform.Rotate(new Vector3(0, 0, 45));


                        break;
                    }
                case 2:
                    {

                        GameObject prefab = Instantiate(redMagic) as GameObject;
                        int j = Random.Range(0, Tpos.Length);
                        while (j == targetCount || j == playerpos)
                        {
                            j = Random.Range(0, Tpos.Length);
                        }
                        prefab.transform.Translate(Tpos[j]);
                     //   prefab.transform.Rotate(new Vector3(0, 0, 45));

                        break;
                    }
                case 3:
                    {

                        GameObject prefab = Instantiate(greenMagic) as GameObject;
                        int j = Random.Range(0, Tpos.Length);
                        while (j == targetCount || j == playerpos)
                        {
                            j = Random.Range(0, Tpos.Length);
                        }
                        prefab.transform.Translate(Tpos[j]);
                     //   prefab.transform.Rotate(new Vector3(0, 0, 45));

                        break;
                    }
                case 4:
                    {

                        GameObject prefab = Instantiate(goldenMagic) as GameObject;
                        int j = Random.Range(0, Tpos.Length);
                        while (j == targetCount || j == playerpos)
                        {
                            j = Random.Range(0, Tpos.Length);
                        }
                        prefab.transform.Translate(Tpos[j]);
                      //  prefab.transform.Rotate(new Vector3(0, 0, 45));

                        break;
                    }
            }
        }

    }

    void NewObstacle()
    {

        switch (level)
        {
            case 1:
                {
                    int i = Random.Range(0, Opos.Length);
                    GameObject prefab = Instantiate(obstacle) as GameObject;
                    prefab.transform.Translate(Opos[i]);
                    // prefab.transform.Rotate(new Vector3(90, 0, 0));
                    Rigidbody rb = prefab.GetComponent<Rigidbody>();
                    rb.velocity = velocityGen(Opos[i]);
                    //Debug.Log(" Generate at " + Opos[i]);
                    // 老年
                    break;
                }
            case 2:
                {
                    int i = Random.Range(0, Opos.Length);
                    GameObject prefab = Instantiate(obstacle) as GameObject;
                    prefab.transform.Translate(Opos[i]);
                    // prefab.transform.Rotate(new Vector3(90, 0, 0));
                    Rigidbody rb = prefab.GetComponent<Rigidbody>();
                    rb.velocity = velocityGen(Opos[i]);
                    break;
                }
            case 3:
                {
                    int i = Random.Range(0, Opos.Length);
                    GameObject prefab = Instantiate(obstacle) as GameObject;
                    prefab.transform.Translate(Opos[i]);
                    //prefab.transform.Rotate(new Vector3(90, 0, 0));
                    Rigidbody rb = prefab.GetComponent<Rigidbody>();
                    rb.velocity = velocityGen(Opos[i]);

                    int j = opposite(i);
                    Debug.Log("this is j " + j);
                    GameObject prefab2 = Instantiate(obstacle) as GameObject;
                    prefab2.transform.Translate(Opos[j]);
                    //prefab2.transform.Rotate(new Vector3(90, 0, 0));
                    Rigidbody rb2 = prefab2.GetComponent<Rigidbody>();
                    rb2.velocity = velocityGen(Opos[j]);
                    break;
                }
            case 4:
                {
                    int i = Random.Range(0, Opos.Length);
                    GameObject prefab = Instantiate(obstacle) as GameObject;
                    prefab.transform.Translate(Opos[i]);
                    //prefab.transform.Rotate(new Vector3(90, 0, 0));
                    Rigidbody rb = prefab.GetComponent<Rigidbody>();
                    rb.velocity = velocityGen(Opos[i]);

                    int j = sameside(i);
                    GameObject prefab2 = Instantiate(obstacle) as GameObject;
                    prefab2.transform.Translate(Opos[j]);
                    //prefab2.transform.Rotate(new Vector3(90, 0, 0));
                    Rigidbody rb2 = prefab2.GetComponent<Rigidbody>();
                    rb2.velocity = velocityGen(Opos[j]);
                    break;
                }
        }


    }

    Vector3 velocityGen(Vector3 vec)
    {
        float speed = 0;
        switch (level)
        {
            case 1:
                speed = 2.5f * level * slowspeed;
                break;
            case 2:
                speed = 6.5f * level * slowspeed;
                break;
            case 3:
                speed = 8 * level * slowspeed;
                break;
            case 4:
                speed = 9 * level * slowspeed;
                break;
        }
        Vector3 vel = new Vector3();
        if (vec.x < -61 && vec.x > -62)
            vel = new Vector3(speed, 0, 0);
        if (vec.x > 61 && vec.x < 62)
            vel = new Vector3(-speed, 0, 0);
        if (vec.y > 39 && vec.y < 41)
            vel = new Vector3(0, -speed, 0);
        if (vec.y < -39 && vec.y > -41)
            vel = new Vector3(0, speed, 0);
        return vel;
    }

    int opposite(int pos)
    {
        int res = 0;
        switch (pos)
        {
            case 0:
                res = 5;
                break;
            case 1:
                res = 4;
                break;
            case 2:
                res = 3;
                break;
            case 3:
                res = 2;
                break;
            case 4:
                res = 1;
                break;
            case 5:
                res = 0;
                break;
            case 6:
                res = 11;
                break;
            case 7:
                res = 10;
                break;
            case 8:
                res = 9;
                break;
            case 9:
                res = 8;
                break;
            case 10:
                res = 7;
                break;
            case 11:
                res = 6;
                break;
        }
        return res;
    }

    int sameside(int pos)
    {
        int res = 0;
        switch (pos)
        {
            case 0:
                res = 1;
                break;
            case 1:
                res = 2;
                break;
            case 2:
                res = 0;
                break;
            case 3:
                res = 4;
                break;
            case 4:
                res = 5;
                break;
            case 5:
                res = 3;
                break;
            case 6:
                res = 7;
                break;
            case 7:
                res = 8;
                break;
            case 8:
                res = 6;
                break;
            case 9:
                res = 10;
                break;
            case 10:
                res = 11;
                break;
            case 11:
                res = 9;
                break;
        }
        return res;
    }

    int PlayerPos()
    {
        int res = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x < -2.9 && player.transform.position.x > -3.1)
        {
            if (player.transform.position.y > 2.9 && player.transform.position.y < 3.1)
            {
                res = 0;
            }
            if (player.transform.position.y > -0.1 && player.transform.position.y < 0.1)
            {
                res = 3;
            }
            if (player.transform.position.y > -3.1 && player.transform.position.y < -2.9)
            {
                res = 6;
            }
        }
        if (player.transform.position.x > -0.1 && player.transform.position.x < 0.1)
        {
            if (player.transform.position.y > 2.9 && player.transform.position.y < 3.1)
            {
                res = 1;
            }
            if (player.transform.position.y > -0.1 && player.transform.position.y < 0.1)
            {
                res = 4;
            }
            if (player.transform.position.y > -3.1 && player.transform.position.y < -2.9)
            {
                res = 7;
            }
        }
        if (player.transform.position.x > 2.9 && player.transform.position.x < 3.1)
        {
            if (player.transform.position.y > 2.9 && player.transform.position.y < 3.1)
            {
                res = 2;
            }
            if (player.transform.position.y > -0.1 && player.transform.position.y < 0.1)
            {
                res = 5;
            }
            if (player.transform.position.y > -3.1 && player.transform.position.y < -2.9)
            {
                res = 8;
            }
        }
        return res;
    }

    void DisplayLevel(float value)
    {
        if (GameObject.Find("CurrLevelVal") != null)
        {
            Text Ttext = GameObject.Find("CurrLevelVal").GetComponent<Text>();
            Ttext.text = "" + value;

        }


    }

    public void BackMain()
    {
        Application.LoadLevel("MainMenu");
    }

    void onClick()
    {

        if (PlayerPrefs.HasKey("score"))
        {
            string s_score = PlayerPrefs.GetString("score");
            int i_score = int.Parse(s_score);
            if (i_score <= scores.score)
            {
                PlayerPrefs.SetString("level", "" + (level-1));
                PlayerPrefs.SetString("minute", scores.string_minute);
                PlayerPrefs.SetString("second", scores.string_second);
                PlayerPrefs.SetString("score", "" + scores.score);
            }
        }
        else
        {
            PlayerPrefs.SetString("level", "" + (level-1));
            PlayerPrefs.SetString("minute", scores.string_minute);
            PlayerPrefs.SetString("second", scores.string_second);
            PlayerPrefs.SetString("score", "" + scores.score);
        }
        Application.LoadLevel("MainMenu");
        Time.timeScale=1;
        scores.score=0;

    }
}
