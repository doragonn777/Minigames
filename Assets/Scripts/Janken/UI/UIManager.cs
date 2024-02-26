using System;
using UnityEngine;

namespace JankenGame
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        //�Q�[���J�n�O�ɕ\������UI
        [SerializeField] BaseUI _pregameUI;

        //����L�����N�^�[���o�����I������UI
        [SerializeField] BaseUI _handSelectUI;
        
        //����񂯂�ŏ��s���t�����Ƃ��ɕ\�������
        [SerializeField] BaseUI _resultUI;

        //��������eUI���������Ƃ�m�点��C�x���g
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

        //�Q�[���̏�Ԃ��ύX���ꂽ�ۂɌĂ΂��
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

        //�eUI���������ɁA������O���ɓ`����C�x���g�𔭐�������
        private void InvokeOnPregameUIClosed()
        {
            OnPregameUIClosed?.Invoke();
        }

        //�eUI���������ɁA������O���ɓ`����C�x���g�𔭐�������
        private void InvokeOnHandSelectUIClosed()
        {
            OnHandSelectUIClosed?.Invoke();
        }

        //�eUI���������ɁA������O���ɓ`����C�x���g�𔭐�������
        private void InvokeOnResultUIClosed()
        {
            OnResultUIClosed?.Invoke();
        }
    }
}
