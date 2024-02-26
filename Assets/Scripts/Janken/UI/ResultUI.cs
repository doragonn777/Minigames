namespace JankenGame
{

    public class ResultUI : BaseUI
    {
        //プレイヤーが勝利したかどうか（勝敗に応じてUIの表示内容を変更する）
        public bool _doesPlayerWin;

        public void Replay()
        {
            JankenManager.Instance.OnReplay();
        }

        public void Quit()
        {
            JankenManager.Instance.OnQuit();
        }
    }

}