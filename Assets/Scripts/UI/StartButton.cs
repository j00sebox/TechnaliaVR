using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponent<Button> ();
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("TechnaliaDemo", LoadSceneMode.Single);
    }
}