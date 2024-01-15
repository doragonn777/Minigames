
namespace JankenGame
{
    public class HandSelectUI : BaseUI
    {
        //UI�őI�����ꂽ���ۑ�����
        JankenHand _selectedHand;
        public JankenHand SelectedHand => _selectedHand;

        public void SelectRock()
        {
            _selectedHand = JankenHand.Rock;
            Close();
        }

        public void SelectScissors()
        {
            _selectedHand = JankenHand.Scissors;
            Close();
        }

        public void SelectPaper()
        {
            _selectedHand = JankenHand.Paper;
            Close();
        }
    }

}