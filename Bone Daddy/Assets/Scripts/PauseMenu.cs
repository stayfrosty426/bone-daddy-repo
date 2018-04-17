using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;
    private bool paused = false;

     void Start()
    {
        PauseUI.SetActive(false); //when the game starts, the Pause UI will be disabled
    }

     void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0; //time stops or nothing happens in the game
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1; //normal gameplay/resumes the time
        }
    }


    public void Resume()
    {
        paused = false;// set up in inspect to be "on click" so when you click the button or image you upload, it will resume the game/make pause false
    }

    public void Restart()
    {
        //loads up the current scene from the start, resetting all stats etc.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        //loads up the main menu
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        //only works when you export/build the game
        Application.Quit();
    }
}
