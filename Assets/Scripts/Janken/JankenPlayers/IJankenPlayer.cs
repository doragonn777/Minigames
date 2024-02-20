
namespace JankenGame
{
    public interface IJankenPlayer
    {
        //じゃんけんで出した手をここに保存する
        public JankenHand PlayedHand { get; set; }

        /// <summary>
        /// じゃんけんで出す手を決定する。出した手は基本PlayedHandに保存する。
        /// </summary>
        public JankenHand GetHand();
    }
}

