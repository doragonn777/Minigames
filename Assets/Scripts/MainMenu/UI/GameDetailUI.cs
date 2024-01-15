using UnityEngine;
using UnityEngine.UI;

public class GameDetailUI : BaseUI
{
    [SerializeField]
    Text _text;         //UI��ŃQ�[���̏ڍׂ������\������ӏ�

    [SerializeField]
    string _detailText; //�Q�[���̏ڍׂ������

    protected override void Awake()
    {
        base.Awake();

        //�ݒ肵���e�L�X�g��\��������
        _text.text = _detailText;
    }
}
