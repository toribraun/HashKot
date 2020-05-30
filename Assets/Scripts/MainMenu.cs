using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    public GameObject RulesMenu;
    [SerializeField] 
    public GameObject PauseMenu;
    
    public void StartGame(string levelScene)
    {
        Time.timeScale = 1F;
        SceneManager.LoadScene(levelScene);
    }
    
    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadRulesMenu()
    {
        RulesMenu.SetActive(true);
    }
    
    public void CloseRulesMenu()
    {
        RulesMenu.SetActive(false);
    }
    
    public void LoadRulesMenuFromPause()
    {
        LoadRulesMenu();
        PauseMenu.SetActive(false);
    }
    
    public void CloseRulesMenuFromPause()
    {
        CloseRulesMenu();
        PauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
