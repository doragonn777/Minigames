namespace JankenGame
{

    public class ResultUI : BaseUI
    {
        //�v���C���[�������������ǂ����i���s�ɉ�����UI�̕\�����e��ύX����j
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