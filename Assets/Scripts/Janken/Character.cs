using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JankenGame
{
    /// <summary>
    /// �Q�[���̐i�s�ɍ��킹�ĉ�ʏ�̃L�����N�^�[�̃A�j���[�V�����𐧌䂷��
    /// </summary>
    public class Character : MonoBehaviour
    {
        //�e�A�j���[�V�����̍Đ��Ɏg�p���镶����
        const string STATE_INIT = "Init";                   //�A�j���[�V������������Ԃɂ��邽�߂Ɏg�p
        const string TRIG_THINKING = "Thinking";
        const string TRIG_SHOWHAND = "ShowHand";
        const string TRIG_SHOWROCK = "ShowRock";
        const string TRIG_SHOWSCISSORS = "ShowScissors";
        const string TRIG_SHOWPAPER = "ShowPaper";
        const string TRIG_HAPPY = "Happy";
        const string TRIG_SAD = "Sad";

        public IJankenPlayer Player { get; set; }

        //�L�����N�^�[�̃A�j���[�V��������Ɏg�p
        Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();   
        }

        public void Init()
        {
            _animator.Play(STATE_INIT);
        }

        public void Happy()
        {
            _animator.SetTrigger(TRIG_HAPPY);
        }

        public void Sad()
        {
            _animator.SetTrigger(TRIG_SAD);
        }

        public void Thinking()
        {
            _animator.SetTrigger(TRIG_THINKING);
        }

        //����񂯂�ŏo������ɉ��������o���Đ�����
        public void ShowHand()
        {
            //�w����񂯂�c�x�̃��[�V����
            _animator.SetTrigger(TRIG_SHOWHAND);

            //IJankenPlayer����o��������擾
            JankenHand hand = Player.PlayedHand;

            //�w�ۂ�I�x�ɑ������郂�[�V����
            switch (hand)
            {
                case JankenHand.Rock:
                    _animator.SetTrigger(TRIG_SHOWROCK);
                    break;

                case JankenHand.Scissors:
                    _animator.SetTrigger(TRIG_SHOWSCISSORS);
                    break;

                case JankenHand.Paper:
                    _animator.SetTrigger(TRIG_SHOWPAPER);
                    break;
            }
        }

    }
}
