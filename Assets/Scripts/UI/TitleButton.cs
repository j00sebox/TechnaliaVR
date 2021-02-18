using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    Button titleButton;

    // Start is called before the first frame update
    void Start()
    {
        titleButton = GetComponent<Button> ();
        titleButton.onClick.AddListener(ReturnToTitle);
    }

    void ReturnToTitle()
    {
        // make sure the game is not paused when going back
        PauseManager.paused = false;
        
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
