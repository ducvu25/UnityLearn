using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float speedGame = 10f;
    [SerializeField] float speedPoost = 5;
    [SerializeField] float timePoost = 3;
    float _timePoost = 0;
    [SerializeField] float speedY = 10f;

    private Rigidbody rb;
    [SerializeField] bool isActive = true;
    [SerializeField] GameObject goPaticel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        goPaticel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive) return;
        // Lấy đầu vào từ các phím mũi tên
        float moveVertical = 1;//Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
        float moveHorizontal = Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);

        float speed = speedGame + speedPoost*_timePoost/timePoost;
        Debug.Log(speed);
        if(_timePoost > 0) _timePoost -= Time.deltaTime;
        // Tính toán vector di chuyển
        Vector3 movement = new Vector3(speedY*moveHorizontal, 0, speed * moveVertical) * Time.deltaTime;

        // Di chuyển xe
        rb.MovePosition(rb.position + movement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Trap")){
            isActive = false;
            rb.velocity = Vector3.zero;
            RoadController.instance.EndGame();
            goPaticel.SetActive(true);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
        {
            _timePoost = timePoost;
            other.gameObject.SetActive(false);
        }
    }
}