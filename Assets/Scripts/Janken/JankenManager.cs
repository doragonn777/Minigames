using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace JankenGame
{
    public enum JankenState
    {
        Pregame,            //�Q�[���J�n�O
        HandSelect,         //�v���C���[���o�����I�����Ă�����
        HandSelectAgain,    //�������ōēx���I�����Ă�����
        PlayJanken,         //����񂯂�����鉉�o��
        JankenEnded,        //����񂯂�̉��o���I���������
        Result              //����񂯂�̌������������
    }

    public class JankenManager : MonoBehaviour
    {
        public static JankenManager Instance { get; private set; }

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
        
        MainPlayer _mainPlayer;      //�v���C���[���I���������ۑ�����N���X
        
        //�o�����I������UI�����I���������o�^���邽�߂̃v���p�e�B
        public MainPlayer MainPlayer { get { return _mainPlayer; } }

        [SerializeField]
        Character _npc;             //NPC�ɑΉ�����L�����N�^�[

        JankenState _currentState;  //���݂̏��

        JankenSystem _system;

        public Action<JankenState> OnStateChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _system = new JankenSystem();
        }

        private void Start()
        {
            UIManager.Instance.OnPregameUIClosed += OnHandSelect;
            UIManager.Instance.OnHandSelectUIClosed += OnPlayJanken;
            Init();
        }

        //�Q�[���̏�Ԃ�ύX���A�����ʒm����
        private void ChangeState(JankenState state)
        {
            _currentState = state;
            OnStateChanged?.Invoke(state);
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void Init()
        {
            ChangeState(JankenState.Pregame);

            _jankenText.text = TEXT_PREGAME;

            //�}�l�[�W�����Ǘ����邶��񂯂�Q���҂����Z�b�g
            CharacterManager.Instance.ResetCharacter();

            //JankenSystem�̓����ŊǗ����邶��񂯂�Q���҂����Z�b�g
            _system.ResetPlayer();

            //��ʂɕ\������Ă���L�����N�^�[�̏�Ԃ�����������
            _mainPlayerCharacter.Init();
            _npc.Init();

            //�����_�ł́A����񂯂�ɎQ������̂̓v���C���[��NPC1�̂̂�
            //�i����A���I�ɐl����ύX�ł���悤�ɂ���j
            _mainPlayer = new MainPlayer(); //�v���C���[�����삷��Ώ�
            _mainPlayerCharacter.Player = _mainPlayer;
            CharacterManager.Instance.AddCharacter(_mainPlayerCharacter);

            _npc.Player = new RandomAI();   //�����_���Ɏ���o��NPC
            CharacterManager.Instance.AddCharacter(_npc);
        }

        /// <summary>
        /// HandSelectUI��\�����A�o�����I�΂���
        /// </summary>
        private void OnHandSelect()
        {
            _jankenText.text = TEXT_SELECTHAND;

            if (_currentState == JankenState.Pregame)
            {
                _currentState = JankenState.HandSelect;

                //����񂯂�̎Q���҂�JankenSystem�ɓo�^
                foreach (var c in CharacterManager.Instance.Characters)
                {
                    _system.AddPlayer(c.Player);
                }
            } 
            else
            {
                _currentState = JankenState.HandSelectAgain;
            }


            //��ʏ�̉��o
            foreach (var c in CharacterManager.Instance.Characters)
            {
                c.Thinking();
            }

            ChangeState(JankenState.HandSelect);
        }

        /// <summary>
        /// HandSelectUI���������ɌĂяo�����
        /// </summary>
        private void OnPlayJanken()
        {
            ChangeState(JankenState.PlayJanken);

            _jankenText.text = TEXT_HANDSELECTED;

            _currentState = JankenState.PlayJanken;

            //����񂯂�̌��ʂ��擾
            JankenResult result = _system.Judge();

            //��ʏ�̃L�����N�^�[�Ɏ������o������ɑΉ��������o������
            foreach (var c in CharacterManager.Instance.Characters)
            {
                c.ShowHand();
            }

            //��ʏ�̉��o��҂��A����񂯂�̌��ʂɉ���������������
            StartCoroutine(WaitJankenAnim(result));
        }

        /// <summary>
        /// ����񂯂񂪈��������ɂȂ������ɌĂяo�����
        /// </summary>
        private void OnDraw()
        {
            //TODO: �u�������Łc�v�I�ȕ�����\��������
            OnHandSelect();
        }

        /// <summary>
        /// ����񂯂�̌����������Ƃ��ɌĂяo�����
        /// </summary>
        private void OnResult(JankenResult result)
        {
            //���������̏�ԂŃ��U���g�͕\������Ȃ�
            Assert.IsTrue(result != JankenResult.Draw);

            ChangeState(JankenState.Result);

            //�O�[�`���L�p�[�̕��т���v���Ă���̂Ŗ����I�ɕϊ�����
            JankenHand winnerHand = (JankenHand) result;

            //�v���C���[�̏��s�ɉ����ĕ\������e�L�X�g��ύX
            if (_mainPlayer.PlayedHand == winnerHand)
            {
                _jankenText.text = TEXT_RESULT_WIN;
            }
            else
            {
                _jankenText.text = TEXT_RESULT_LOSE;
            }

            foreach (var c in CharacterManager.Instance.Characters)
            {
                //����񂯂�̌��ʂ���Ap�Ɉ�v����Q���҂��o��������擾
                if (c.Player.PlayedHand == winnerHand)
                {
                    c.Happy();
                } 
                else
                {
                    c.Sad();
                }
            }
        }

        //���v���C�{�^�����������ꂽ���ɌĂяo��
        public void OnReplay()
        {
            GameStateManager.Instance.ChangeState(Games.Janken);
        }

        //�Q�[���I���{�^�����������ꂽ���ɌĂяo��
        public void OnQuit()
        {
            GameStateManager.Instance.ChangeState(GameState.GameSelect);
        }

        //�w����񂯂�c�x�̕����̉��o�̑҂����ԁi�b�j
        const int JANKEN_ANIM_WAIT_SEC = 1;
        //�w�ۂ�I�x�̕����̉��o�̑҂����ԁi�b�j
        const int PON_ANIM_WAIT_SEC = 1;

        //�L�����N�^�[�́w����񂯂�ۂ�x�Ƃ������o��҂��A���s�ɉ���������������
        private IEnumerator WaitJankenAnim(JankenResult result)
        {
            yield return new WaitForSeconds(JANKEN_ANIM_WAIT_SEC);

            _jankenText.text = TEXT_SHOWEDHAND;

            //����񂯂񂷂鉉�o�̊�����҂�
            yield return new WaitForSeconds(PON_ANIM_WAIT_SEC);

            ChangeState(JankenState.JankenEnded);

            //���s�i���������j�ɉ����ď�����ύX����
            if (result == JankenResult.Draw)
            {
                OnDraw();
            }
            else
            {
                OnResult(result);
            }
        }
    }

}