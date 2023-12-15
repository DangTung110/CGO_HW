using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetEnum
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
}
public enum DriverMode
{
    Auto,
    Manual,
}
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private float speed = 20.0f; // tốc độ của xe
    private TargetEnum nextTarget = TargetEnum.TopLeft; // Gán trạng thái
    private Transform currentTarget;
    private Transform topLeftTransform;
    private Transform topRightTransform;
    private Transform bottomLeftTransform;
    private Transform bottomRightTransform;
    [SerializeField] DriverMode mode = DriverMode.Manual;
    [SerializeField] double damaged = 0.0f; // mức độ hư hại của xe
    [SerializeField] int fuel = 0; // lượng xăng hiên có
    [SerializeField] int capacity = 100; // tổng lượng xăng

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    { 
        currentTarget = topLeftTransform;
    }
    // Update is called once per frame
    void Update()
    {
        if (mode == DriverMode.Auto)
            AutoMode();
    }
    private void FixedUpdate()
    {
        if (mode == DriverMode.Manual)
            ManualMode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Petro2") fuel += 25;
        if (collision.gameObject.tag == "Petro1") capacity += 10;
        if (collision.gameObject.tag == "Barrier")  damaged += 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Corn30")
        {
            other.gameObject.SetActive(false);
            damaged *= 0.7;
        }
        if (other.gameObject.tag == "Corn100")
        {
            other.gameObject.SetActive(false);
            damaged = 0;
        }
    }
    private void ManualMode()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (horizontal, 0f, vertical);
        rb.AddForce(movement * speed);
        transform.Rotate(0f, 10 * speed * Time.deltaTime * horizontal, 0f);
    }
    private void AutoMode()
    {
        Vector3 targetPosition = currentTarget.position;
        Vector3 moveDirection = targetPosition - transform.position;

        float distance = moveDirection.magnitude; // Tính khoảng cách giữa 2 tọa độ

        if (distance > 0.1f)
        {
            // Khi chưa tới điểm tiếp theo thì vẫn tiếp tục di chuyển
            transform.position = Vector3.MoveTowards(transform.position,
                currentTarget.position, speed * Time.deltaTime);
            // Từ frame 1 => frame 2  
        }
        else
        {
            // Chuyen target
            SetNextTarget(nextTarget);
        }
        // Thay đổi góc quay theo hướng target
        Vector3 direction = currentTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = targetRotation;
    }
    void SetNextTarget(TargetEnum target)
    {
        switch (target)
        {
            case TargetEnum.TopLeft:
                currentTarget = topLeftTransform;
                nextTarget = TargetEnum.TopRight;
                break;
            case TargetEnum.TopRight:
                currentTarget = topRightTransform;
                nextTarget = TargetEnum.BottomRight;
                break;
            case TargetEnum.BottomRight:
                currentTarget = bottomRightTransform;
                nextTarget = TargetEnum.BottomLeft;
                break;
            case TargetEnum.BottomLeft:
                currentTarget = bottomLeftTransform;
                nextTarget = TargetEnum.TopLeft;
                break;
        }
    }

}
