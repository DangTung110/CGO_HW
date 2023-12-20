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
    float speed = 0.2f; // tốc độ của xe
    public float rotationSpeed = 50f;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }
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
    }
    private void FixedUpdate()
    {
        if (mode == DriverMode.Manual)
        {
            ManualMode();
        }
    }
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
        // Thêm hàm xử lý di chuyển và xoay. Set freeze rotation ngoài rigidbody để xe không bị xoay.
        MoveCar(vertical);
        RotateCar(horizontal);

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

    void MoveCar(float input)
    {
        // Tính toán vectơ di chuyển
        Vector3 movement = transform.forward * input * speed;

        // Áp dụng lực di chuyển lên Rigidbody, thêm lực theo kiểu thay đổi vận tốc thì xe sẽ đi mượn hơn
        rb.AddForce(movement, ForceMode.VelocityChange);
    }

    void RotateCar(float input)
    {
        // Tính toán góc xoay
        float rotation = input * rotationSpeed * Time.deltaTime;

        // Xoay xe quanh trục y
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
