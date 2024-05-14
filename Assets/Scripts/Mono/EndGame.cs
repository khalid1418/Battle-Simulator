using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject teamBlueText;
    [SerializeField]
    private GameObject teamRedText;
    [SerializeField]
    private GameObject ButtonMainMenu;
    private GameManagerAuthoring gameManagerAuthoring;


    void Start()
    {
      TargetFinderSystem targetFinderSystem=  World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TargetFinderSystem>();
        targetFinderSystem.TeamBlueWin += TargetFinderSystem_TeamBlueWin;
        targetFinderSystem.TeamRedWin += TargetFinderSystem_TeamRedWin;

    }

    private void TargetFinderSystem_TeamRedWin(object sender, EventArgs e)
    {
        teamRedText.SetActive(true);
        ButtonMainMenu.SetActive(true);
    }

    private void TargetFinderSystem_TeamBlueWin(object sender, EventArgs e)
    {
       teamBlueText.SetActive(true);
        ButtonMainMenu.SetActive(true);

    }

    public void GTOMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
