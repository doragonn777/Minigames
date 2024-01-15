using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,      //�^�C�g�����
    GameSelect, //�Q�[���I�����
    InGame,     //�C���Q�[��
    ExitGame    //�Q�[���I��
}

public class GameStateManager : MonoBehaviour
{

    static GameStateManager _instance;
    public static GameStateManager Instance => _instance;

    SceneLoader _sceneLoader;

    //���݂̏��
    GameState _currentState;
    GameState CurrentState { get { return _currentState; } }

    public Action OnStateChanged;    //�Q�[���̏�Ԃ��ύX���ꂽ���ɌĂяo�����

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
