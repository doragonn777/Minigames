
namespace JankenGame
{
    public class HandSelectUI : BaseUI
    {
        //UIで選択された手を保存する
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

        public override void Close()
        {
            JankenManager.Instance.MainPlayer.PlayedHand = _selectedHand;
            base.Close();
        }
    }

}