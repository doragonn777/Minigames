
namespace JankenGame
{
    public interface IJankenPlayer
    {
        //����񂯂�ŏo������������ɕۑ�����
        public JankenHand PlayedHand { get; set; }

        /// <summary>
        /// ����񂯂�ŏo��������肷��B�o������͊�{PlayedHand�ɕۑ�����B
        /// </summary>
        public JankenHand GetHand();
    }
}

