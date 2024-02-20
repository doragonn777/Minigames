using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JankenGame
{
    /// <summary>
    /// ゲームの進行に合わせて画面上のキャラクターのアニメーションを制御する
    /// </summary>
    public class Character : MonoBehaviour
    {
        //各アニメーションの再生に使用する文字列
        const string STATE_INIT = "Init";                   //アニメーションを初期状態にするために使用
        const string TRIG_THINKING = "Thinking";
        const string TRIG_SHOWHAND = "ShowHand";
        const string TRIG_SHOWROCK = "ShowRock";
        const string TRIG_SHOWSCISSORS = "ShowScissors";
        const string TRIG_SHOWPAPER = "ShowPaper";
        const string TRIG_HAPPY = "Happy";
        const string TRIG_SAD = "Sad";

        public IJankenPlayer Player { get; set; }

        //キャラクターのアニメーション制御に使用
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

        //じゃんけんで出した手に応じた演出を再生する
        public void ShowHand()
        {
            //『じゃんけん…』のモーション
            _animator.SetTrigger(TRIG_SHOWHAND);

            //IJankenPlayerから出した手を取得
            JankenHand hand = Player.PlayedHand;

            //『ぽん！』に相当するモーション
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
