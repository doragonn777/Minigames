using System.Collections.Generic;

namespace JankenGame
{
    public class JankenSystem
    {
        JankenInfo.MatchResult[] _judgeResult =
        {                                       //�O�[�@�`���L�@�p�[
            JankenInfo.MatchResult.Draw,        //0     0       0   (�N���蓾�Ȃ�)
            JankenInfo.MatchResult.Draw,        //0     0       1
            JankenInfo.MatchResult.Draw,        //0     1       0
            JankenInfo.MatchResult.ScissorsWin, //0     1       1
            JankenInfo.MatchResult.Draw,        //1     0       0
            JankenInfo.MatchResult.PaperWin,    //1     0       1
            JankenInfo.MatchResult.RockWin,     //1     1       0
            JankenInfo.MatchResult.Draw         //1     1       1
        };

        List<IJankenPlayer> _participants;

        public JankenSystem()
        {
            _participants = new List<IJankenPlayer>();
        }

        public void AddPlayer(IJankenPlayer p)
        {
            _participants.Add(p);
        }

        public void ResetPlayer()
        {
            _participants.Clear();
        }

        public JankenInfo Judge()
        {
            var participants = new Dictionary<IJankenPlayer, JankenHand>();

            foreach (var p in _participants)
            {
                participants.Add(p, p.GetHand());
            }

            //�r�b�g���Z�Ńt���O�Ǘ�������
            int showedHands = 0;
            if (participants.ContainsValue(JankenHand.Rock)) showedHands += 4;
            if (participants.ContainsValue(JankenHand.Scissors)) showedHands += 2;
            if (participants.ContainsValue(JankenHand.Paper)) showedHands += 1;

            return new JankenInfo(participants, _judgeResult[showedHands]);

        }
    }
}
