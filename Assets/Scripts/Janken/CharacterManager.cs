using System.Collections.Generic;
using UnityEngine;
using System;
namespace JankenGame
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance { get; private set; }

        //����񂯂�ɎQ������L�����N�^�[
        public List<Character> Characters { get; private set; }

        //�L�����N�^�[�̒ǉ���폜�ɂ���Đl�����ω��������Ƃ�ʒm����
        public Action OnCharacterNumChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Characters = new List<Character>();
            } 
            else
            {
                Destroy(gameObject);
            }
        }

        //�L�����N�^�[��ǉ�����
        public void AddCharacter(Character c)
        {
            Characters.Add(c);
            OnCharacterNumChanged?.Invoke();
        }

        //�L�����N�^�[�̃��X�g�����Z�b�g����
        public void ResetCharacter()
        {
            Characters.Clear();
        }
    }
}

