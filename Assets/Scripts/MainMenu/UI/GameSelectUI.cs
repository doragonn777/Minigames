using UnityEngine;

public class GameSelectUI : BaseUI
{
    [SerializeField]
    GameUI[] _gameUI;   //�V�Ԃ��Ƃ̂ł���Q�[����\������UI�i���͂���񂯂�̂݁j

    protected override void Awake()
    {
        base.Awake();

        //�����_�ł͑S�Ă�GameUI�𖳏����ŉ������
        foreach (GameUI ui in _gameUI)
        {
            ui.Unlock();
        }
    }

    public void BackToTitle()
    {
        GameStateManager.Instance.ChangeState(GameState.Title);
    }
}
