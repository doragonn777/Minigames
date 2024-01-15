using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace JankenGame
{
    public enum JankenState
    {
        PreGame,            //�Q�[���J�n�O
        HandSelect,         //�v���C���[���o�����I�����Ă�����
        ShowingHands,       //����񂯂�����鉉�o��
        HandSelectAgain,    //�������ōēx���I�����Ă�����
        Result              //����񂯂�̌������������
    }

    public class JankenManager : MonoBehaviour
    {
        //�Q�[���̐i�s�ɍ��킹�ĉ�ʏ�ɕ\�������e�L�X�g
        const string TEXT_PREGAME = "";
        const string TEXT_SELECTHAND = "Select your hand!";
        const string TEXT_HANDSELECTED = "Jan Ken ...";
        const string TEXT_SHOWEDHAND = "PON!!";
        const string TEXT_RESULT_WIN = "YOU WIN!!!";
        const string TEXT_RESULT_LOSE = "You Lose...";

        [SerializeField]
        Text _jankenText;           //��ʏ�ɕ\������e�L�X�g

        [SerializeField]
        Character _mainPlayerCharacter;     //�v���C���[�ɑΉ�����L�����N�^�[
        MainPlayer _mainPlayer;             //�v���C���[���I���������ۑ�����N���X

        [SerializeField]
        Character _npc;             //NPC�ɑΉ�����L�����N�^�[

        [SerializeField]
        BaseUI preGameUI;           //�Q�[���J�n�O�ɕ\�������UI

        [SerializeField]
        HandSelectUI handSelectUI;  //�v���C���[���o�����I������UI

        [SerializeField]
        ResultUI resultUI;          //����񂯂�̌��ʂ�\������UI

        JankenState _currentState;  //���݂̏��

        JankenSystem _system;

        //����񂯂�̎Q����(IJankenPlayer)�ƃr���[(Character)��R�Â��Ă���
        Dictionary<IJankenPlayer, Character> _players = new Dictionary<IJankenPlayer, Character>();

        private void Awake()
        {
            preGameUI.OnClosed += SelectHand;
            handSelectUI.OnClosed += OnHandSelected;
            resultUI.OnClosed += OnQuitResult;
            _system = new JankenSystem();
        }

        private void Start()
        {
            Init();
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void Init()
        {
            _jankenText.text = TEXT_PREGAME;

            //�}�l�[�W�����Ǘ����邶��񂯂�Q���҂����Z�b�g
            _players.Clear();

            //JankenSystem�̓����ŊǗ����邶��񂯂�Q���҂����Z�b�g
            _system.ResetPlayer();

            //��ʂɕ\������Ă���L�����N�^�[�̏�Ԃ�����������
            _mainPlayerCharacter.Init();
            _npc.Init();

            //�v���C���[������i���o�����I�ԁj����Ώ�
            _mainPlayer = new MainPlayer();

            //�����_�ł́A����񂯂�ɎQ������̂̓v���C���[��NPC1�̂̂�
            //�i����A���I�ɐl����ύX�ł���悤�ɂ���j
            _players.Add(_mainPlayer, _mainPlayerCharacter);
            _players.Add(new RandomAI(), _npc);

            _currentState = JankenState.PreGame;
            preGameUI.Open();
        }

        /// <summary>
        /// HandSelectUI��\�����A�o�����I�΂���
        /// </summary>
        private void SelectHand()
        {
            _jankenText.text = TEXT_SELECTHAND;

            if (_currentState == JankenState.PreGame)
            {
                _currentState = JankenState.HandSelect;

                //����񂯂�̎Q���҂�JankenSystem�ɓo�^
                foreach (var p in _players)
                {
                    _system.AddPlayer(p.Key);
                }
            } 
            else
            {
                _currentState = JankenState.HandSelectAgain;
            }


            //��ʏ�̉��o
            foreach (var p in _players.Values)
            {
                p.Thinking();
            }

            handSelectUI.Open();
        }

        /// <summary>
        /// HandSelectUI���������ɌĂяo�����
        /// </summary>
        private void OnHandSelected()
        {
            _jankenText.text = TEXT_HANDSELECTED;

            //UI�őI���������o�^
            _mainPlayer.selectedHand = handSelectUI.SelectedHand;

            //����񂯂�̌��ʂ��擾
            JankenInfo info = _system.Judge();

            //��ʏ�̃L�����N�^�[�Ɏ������o������ɑΉ��������o������
            foreach (var p in _players)
            {
                p.Value.ShowHand(info.GetHand(p.Key));
            }

            //��ʏ�̉��o��҂��A����񂯂�̌��ʂɉ���������������
            StartCoroutine(WaitJankenAnim(info));
        }

        /// <summary>
        /// ����񂯂񂪈��������ɂȂ������ɌĂяo�����
        /// </summary>
        private void OnDraw()
        {
            //TODO: �u�������Łc�v�I�ȕ�����\��������
            SelectHand();
        }

        /// <summary>
        /// ����񂯂�̌����������Ƃ��ɌĂяo�����
        /// </summary>
        private void OnResult(JankenInfo info)
        {
            //���������̏�ԂŃ��U���g�͕\������Ȃ�
            Assert.IsTrue(info.Result != JankenInfo.MatchResult.Draw);

            //�O�[�`���L�p�[�̕��т���v���Ă���̂Ŗ����I�ɕϊ�����
            JankenHand winnerHand = (JankenHand) info.Result;

            //�v���C���[�̏��s�ɉ����ĕ\������e�L�X�g��ύX
            if (_mainPlayer.selectedHand == winnerHand)
            {
                _jankenText.text = TEXT_RESULT_WIN;
            }
            else
            {
                _jankenText.text = TEXT_RESULT_LOSE;
            }

            foreach (var p in _players)
            {
                //����񂯂�̌��ʂ���Ap�Ɉ�v����Q���҂��o��������擾
                if (info.GetHand(p.Key) == winnerHand)
                {
                    p.Value.Happy();
                } 
                else
                {
                    p.Value.Sad();
                }
            }

            //TODO: ��L�̉��o����������܂ő҂�

            resultUI.Open();
        }

        /// <summary>
        /// ResultUI���������ɌĂяo�����
        /// </summary>
        private void OnQuitResult()
        {
            if (resultUI.pushedButton == ResultUI.PushedButton.ReplayButton)
            {
                Init();
            }
            else
            {
                QuitGame();
            }
        }

        /// <summary>
        /// ����񂯂���I�����A�Q�[���I����ʂɖ߂�
        /// </summary>
        private void QuitGame()
        {
            GameStateManager.Instance.ChangeState(GameState.GameSelect);
        }

        //�w����񂯂�c�x�̕����̉��o�̑҂����ԁi�b�j
        const int JANKEN_ANIM_WAIT_SEC = 1;
        //�w�ۂ�I�x�̕����̉��o�̑҂����ԁi�b�j
        const int PON_ANIM_WAIT_SEC = 1;

        //�L�����N�^�[�́w����񂯂�ۂ�x�Ƃ������o��҂��A���s�ɉ���������������
        private IEnumerator WaitJankenAnim(JankenInfo info)
        {
            yield return new WaitForSeconds(JANKEN_ANIM_WAIT_SEC);

            _jankenText.text = TEXT_SHOWEDHAND;

            //����񂯂񂷂鉉�o�̊�����҂�
            yield return new WaitForSeconds(PON_ANIM_WAIT_SEC);


            //���s�i���������j�ɉ����ď�����ύX����
            if (info.Result == JankenInfo.MatchResult.Draw)
            {
                OnDraw();
            }
            else
            {
                OnResult(info);
            }
        }
    }

}