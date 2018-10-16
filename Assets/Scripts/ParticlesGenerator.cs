using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticlesGenerator : MonoBehaviour {
    private Vector3 iniPos;

    public GameObject BlueParticle;
    public GameObject RedParticle;
    public GameObject GreenParticle;
    public GameObject Debris;
    public int Tcount;
    private float lastTime;
    private float curTime;
    public int count;
    public bool flag = false;
    // Use this for initialization
    void Start () {
        Time.timeScale = 0;
        lastTime = Time.time;
        InvokeRepeating("NewPrefabInstance", 0, 2);
      
    }
	
	// Update is called once per frame
	void Update () {
        curTime = Time.time;
        if (curTime - lastTime >= 1)
        {
            lastTime = curTime;
            GameObject.Find("Canvas/T").GetComponent<Text>().text = (Tcount++).ToString() + " s";
        }
    }



    void NewPrefabInstance()
    {
  
        {


            int i = Random.Range(1, 5);
            while(i==count)
            {
                i = Random.Range(1, 5);
            }
            count = i;
            
            switch (i)
            {
                case 1:
          
                    GameObject prefab = Instantiate(BlueParticle) as GameObject;
             
                          Rigidbody rb = prefab.GetComponent<Rigidbody>();
                       rb.velocity = new Vector3(0, -2.0f, 0);
                   
                    break;
                case 2:
        
                    GameObject prefab1 = Instantiate(RedParticle) as GameObject;
 
                    Rigidbody rb1 = prefab1.GetComponent<Rigidbody>();
                    rb1.velocity = new Vector3(0, -2.0f, 0);
                    break;
                case 3:
 
                    GameObject prefab2 = Instantiate(GreenParticle) as GameObject;
               
                    Rigidbody rb2 = prefab2.GetComponent<Rigidbody>();
                    rb2.velocity = new Vector3(0, -2.0f, 0);
                    break;
                case 4:
                  
                    GameObject prefab3 = Instantiate(Debris) as GameObject;
                
                    Rigidbody rb3 = prefab3.GetComponent<Rigidbody>();
                    rb3.velocity = new Vector3(0, -2.0f, 0);
                    break;
            }
      
        }
    }

    public bool doWindow0 = true;

    void DoWindow0(int windowID)
    {
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    void OnGUI()
    {
        if(!flag)
        {
            doWindow0 = GUI.Button(new Rect(Screen.width / 4 + 200, Screen.height / 4 + 100, 80, 40), "Start!");
            if (doWindow0)
            {
                Time.timeScale = 1;
                flag = !flag;
            }
        }
    }
}
