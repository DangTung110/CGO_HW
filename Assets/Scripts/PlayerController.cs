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
    [SerializeField] float speed = 10.0f;
    [SerializeField] Transform topLeftTransform;
    [SerializeField] Transform topRightTransform;
    [SerializeField] Transform bottomLeftTransform;
    [SerializeField] Transform bottomRightTransform;
    [SerializeField] DriverMode mode = DriverMode.Manual;
    private TargetEnum nextTarget = TargetEnum.TopLeft; // Gán trạng thái
    private Transform currentTarget;



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
        else if (mode == DriverMode.Manual)
            ManualMode();
    }
    private void ManualMode()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3 (0f, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);
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
