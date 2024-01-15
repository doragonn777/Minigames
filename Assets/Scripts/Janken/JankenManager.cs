using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace JankenGame
{
    public enum JankenState
    {
        PreGame,            //ゲーム開始前
        HandSelect,         //プレイヤーが出す手を選択している状態
        ShowingHands,       //じゃんけんをする演出中
        HandSelectAgain,    //あいこで再度手を選択している状態
        Result              //じゃんけんの決着がついた状態
    }

    public class JankenManager : MonoBehaviour
    {
        //ゲームの進行に合わせて画面上に表示されるテキスト
        const string TEXT_PREGAME = "";
        const string TEXT_SELECTHAND = "Select your hand!";
        const string TEXT_HANDSELECTED = "Jan Ken ...";
        const string TEXT_SHOWEDHAND = "PON!!";
        const string TEXT_RESULT_WIN = "YOU WIN!!!";
        const string TEXT_RESULT_LOSE = "You Lose...";

        [SerializeField]
        Text _jankenText;           //画面上に表示するテキスト

        [SerializeField]
        Character _mainPlayerCharacter;     //プレイヤーに対応するキャラクター
        MainPlayer _mainPlayer;             //プレイヤーが選択した手を保存するクラス

        [SerializeField]
        Character _npc;             //NPCに対応するキャラクター

        [SerializeField]
        BaseUI preGameUI;           //ゲーム開始前に表示されるUI

        [SerializeField]
        HandSelectUI handSelectUI;  //プレイヤーが出す手を選択するUI

        [SerializeField]
        ResultUI resultUI;          //じゃんけんの結果を表示するUI

        JankenState _currentState;  //現在の状態

        JankenSystem _system;

        //じゃんけんの参加者(IJankenPlayer)とビュー(Character)を紐づけている
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
        /// 初期化処理
        /// </summary>
        private void Init()
        {
            _jankenText.text = TEXT_PREGAME;

            //マネージャが管理するじゃんけん参加者をリセット
            _players.Clear();

            //JankenSystemの内部で管理するじゃんけん参加者をリセット
            _system.ResetPlayer();

            //画面に表示されているキャラクターの状態を初期化する
            _mainPlayerCharacter.Init();
            _npc.Init();

            //プレイヤーが操作（＝出す手を選ぶ）する対象
            _mainPlayer = new MainPlayer();

            //現時点では、じゃんけんに参加するのはプレイヤーとNPC1体のみ
            //（今後、動的に人数を変更できるようにする）
            _players.Add(_mainPlayer, _mainPlayerCharacter);
            _players.Add(new RandomAI(), _npc);

            _currentState = JankenState.PreGame;
            preGameUI.Open();
        }

        /// <summary>
        /// HandSelectUIを表示し、出す手を選ばせる
        /// </summary>
        private void SelectHand()
        {
            _jankenText.text = TEXT_SELECTHAND;

            if (_currentState == JankenState.PreGame)
            {
                _currentState = JankenState.HandSelect;

                //じゃんけんの参加者をJankenSystemに登録
                foreach (var p in _players)
                {
                    _system.AddPlayer(p.Key);
                }
            } 
            else
            {
                _currentState = JankenState.HandSelectAgain;
            }


            //画面上の演出
            foreach (var p in _players.Values)
            {
                p.Thinking();
            }

            handSelectUI.Open();
        }

        /// <summary>
        /// HandSelectUIが閉じた時に呼び出される
        /// </summary>
        private void OnHandSelected()
        {
            _jankenText.text = TEXT_HANDSELECTED;

            //UIで選択した手を登録
            _mainPlayer.selectedHand = handSelectUI.SelectedHand;

            //じゃんけんの結果を取得
            JankenInfo info = _system.Judge();

            //画面上のキャラクターに自分が出した手に対応する手を出させる
            foreach (var p in _players)
            {
                p.Value.ShowHand(info.GetHand(p.Key));
            }

            //画面上の演出を待ち、じゃんけんの結果に応じた処理をする
            StartCoroutine(WaitJankenAnim(info));
        }

        /// <summary>
        /// じゃんけんが引き分けになった時に呼び出される
        /// </summary>
        private void OnDraw()
        {
            //TODO: 「あいこで…」的な文字を表示させる
            SelectHand();
        }

        /// <summary>
        /// じゃんけんの決着がついたときに呼び出される
        /// </summary>
        private void OnResult(JankenInfo info)
        {
            //引き分けの状態でリザルトは表示されない
            Assert.IsTrue(info.Result != JankenInfo.MatchResult.Draw);

            //グーチョキパーの並びが一致しているので明示的に変換する
            JankenHand winnerHand = (JankenHand) info.Result;

            //プレイヤーの勝敗に応じて表示するテキストを変更
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
                //じゃんけんの結果から、pに一致する参加者が出した手を取得
                if (info.GetHand(p.Key) == winnerHand)
                {
                    p.Value.Happy();
                } 
                else
                {
                    p.Value.Sad();
                }
            }

            //TODO: 上記の演出が完了するまで待つ

            resultUI.Open();
        }

        /// <summary>
        /// ResultUIが閉じた時に呼び出される
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
        /// じゃんけんを終了し、ゲーム選択画面に戻る
        /// </summary>
        private void QuitGame()
        {
            GameStateManager.Instance.ChangeState(GameState.GameSelect);
        }

        //『じゃんけん…』の部分の演出の待ち時間（秒）
        const int JANKEN_ANIM_WAIT_SEC = 1;
        //『ぽん！』の部分の演出の待ち時間（秒）
        const int PON_ANIM_WAIT_SEC = 1;

        //キャラクターの『じゃんけんぽん』という演出を待ち、勝敗に応じた処理をする
        private IEnumerator WaitJankenAnim(JankenInfo info)
        {
            yield return new WaitForSeconds(JANKEN_ANIM_WAIT_SEC);

            _jankenText.text = TEXT_SHOWEDHAND;

            //じゃんけんする演出の完了を待つ
            yield return new WaitForSeconds(PON_ANIM_WAIT_SEC);


            //勝敗（引き分け）に応じて処理を変更する
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