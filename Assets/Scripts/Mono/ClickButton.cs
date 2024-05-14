using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{


    public void StarGame()
    {
        StaticValue.Index = 1;
        SceneManager.LoadScene("MainScene");
    }
}
