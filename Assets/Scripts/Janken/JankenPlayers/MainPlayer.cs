
namespace JankenGame
{
    /// <summary>
    /// �Q�[���̃v���C���[�i���p�ҁj���g�p����
    /// </summary>
    public class MainPlayer : IJankenPlayer
    {
        //UI���őI�����ꂽ��������ɕۑ�����
        public JankenHand selectedHand;

        public MainPlayer()
        {

        }

        public JankenHand GetHand()
        {
            return selectedHand;
        }
    }
}

