using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private GameObject pauseScreen;
private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }
  
    #region GameOver
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    
    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        PauseGame(false);
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying= false;
        #endif
    }
    #endregion
    #region Pause
    
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        Time.timeScale = System.Convert.ToInt32(!status);
    }
    public void SoundValue()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicValue()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    
}

