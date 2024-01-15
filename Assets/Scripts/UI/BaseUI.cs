using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BaseUI : MonoBehaviour
{
    //アニメーションを再生のために変更するAnimatorのパラメータ名
    const string UI_ANIM_PARAM = "isOpen";

    protected Animator _animator;     //UIの表示・非表示時に使用するアニメーション

    [SerializeField]
    protected Button[] _buttons;      //UIが所持しているボタン

    protected enum UIState
    {
        InActive,   //UIが非表示の状態
        Opening,    //UIを開くアニメーションを再生中
        Active,     //UIが表示中
        Closing     //UIを閉じるアニメーションを再生中
    }

    protected UIState _uiState;       //現在のUIの状態

    public Action OnClosed; //UIが閉じ切った事を知らせるイベント

    protected virtual void Awake()
    {
        _uiState = UIState.InActive;
        _animator = gameObject.GetComponent<Animator>();
    }

    public virtual void Open()
    {
        _uiState = UIState.Opening;

        if (_animator != null)
        {
            PlayAnim(true);
        } else
        {
            OnAnimEnded();
        }
    }

    public virtual void Close()
    {
        _uiState = UIState.Closing;
        ButtonUtil.DisableButton(_buttons);

        if (_animator != null)
        {
            PlayAnim(false);
        }
        else
        {
            OnAnimEnded();
        }
    }

    public virtual void OnAnimEnded()
    {
        if (_uiState == UIState.Opening)
        {
            _uiState = UIState.Active;
            ButtonUtil.EnableButton(_buttons);
        } 
        else if (_uiState == UIState.Closing)
        {
            _uiState = UIState.InActive;
            OnClosed?.Invoke();
        }
    }

    /// <summary>
    /// UIの開閉アニメーションを再生する。引数で開閉どちらかを指定する
    /// </summary>
    /// <param name="isUIOpening"></param>
    private void PlayAnim(bool isUIOpening)
    {
        _animator.SetBool(UI_ANIM_PARAM, isUIOpening);
    }
}
