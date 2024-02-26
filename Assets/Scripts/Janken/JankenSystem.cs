using System.Collections.Generic;

namespace JankenGame
{
    public class JankenSystem
    {
        JankenResult[] _judgeResult =
        {                                       //�O�[�@�`���L�@�p�[
            JankenResult.Draw,                  //0     0       0   (�N���蓾�Ȃ�)
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

        //����񂯂�Q���҂�o�^
        public void AddPlayer(IJankenPlayer p)
        {
            _participants.Add(p);
        }

        //����񂯂�Q���҂����Z�b�g
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

            //�r�b�g���Z�Ńt���O�Ǘ�������
            int showedHands = 0;
            if (hands.Contains(JankenHand.Rock))        showedHands += 4;
            if (hands.Contains(JankenHand.Scissors))    showedHands += 2;
            if (hands.Contains(JankenHand.Paper))       showedHands += 1;

            return _judgeResult[showedHands];

        }
    }

    public enum JankenResult
    {
        RockWin,        //�O�[�̏���
        ScissorsWin,    //�`���L�̏���
        PaperWin,       //�p�[�̏���
        Draw            //������
    }
}
