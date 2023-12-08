using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {
        transform.rotation = Quaternion.Euler(90,0,0);
        Vector3 playerPosition = player.transform.position + Vector3.up * 20;
        transform.position = playerPosition;
    }
}
