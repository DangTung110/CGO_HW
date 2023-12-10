using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // 0 la chuot trai , 1 la chuot phat , 2 scroll
        {
            Debug.Log("<color=red> Mau do </color>");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Nguoi dung bam trong khung hien tai");
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Nguoi dung nha trong khung hien tai");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Nguoi bam phim space trong khung hien tai");
        }
    }
}
