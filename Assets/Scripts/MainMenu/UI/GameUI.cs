using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Games
{
    Janken
}

public class GameUI : BaseUI
{
    [SerializeField]
    Games _game;        //UIがどのゲームに対応しているかを示す

    [SerializeField]
    Image _uiImage;             //UI全体の画像

    [SerializeField]
    Sprite _uiUnlockedSprite;   //UIがアンロックされている時に表示する画像

    [SerializeField]
    Sprite _uiLockedSprite;     //UIがロックされている時に表示する画像

    [SerializeField]
    GameDetailUI _gameDetailUI; //ゲームの詳細や説明が書かれたUI

    protected override void Awake()
    {
        base.Awake();
        _uiState = UIState.Active;
    }

    public void Unlock()
    {
        ButtonUtil.EnableButton(_buttons);
        _uiImage.sprite = _uiUnlockedSprite;
    }

    public void Lock()
    {
        ButtonUtil.DisableButton(_buttons);
        _uiImage.sprite = _uiLockedSprite;
    }

    public void ShowDetails()
    {
        _gameDetailUI.Open();
    }

    public void PlayGame()
    {
        GameStateManager.Instance.ChangeState(_game);
    }
}
