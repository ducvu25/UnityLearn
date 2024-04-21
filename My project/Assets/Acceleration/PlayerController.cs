using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Vector3 pStart, pEnd;
    [SerializeField] Vector2 timeAlive;
    float tStart, tEnd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began) {
                pStart = Input.GetTouch(0).position;
                tStart = Time.time;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                pEnd = Input.GetTouch(0).position;
                tEnd = Time.time;
                Show();
            }
        }
    }
    void Show()
    {
        Vector3 p = -(pStart - pEnd)/100;
        transform.Translate(p*Time.deltaTime);
        print(pStart + " " + pEnd);
        print(tStart + " " + tEnd); 
    }
}
