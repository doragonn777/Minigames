using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,      //タイトル画面
    GameSelect, //ゲーム選択画面
    InGame,     //インゲーム
    ExitGame    //ゲーム終了
}

public class GameStateManager : MonoBehaviour
{

    static GameStateManager _instance;
    public static GameStateManager Instance => _instance;

    SceneLoader _sceneLoader;

    //現在の状態
    GameState _currentState;
    GameState CurrentState { get { return _currentState; } }

    public Action OnStateChanged;    //ゲームの状態が変更された時に呼び出される

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _sceneLoader = new SceneLoader();

            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState state)
    {
        _currentState = state;
        OnStateChanged?.Invoke();

        _sceneLoader.LoadScene(state);
    }

    public void ChangeState(Games game)
    {
        _currentState = GameState.InGame;
        OnStateChanged?.Invoke();

        _sceneLoader.LoadScene(game);
    }
}
