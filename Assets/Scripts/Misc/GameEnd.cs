using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{

    public Text text;

    void OnTriggerEnter()
    {
        text.enabled = true;
    }

    void OnTriggerExit()
    {
        text.enabled = false;
    }
}
