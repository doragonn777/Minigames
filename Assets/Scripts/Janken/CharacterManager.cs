using System.Collections.Generic;
using UnityEngine;
using System;
namespace JankenGame
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance { get; private set; }

        //じゃんけんに参加するキャラクター
        public List<Character> Characters { get; private set; }

        //キャラクターの追加や削除によって人数が変化したことを通知する
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

        //キャラクターを追加する
        public void AddCharacter(Character c)
        {
            Characters.Add(c);
            OnCharacterNumChanged?.Invoke();
        }

        //キャラクターのリストをリセットする
        public void ResetCharacter()
        {
            Characters.Clear();
        }
    }
}

