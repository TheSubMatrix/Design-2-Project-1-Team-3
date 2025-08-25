using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UiMenu : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject creditsButton;
    public GameObject playButton;
    public GameObject helpButton;
    public GameObject titleButton;
    public GameObject panel;
    public GameObject Character;
    public TMP_Text title;
    //public TMP_Text gameOver;
    public TMP_Text howToPlay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Character.SetActive(false);
        titleButton.SetActive(false);
        panel.SetActive(true);
        creditsButton.SetActive(true);
        playButton.SetActive(true);
        helpButton.SetActive(true);
        //gameOver.enabled = false;
        title.enabled = true;
        howToPlay.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        panel.SetActive(false);
        //Character.SetActive(true);
        titleButton.SetActive(false);
        panel.SetActive(false);
        creditsButton.SetActive(false);
        playButton.SetActive(false);
        helpButton.SetActive(false);
        title.enabled = false;
        howToPlay.enabled = false;
        //goes to new scene
    }
    public void Help()
    {
        panel.SetActive(false);
        //Character.SetActive(true);
        titleButton.SetActive(true);
        panel.SetActive(false);
        creditsButton.SetActive(false);
        playButton.SetActive(false);
        helpButton.SetActive(false);
        title.enabled = false;
        howToPlay.enabled = false;
        //teleports player in the help room
        //transform.position = new Vector3(8.1f, 1.18f, 102.02f);
    }
    public void Credits()
    {
        //teleports player in the credits room
        //transform.position = new Vector3(-154.3f, 1.18f, -76f);
        panel.SetActive(false);
        //Character.SetActive(true);
        titleButton.SetActive(true);
        panel.SetActive(false);
        creditsButton.SetActive(false);
        playButton.SetActive(false);
        helpButton.SetActive(false);
        title.enabled = false;
        howToPlay.enabled = false;
    }
    public void Title()
    {
        //teleports player in the credits room
        //transform.position = new Vector3(-154.3f, 1.18f, -76f);
        //Character.SetActive(false);
        titleButton.SetActive(false);
        panel.SetActive(true);
        creditsButton.SetActive(true);
        playButton.SetActive(true);
        helpButton.SetActive(true);
        title.enabled = false;
        howToPlay.enabled = false;
    }
}
