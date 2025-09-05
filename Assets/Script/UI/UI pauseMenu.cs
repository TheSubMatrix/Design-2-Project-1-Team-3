using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIpauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pausedMenu;
    public static bool isPaused;
    public void TransitionToScene(string sceneName, float transitionTime = 0.5f)
    {
        SceneTransitionManager.Instance.TransitionToScene("Level 1", 0.5f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausedMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) 
            {
                
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                Paused();
            }
            else
            {

                Resume();
            }
        }
    }
    private void Paused()
    {
        //SceneTransitionManager.Instance.TransitionToScene("Isaiah's Test Scene", 0.5f);
        pausedMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Cursor.visible = false;
        pausedMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
    }
    public void Title()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }
    public void Quit()
    {
        Debug.Log("you have quit the game");
        Application.Quit();
    }
}
