
public class ResultUI : BaseUI
{
    //�v���C���[�������������ǂ����i���s�ɉ�����UI�̕\�����e��ύX����j
    public bool _doesPlayerWin;

    public enum PushedButton
    {
        ReplayButton,
        QuitButton,
        Null
    }

    //UI�̏����҂������ꂽ�{�^�������ʂ��邽�߂Ɏg�p
    PushedButton _pushedButton = PushedButton.Null;
    public PushedButton pushedButton => _pushedButton;

    public void Replay()
    {
        _pushedButton = PushedButton.ReplayButton;
        Close();
    }

    public void Quit()
    {
        _pushedButton = PushedButton.QuitButton;
        Close();
    }
}
