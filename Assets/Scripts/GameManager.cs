using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private float timeToEnd;
    public int points = 0;
    public int redKey = 0;
    public int greenKey = 0;
    public int blueKey = 0;
    bool win;
    bool gamePaused;
    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip pickupClip;

    public SoundtrackScript soundtrack;

    private void Start()
    {
        if (gameManager == null) 
        { 
            gameManager = this; 
        }
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stoper", 1f, 1f);
        gamePaused = false;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
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
        soundtrack.OnPauseGame();
        PlayClip(pauseClip);
        Debug.Log("Game Paused");
        gamePaused=true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        soundtrack.OnResumeGame();
        PlayClip(resumeClip);
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

    public void AddPoints(int p)
    {
        points += p;
    }

    public void AddTime(int t)
    {
        timeToEnd += t;
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Red) redKey++;
        else if (color == KeyColor.Green) greenKey++;
        else if (color == KeyColor.Blue) blueKey++;
    }

    public void FreezeTime(int time)
    {
        CancelInvoke(nameof(Stoper));
        InvokeRepeating(nameof(Stoper), time, 1f);
    }
}
