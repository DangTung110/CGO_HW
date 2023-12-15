using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float speed = 10.0f; // tốc độ của xe
    float horizontal;
    float vertical;
    private TargetEnum nextTarget = TargetEnum.TopLeft; // Gán trạng thái
    private Transform currentTarget;
    private Transform topLeftTransform;
    private Transform topRightTransform;
    private Transform bottomLeftTransform;
    private Transform bottomRightTransform;
    [SerializeField] DriverMode mode = DriverMode.Manual;
    [SerializeField] double damaged = 0.0f; // mức độ hư hại của xe
    [SerializeField] float fuel = 0; // lượng xăng hiên có
    [SerializeField] float capacity = 100; // tổng lượng xăng

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    // Start is called before the first frame update
    void Start()
    { 
        currentTarget = topLeftTransform;
        fuel = capacity;
    }
    // Update is called once per frame
    void Update()
    {
        if (mode == DriverMode.Auto)
            AutoMode();
        else if (mode == DriverMode.Manual)
            ManualMode();
    }
    //private void FixedUpdate()
    //{
    //    if (mode == DriverMode.Manual)
    //        ManualMode();
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")  damaged += 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Petro2")
        {
            fuel += 25;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Petro1") 
        { 
            capacity += 10; 
            other.gameObject.SetActive(false);
        }
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
        if (other.gameObject.tag == "GoalLine")
            Time.timeScale = 0f;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void ManualMode()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (0f, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        //Vector3 movement = new Vector3 (horizontal, 0f, vertical) * speed;
        //rb.AddForce(movement);
        fuel -= vertical * Time.deltaTime;
        transform.Rotate(0f, 10 * speed * Time.deltaTime * horizontal, 0f);
        if (damaged == 100) Time.timeScale = 0f;

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
