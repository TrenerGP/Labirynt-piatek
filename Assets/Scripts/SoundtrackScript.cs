using UnityEngine;

public class SoundtrackScript : MonoBehaviour
{
    AudioSource source;
    public AudioClip[] clips;
    int actualClip = 0;
    double pauseClipTime = 0;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clips[actualClip];
        source.Play();
    }

    private void Update()
    {
        if(source.time >= clips[actualClip].length)
        {
            actualClip++;
            actualClip%=clips.Length;
            source.clip = clips[actualClip];
            source.Play();
        }
    }

    public void OnPauseGame()
    {
        pauseClipTime = source.time;
        source.Stop();
    }

    public void OnResumeGame()
    {
        source.PlayScheduled(pauseClipTime);
        pauseClipTime = 0;
    }



}
