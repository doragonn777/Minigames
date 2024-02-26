using System;
using UnityEngine;

namespace JankenGame
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        //ゲーム開始前に表示するUI
        [SerializeField] BaseUI _pregameUI;

        //操作キャラクターが出す手を選択するUI
        [SerializeField] BaseUI _handSelectUI;
        
        //じゃんけんで勝敗が付いたときに表示される
        [SerializeField] BaseUI _resultUI;

        //所持する各UIが閉じたことを知らせるイベント
        public Action OnPregameUIClosed;
        public Action OnHandSelectUIClosed;
        public Action OnResultUIClosed;

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
        }

        private void Start()
        {
            JankenManager.Instance.OnStateChanged += OnJankenStateChanged;

            _pregameUI.OnClosed += InvokeOnPregameUIClosed;
            _handSelectUI.OnClosed += InvokeOnHandSelectUIClosed;
            _resultUI.OnClosed += InvokeOnResultUIClosed;

            _pregameUI.Open();
        }

        //ゲームの状態が変更された際に呼ばれる
        private void OnJankenStateChanged(JankenState state)
        {
            switch (state)
            {
                case JankenState.HandSelect:
                    _handSelectUI.Open();
                    break;

                case JankenState.Result:
                    _resultUI.Open();
                    break;
            }
        }

        //各UIが閉じた時に、それを外部に伝えるイベントを発生させる
        private void InvokeOnPregameUIClosed()
        {
            OnPregameUIClosed?.Invoke();
        }

        //各UIが閉じた時に、それを外部に伝えるイベントを発生させる
        private void InvokeOnHandSelectUIClosed()
        {
            OnHandSelectUIClosed?.Invoke();
        }

        //各UIが閉じた時に、それを外部に伝えるイベントを発生させる
        private void InvokeOnResultUIClosed()
        {
            OnResultUIClosed?.Invoke();
        }
    }
}
