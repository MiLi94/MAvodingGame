using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleMove : MonoBehaviour {
    public static int Dcount;
    public static int Xcount;
    public static int Ycount;
    public static int Icount;
    public static int Score;
    public static int flag;
    public static int outcome;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnCollisionStay(Collision colli)
    {
     
        if ( this.gameObject.tag.Equals("red") || this.gameObject.tag.Equals("green") || this.gameObject.tag.Equals("blue") || this.gameObject.tag.Equals("debris"))
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -2.0f, 0);
        if (colli.gameObject.name.Equals("Shuttle"))
            colli.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void OnCollisionExit(Collision colli)
    {
        if ( this.gameObject.tag.Equals("red") || this.gameObject.tag.Equals("green") || this.gameObject.tag.Equals("blue") || this.gameObject.tag.Equals("debris"))
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -2.0f, 0);
        if (colli.gameObject.name.Equals("Shuttle"))
            colli.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter(Collision colli)
    {
        if (this.gameObject.tag.Equals("red") || this.gameObject.tag.Equals("green") || this.gameObject.tag.Equals("blue") || this.gameObject.tag.Equals("debris"))
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -2.0f, 0);
        if (colli.gameObject.name.Equals("Shuttle"))
            colli.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (!(colli.gameObject.tag.Equals("wall") || colli.gameObject.name.Equals("Shuttle")))
        {
            if (!(this.gameObject.tag.Equals("wall") || this.gameObject.name.Equals("Shuttle")))
            {
                Delete(this.gameObject);
            }
        }
        
        if (colli.gameObject.name.Equals("Shuttle"))
        {
            Vector3 pos = colli.gameObject.transform.position - this.gameObject.transform.position;
     
            if (this.gameObject.tag.Equals("debris"))
            {
                if ((pos.x < 0.5 || pos.x > -0.5 ) && pos.y < 0)
                {
                    Dcount++;
                    GameObject.Find("Canvas/DH").GetComponent<Text>().text = (Dcount).ToString();    
                        Score += 5; 
                    GameObject.Find("Canvas/S").GetComponent<Text>().text = (Score).ToString();
                    Destroy(this.gameObject);
                }
            }
            
            {

           
                    if (pos.x < 0)
                    {
                        if (this.gameObject.transform.position.x < 2)
                            this.gameObject.transform.Translate(1.0f, 0, 0);
                    }
                    else
                    {
                        if (this.gameObject.transform.position.x > -2)
                            this.gameObject.transform.Translate(-1.0f, 0, 0);
                    }
               
            }
        }
    }

    void Delete(GameObject go)
    {
        if (go.tag.Equals("debris"))
            return;
        ArrayList removesX = new ArrayList();
        ArrayList removesY = new ArrayList();
        GameObject[] neighbors = GameObject.FindGameObjectsWithTag(go.tag);
 
        foreach (GameObject target in neighbors)
        {
            foreach (GameObject neighbor in neighbors)
            {
                if (!neighbor.Equals(target))
                {
                    Vector3 temp3 = (target.transform.position - neighbor.transform.position);
                    float disX = Mathf.Abs(temp3.x);
                    float disY = Mathf.Abs(temp3.y);
                    float dis = Mathf.Sqrt(disX * disX + disY * disY);
                    // 0.7, 1.4
                    if (dis >= .92 && dis <= 1.3)
                    {
                        if (disX >= .92 && disX <= 1.3)
                        {
                            removesX.Add(neighbor);
                        }
                        if (disY >= .92 && disY <= 1.3)
                        {
                            removesY.Add(neighbor);
                        }
                    }
                }
            }
            if(removesX.Count<2)
                removesX.Clear();
            if (removesY.Count < 2)
                removesY.Clear();
            if (removesX.Count == 2)
            {
                foreach (GameObject remove in removesX)
                {
                    Destroy(remove);
                }
                Xcount++;
                Debug.Log(" Xcount " + Xcount.ToString());
                GameObject.Find("Canvas/IPG").GetComponent<Text>().text = (++Icount/2).ToString();
                Score += 15;
               
                GameObject.Find("Canvas/S").GetComponent<Text>().text = (Score).ToString();
                Destroy(target);
                return;
            }
            if (removesY.Count == 2)
            {
                foreach (GameObject remove in removesY)
                { 
                    Destroy(remove);
                }
                Ycount++;
                GameObject.Find("Canvas/IPG").GetComponent<Text>().text = (++Icount/2).ToString();
                Debug.Log(" Ycount " + Ycount.ToString());
                Score += 10 / 2;
                GameObject.Find("Canvas/S").GetComponent<Text>().text = (Score).ToString();
                Destroy(target);
                return;
            }
        }
       

    }
    void Check()
    {
        if (Score >= 100)
        {
            outcome = 100;
            return;
        }
        ArrayList obj = new ArrayList();
        obj.AddRange(GameObject.FindGameObjectsWithTag("debris"));
        int fc = 0;
        foreach (GameObject go in obj)
        {
            if (go.transform.position.y <= -1.5)
            {
                fc++;
            }
        }
        if (fc >= 5)
        {
            outcome = 2;
            return;
        }

        obj.Clear();
        fc = 0;
        obj.AddRange(GameObject.FindGameObjectsWithTag("red"));
        obj.AddRange(GameObject.FindGameObjectsWithTag("blue"));
        obj.AddRange(GameObject.FindGameObjectsWithTag("green"));
        foreach (GameObject go in obj)
        {
            if (go.transform.position.y <= -1.5)
            {
                fc++;
            }
        }
        if (fc >= 25)
        {
            outcome = 1;
            return;
        }
    }

    void OnGUI()

    {
        Check();
        switch (outcome)
        {
         
            case 1:
                GUI.color = new Color(1,0.647f,0,1);
                GUI.Label(new Rect(Screen.width / 4 + 200, Screen.height / 4 + 100, 100, 50), "Opps! Ionised particles are not grouped in the storage!");
                Time.timeScale = 0;
                break;
            case 2:
                GUI.color = Color.black;
                GUI.Label(new Rect(Screen.width / 4 + 200, Screen.height / 4 + 100, 100, 50), "Opps! Debris are filled in the storage!");
                Time.timeScale = 0;
                break;
            case 100:
                GUI.color = Color.yellow;
                GUI.Label(new Rect(Screen.width / 4 + 200, Screen.height / 4 + 100, 100, 50), "Congratulations! You Won!");
                Time.timeScale = 0;
                break;
            default:
                break;
        }

            
        

}
}
