using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public Text TimerText;
    [SerializeField] 
    public GameObject PauseMenu;
    [SerializeField]
    private float time = 90F;
    [SerializeField] 
    private HashKot player;
    [SerializeField] 
    public AudioSource click;
    private bool isPaused;
    
    
    public void Start()
    {
        TimerText.text = ConvertTime(time);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&(!isPaused))
        {
            Pause();
        }
        if (!isPaused)
        {
            time -= Time.deltaTime;
            if (time > 0)
                TimerText.text = ConvertTime(time);
            else
                player.EndGame();
        }
    }

    public void Pause()
    {
        click.Play();
        PauseMenu.SetActive(true);
        Time.timeScale = 0F;
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        click.Play();
        PauseMenu.SetActive(false);
        Time.timeScale = 1F;
        isPaused = false;
    }

    private string ConvertTime(float time)
    {
        var minutes = Mathf.RoundToInt(time) / 60;
        var seconds = Mathf.RoundToInt(time - minutes * 60);
        var secondsText = seconds < 10 ? $"0{seconds}" : $"{seconds}";
        return $"{minutes}:{secondsText}";
    }
}
