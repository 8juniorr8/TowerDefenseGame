using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool GameIsOver;

    public GameObject gameOverUI;

    void Start ()
    {
        GameIsOver = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (GameIsOver)
            return;



        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame ()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);

    }

    public void Surrender()
    {
        EndGame();
    }

}
