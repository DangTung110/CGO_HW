using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 target;
    [SerializeField] private float speed = 20.0f;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + Vector3.up * 20;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        target = player.transform.position + Vector3.up * 20;
        Vector3 newPosition = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        //Vector3 newPosition = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = newPosition;   
    }
}
