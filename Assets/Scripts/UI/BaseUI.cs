using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BaseUI : MonoBehaviour
{
    //�A�j���[�V�������Đ��̂��߂ɕύX����Animator�̃p�����[�^��
    const string UI_ANIM_PARAM = "isOpen";

    protected Animator _animator;     //UI�̕\���E��\�����Ɏg�p����A�j���[�V����

    [SerializeField]
    protected Button[] _buttons;      //UI���������Ă���{�^��

    protected enum UIState
    {
        InActive,   //UI����\���̏��
        Opening,    //UI���J���A�j���[�V�������Đ���
        Active,     //UI���\����
        Closing     //UI�����A�j���[�V�������Đ���
    }

    protected UIState _uiState;       //���݂�UI�̏��

    public Action OnClosed; //UI�����؂�������m�点��C�x���g

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
    /// UI�̊J�A�j���[�V�������Đ�����B�����ŊJ�ǂ��炩���w�肷��
    /// </summary>
    /// <param name="isUIOpening"></param>
    private void PlayAnim(bool isUIOpening)
    {
        _animator.SetBool(UI_ANIM_PARAM, isUIOpening);
    }
}
