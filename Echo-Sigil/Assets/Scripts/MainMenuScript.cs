﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Transform canvas;
    public GameObject mapEditor;
    public GameObject gameplayGUIElements;
    public GameObject mainMenuElements;
    public Toggle developerToggle;

    public void Start()
    {
        developerToggle.isOn = SaveSystem.developerMode;
        developerToggle.onValueChanged.AddListener(delegate { SaveSystem.developerMode = developerToggle.isOn; });
    }
    public void NewGame()
    {
        StartGame();
    }

    public void LoadGame()
    {
        Map map = null;
        throw new NotImplementedException();
        StartGame(map);
    }

    private void StartGame(Map map = null)
    {
        mainMenuElements.SetActive(false);
        Instantiate(gameplayGUIElements, canvas);
        if(map == null)
        {
            throw new NotImplementedException();
        }
        LoadMap(map);
    }

    void LoadMap(Map map)
    {
        MapReader.GeneratePhysicalMap(SaveSystem.LoadPallate(Directory.GetParent(map.path).FullName), map);
    }

    public void MapEditor()
    {
        mainMenuElements.SetActive(false);
        Instantiate(mapEditor, canvas);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        developerToggle.onValueChanged.RemoveAllListeners();
    }
}
