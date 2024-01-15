using UnityEngine;
using UnityEngine.UI;

public class GameDetailUI : BaseUI
{
    [SerializeField]
    Text _text;         //UI上でゲームの詳細や説明を表示する箇所

    [SerializeField]
    string _detailText; //ゲームの詳細や説明文

    protected override void Awake()
    {
        base.Awake();

        //設定したテキストを表示させる
        _text.text = _detailText;
    }
}
