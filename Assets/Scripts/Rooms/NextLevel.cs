using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel Instance;
    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        _MainMenu,
        Plot,
        Level01,
        Level02,
        Credits,

    }
    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene._MainMenu.ToString());
    }
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNextLevel(Scene scene)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   if(collision.tag=="Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
