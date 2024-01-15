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
    Games _game;        //UI���ǂ̃Q�[���ɑΉ����Ă��邩������

    [SerializeField]
    Image _uiImage;             //UI�S�̂̉摜

    [SerializeField]
    Sprite _uiUnlockedSprite;   //UI���A�����b�N����Ă��鎞�ɕ\������摜

    [SerializeField]
    Sprite _uiLockedSprite;     //UI�����b�N����Ă��鎞�ɕ\������摜

    [SerializeField]
    GameDetailUI _gameDetailUI; //�Q�[���̏ڍׂ�����������ꂽUI

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
