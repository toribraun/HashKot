using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    public GameObject RulesMenu;
    [SerializeField] 
    public GameObject RulesMenuBeforeGame;
    [SerializeField] 
    public GameObject PauseMenu;

    [SerializeField] 
    public AudioSource click;

    public void StartGame(string levelScene)
    {
        click.Play();
        Time.timeScale = 1F;
        GameStates.CurrentLevel = levelScene;
        SceneManager.LoadScene(levelScene);
    }
    
    public void RestartGame()
    {
        click.Play();
        Time.timeScale = 1F;
        SceneManager.LoadScene(GameStates.CurrentLevel);
    }
    
    public void LoadLevelsMenu()
    {
        click.Play();
        if (GameStates.IsFirstPlay)
        {
            GameStates.IsFirstPlay = false;
            RulesMenuBeforeGame.SetActive(true);
        }
        else
            SceneManager.LoadScene("LevelsMenu");
    }
    
    public void LoadMainMenu()
    {
        click.Play();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadRulesMenu()
    {
        click.Play();
        RulesMenu.SetActive(true);
    }
    
    public void CloseRulesMenu()
    {
        click.Play();
        RulesMenu.SetActive(false);
    }
    
    public void LoadRulesMenuFromPause()
    {
        click.Play();
        LoadRulesMenu();
        PauseMenu.SetActive(false);
    }
    
    public void CloseRulesMenuFromPause()
    {
        click.Play();
        CloseRulesMenu();
        PauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        click.Play();
        Application.Quit();
    }
}
