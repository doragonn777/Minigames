
namespace JankenGame
{
    /// <summary>
    /// ゲームのプレイヤー（利用者）が使用する
    /// </summary>
    public class MainPlayer : IJankenPlayer
    {
        //UI等で選択された手をここに保存する
        public JankenHand PlayedHand { get; set; }

        public MainPlayer()
        {

        }

        public JankenHand GetHand()
        {
            return PlayedHand;
        }
    }
}

