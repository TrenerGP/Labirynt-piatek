using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] private float timeToEnd;
    public int points = 0;
    public int redKey = 0;
    public int greenKey = 0;
    public int blueKey = 0;
    public bool win;
    bool gamePaused;
    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip pickupClip;

    public SoundtrackScript soundtrack;

    // PickUpPanel
    public Text timeText;
    public Text crystalText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text blueKeyText;
    public Image snowFlake;

    // InfoPanel
    public GameObject infoPanel;
    public Text infoText;
    public Text reloadText;

    // GamePanel
    public Text inGameText;

    private void Start()
    {
        if (gameManager == null) 
        { 
            gameManager = this; 
        }
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stoper", 1f, 1f);
        gamePaused = false;
        snowFlake.enabled = false;
        infoPanel.SetActive(false);
        timeText.text = timeToEnd.ToString();
        crystalText.text = points.ToString();
        redKeyText.text = redKey.ToString();
        greenKeyText.text = greenKey.ToString();
        blueKeyText.text = blueKey.ToString();
        inGameText.text = "";
        reloadText.text = "";
        infoText.text = "Game Paused";
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void Stoper()
    {
        snowFlake.enabled=false;
        timeToEnd--;
        timeText.text = timeToEnd.ToString();
        Debug.Log("Time: "+timeToEnd + " s");
        if (timeToEnd <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        CancelInvoke("Stoper");
        infoPanel.SetActive(true);
        if (win)
        {
            infoText.text = "Brawo wygra³eœ!!!";
            reloadText.text = "Reload?";
            Debug.Log("Brawo wygra³eœ!!! Reload?");
        }
        else
        {
            infoText.text = "Koniec czasu!!!";
            reloadText.text = "Reload?";
            Debug.Log("Koniec czasu!!! Reload?");
        }
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        soundtrack.OnPauseGame();
        PlayClip(pauseClip);
        Debug.Log("Game Paused");
        infoPanel.SetActive(true);
        gamePaused=true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        soundtrack.OnResumeGame();
        PlayClip(resumeClip);
        Debug.Log("Game Resumed");
        infoPanel.SetActive(false);
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
        crystalText.text = points.ToString();
    }

    public void AddTime(int t)
    {
        timeToEnd += t;
        timeText.text = timeToEnd.ToString();
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Red) redKey++;
        else if (color == KeyColor.Green) greenKey++;
        else if (color == KeyColor.Blue) blueKey++;

        redKeyText.text = redKey.ToString();
        greenKeyText.text = greenKey.ToString();
        blueKeyText.text = blueKey.ToString();
    }

    public void FreezeTime(int time)
    {
        CancelInvoke(nameof(Stoper));
        snowFlake.enabled = true;
        InvokeRepeating(nameof(Stoper), time, 1f);
    }
}
