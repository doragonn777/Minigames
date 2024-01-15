
public class ResultUI : BaseUI
{
    //プレイヤーが勝利したかどうか（勝敗に応じてUIの表示内容を変更する）
    public bool _doesPlayerWin;

    public enum PushedButton
    {
        ReplayButton,
        QuitButton,
        Null
    }

    //UIの所持者が押されたボタンを識別するために使用
    PushedButton _pushedButton = PushedButton.Null;
    public PushedButton pushedButton => _pushedButton;

    public void Replay()
    {
        _pushedButton = PushedButton.ReplayButton;
        Close();
    }

    public void Quit()
    {
        _pushedButton = PushedButton.QuitButton;
        Close();
    }
}
