using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private float timeToEnd;
    bool win;
    bool gamePaused;

    private void Start()
    {
        if (gameManager == null) 
        { 
            gameManager = this; 
        }
        InvokeRepeating("Stoper", 1f, 1f);
        gamePaused = false;
    }

    void Stoper()
    {
        timeToEnd--;
        Debug.Log("Time: "+timeToEnd + " s");
        if (timeToEnd <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        CancelInvoke("Stoper");
        if (win)
        {
            Debug.Log("Brawo wygra³eœ!!! Reload?");
        }
        else
        {
            Debug.Log("Koniec czasu!!! Reload?");
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        gamePaused=true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        gamePaused = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        PauseCheck();
    }

    void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
