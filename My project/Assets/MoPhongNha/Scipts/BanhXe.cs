using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanhXe : MonoBehaviour
{
    [SerializeField] Vector3 speed;
    public bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
            transform.Rotate(speed*Time.deltaTime); 
    }
}
