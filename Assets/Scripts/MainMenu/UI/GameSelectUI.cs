using UnityEngine;

public class GameSelectUI : BaseUI
{
    [SerializeField]
    GameUI[] _gameUI;   //遊ぶことのできるゲームを表示するUI（今はじゃんけんのみ）

    protected override void Awake()
    {
        base.Awake();

        //現時点では全てのGameUIを無条件で解放する
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
