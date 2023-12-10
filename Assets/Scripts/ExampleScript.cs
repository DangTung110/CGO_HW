using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public int mySpeed = 5;
    //public int myPubic = 10;
    //public string myString;
    //public float myFloat = 5.0f;

    /*
    public BoxCollider myCollider;
    public Camera myCamera;
    public Light myLight;
    public Transform transform;
    */

    public GameObject cube2;
    private MeshRenderer meshRenderer;

    #region UNITY_FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {
        // PrintValue();

        cube2 = GameObject.Find("New Cube");

        meshRenderer = cube2.GetComponent<MeshRenderer>();

        ChangeColor();
    }
    // Update is called once per frame
    void Update()
    {
        RotateObject();
        MoveObject();
    }
    #endregion
    void PrintValue()
    {
        Debug.Log("My Int: " + mySpeed);
        //Debug.Log("My String: " + myString);
    }


    void RotateObject()
    {
        cube2.transform.Rotate(Vector3.up * mySpeed * Time.deltaTime);
    }

    void MoveObject()
    {
        cube2.transform.position = new Vector3(cube2.transform.position.x + mySpeed * Time.deltaTime, 0f, 0f);
    }

    void ChangeColor()
    {
        meshRenderer.material.color = new Color32(255, 0, 0, 255);
    }
}
