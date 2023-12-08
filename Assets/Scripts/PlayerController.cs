using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
   
    // Start is called before the first frame update
    void Start()
    {       
        //camera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveCar();
    }
    void MoveCar()
    {
        //Vector3 carPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        //transform.position = carPos;
        //camera.transform.position = carPos + new Vector3(0, 7, -20);
        Vector3 oz = new Vector3(0, 0, 1);
        Vector3 carPos = transform.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            carPos = carPos + Vector3.left * speed * Time.deltaTime;
            transform.position = carPos;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            carPos = carPos + Vector3.right * speed * Time.deltaTime;
            transform.position = carPos;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            carPos = carPos + oz * speed * Time.deltaTime;
            transform.position = carPos;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            carPos = carPos - oz * speed * Time.deltaTime;
            transform.position = carPos;
        }
    }
}
