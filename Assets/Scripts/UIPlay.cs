using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class UIPlay : MonoBehaviour
{
    private Button bt;
    float speed = 10.0f;
    Vector3 movement;
    float rotation = 4.0f;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bt = GetComponent<Button>(); 
    }

    public void MoveCar(string value)
    {
        movement = transform.forward * speed;
        if (value == "w")
            rb.AddForce(movement, ForceMode.VelocityChange);
        if (value == "s")
            rb.AddForce(- movement, ForceMode.VelocityChange);
    }

    public void RotationCar(string value)
    {
        Quaternion deltaRotation = new Quaternion();
        if (value == "a")
            deltaRotation = Quaternion.Euler(-Vector3.up * rotation);
        if (value == "d")
            deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

}
