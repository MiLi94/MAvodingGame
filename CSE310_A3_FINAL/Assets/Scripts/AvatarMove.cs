using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMove : MonoBehaviour
{
    // Use this for initialization
    public Vector2 startFingerPos;
    public Vector2 curFingerPos;
    public bool isMoved;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!scores.isEnded)
        {
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startFingerPos = Input.GetTouch(0).position;
                    isMoved = false;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    curFingerPos = Input.GetTouch(0).position;
                    isMoved = true;
                }

                float xdis = Mathf.Abs(curFingerPos.x - startFingerPos.x);
                float ydis = Mathf.Abs(curFingerPos.y - startFingerPos.y);
                if (isMoved)
                {
                    if (xdis > ydis)
                    {
                        if (curFingerPos.x - startFingerPos.x > 0)
                        {

                            if (transform.position.x < 3)
                                gameObject.transform.Translate(10.0f, 0, 0);
                        }
                        else
                        {
                            if (transform.position.x > -3)
                                gameObject.transform.Translate(-10.0f, 0, 0);
                        }
                    }
                    else
                    {
                        if (curFingerPos.y - startFingerPos.y > 0)
                        {

                            if (transform.position.y < 3)
                                gameObject.transform.Translate(0, 10.0f, 0);
                        }
                        else
                        {
                            if (transform.position.y > -3)
                                gameObject.transform.Translate(0, -10.0f, 0);
                        }
                    }

                }
            }

#else


        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (transform.position.y < 3)
                gameObject.transform.Translate(0, 10.0f, 0);



        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (transform.position.y > -3)
                gameObject.transform.Translate(0, -10.0f, 0);

        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (transform.position.x > -3)
                gameObject.transform.Translate(-10.0f, 0, 0);

        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (transform.position.x < 3)
                gameObject.transform.Translate(10.0f, 0, 0);

        }
#endif
        }
    }
}
