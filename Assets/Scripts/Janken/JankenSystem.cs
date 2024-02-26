using System.Collections.Generic;

namespace JankenGame
{
    public class JankenSystem
    {
        JankenResult[] _judgeResult =
        {                                       //グー　チョキ　パー
            JankenResult.Draw,                  //0     0       0   (起こり得ない)
            JankenResult.Draw,                  //0     0       1
            JankenResult.Draw,                  //0     1       0
            JankenResult.ScissorsWin,           //0     1       1
            JankenResult.Draw,                  //1     0       0
            JankenResult.PaperWin,              //1     0       1
            JankenResult.RockWin,               //1     1       0
            JankenResult.Draw                   //1     1       1
        };

        List<IJankenPlayer> _participants;

        public JankenSystem()
        {
            _participants = new List<IJankenPlayer>();
        }

        //じゃんけん参加者を登録
        public void AddPlayer(IJankenPlayer p)
        {
            _participants.Add(p);
        }

        //じゃんけん参加者をリセット
        public void ResetPlayer()
        {
            _participants.Clear();
        }

        public JankenResult Judge()
        {
            var hands = new List<JankenHand>();

            foreach (var p in _participants)
            {
                hands.Add(p.GetHand());
            }

            //ビット演算でフラグ管理をする
            int showedHands = 0;
            if (hands.Contains(JankenHand.Rock))        showedHands += 4;
            if (hands.Contains(JankenHand.Scissors))    showedHands += 2;
            if (hands.Contains(JankenHand.Paper))       showedHands += 1;

            return _judgeResult[showedHands];

        }
    }

    public enum JankenResult
    {
        RockWin,        //グーの勝ち
        ScissorsWin,    //チョキの勝ち
        PaperWin,       //パーの勝ち
        Draw            //あいこ
    }
}
