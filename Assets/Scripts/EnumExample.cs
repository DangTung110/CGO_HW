using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    Paussed,
    GameOver
}


public class EnumExample : MonoBehaviour
{
    public GameState currentState;

    public Transform pointA;
    public Transform pointB;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.MainMenu;

        float distance = (pointA.position - pointB.position).magnitude;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.MainMenu)
        {
            // Hiển thị UI Menu
        }
        else if (currentState == GameState.InGame)
        {
            // ẩn UI Menu
            // Hiện In Game UI
        }
    }
}
