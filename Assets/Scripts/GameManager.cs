using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject backgroundPrefab;
    [SerializeField]
    private GameObject sceneLoaderPrefab;

    private Background background;
    private SceneLoader sceneLoader;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private Background Background
    {
        get
        {
            // Background를 hierarchy에서 찾습니다.
            if (background == null)
            {
                background = GameObject.FindGameObjectWithTag("Background")?.GetComponent<Background>();
            }

            // hierarchy에 존재하지 않는다면 새로 만듭니다.
            if (background == null)
            {
                background = Instantiate<GameObject>(backgroundPrefab)?.GetComponent<Background>();
            }

            return background;
        }
    }

    private void Awake()
    {
        InitGameManager();
    }

    private void InitGameManager()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveScene(string sceneName)
    {
        if (sceneLoader == null)
        {
            sceneLoader = Instantiate<GameObject>(sceneLoaderPrefab).GetComponent<SceneLoader>();
        }
        sceneLoader.LoadSceneWithFadeIn(LoadSceneMode.Single, sceneName);
    }

    public void ChangeBackground(string backgroundName)
    {
        Background.ChangeBackground(backgroundName);
    }
}
