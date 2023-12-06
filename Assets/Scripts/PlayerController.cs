using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public Camera camera;
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
        Vector3 carPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        transform.position = carPos;
        camera.transform.position = carPos + new Vector3(0, 7, -20);
    }
}
