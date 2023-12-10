using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("End Delay");
    }

    // Update is called once per frame
    void Update()
    {
        // trả về thời gian (tính bằng giây) từ lúc bắt đầu game
        // Debug.Log("Time.time: " + Time.time);

        // trả về thời gian giữa mỗi lần cập nhật khung hình
        //  Debug.Log("Time.deltaTime: " + Time.deltaTime);

        //Time.timeScale = 0f;
        //Time.timeScale = 1f;

        // Tính FPS

        float fps = 1 / Time.deltaTime;
        Debug.Log("FPS: " + fps);
       
    }

    
}
