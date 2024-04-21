using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAcceleration : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.acceleration.x;
        float z = Input.acceleration.z;

        transform.Translate(x * speed*Time.deltaTime, 0, 0);
        transform.Rotate(0, 0, z * speed*Time.deltaTime);
    }
}
