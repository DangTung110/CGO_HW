using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public Button m_InputField;

    private void Awake()
    {
        m_InputField = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_InputField.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("Button được click!");
    }
}
