using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EnumTarget
{
    TopLeft,
    TopRight,
    BottonLeft,
    BottonRight
}
public class EnumExample : MonoBehaviour
{
    public GameObject TopLeftPosition;
    public GameObject TopRightPosition;
    public GameObject BottonLeftPosition;
    public GameObject BottonRightPosition;
    private GameObject currentTarget;
    private EnumTarget currentFlag;
    public float distance;

    private void Awake()
    {
        TopLeftPosition = GameObject.Find("TopLeft");
        TopRightPosition = GameObject.Find("TopRight");
        BottonLeftPosition = GameObject.Find("BottonLeft");
        BottonRightPosition = GameObject.Find("BottonRight");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentFlag = EnumTarget.TopLeft;
        currentTarget = TopLeftPosition;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = currentTarget.transform.position - transform.position;
        distance = moveDirection.magnitude;
        if (distance <= 0f)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = targetRotation;
            SetNextTarget(currentFlag);
        }
    }
    private void SetNextTarget(EnumTarget flag) // TopLeft -> TopRight -> BottonRight -> BottonLeft -> TopLeft
    {
        switch (flag)
        {
            case EnumTarget.TopLeft:
                currentTarget = TopLeftPosition;
                flag = EnumTarget.TopRight;
                break;
            case EnumTarget.TopRight:
                currentTarget = TopRightPosition;
                flag = EnumTarget.BottonRight;
                break;
            case EnumTarget.BottonRight:
                currentTarget = BottonRightPosition;
                flag = EnumTarget.BottonLeft;
                break;
            case EnumTarget.BottonLeft:
                currentTarget = BottonLeftPosition;
                flag = EnumTarget.TopLeft;
                break;
        }
    }
}
