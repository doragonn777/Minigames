using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace JankenGame
{
    public struct JankenInfo
    {
        //じゃんけんの参加者と、各参加者が出した手を記録する
        Dictionary<IJankenPlayer, JankenHand> _participants;
        public Dictionary<IJankenPlayer, JankenHand> Participants => _participants;

        //じゃんけんの結果、どの手が勝ったか（あるいは引き分けか）を表す
        public enum MatchResult
        {
            RockWin,       //グー
            ScissorsWin,   //チョキ
            PaperWin,      //パー
            Draw        //引き分け
        }

        //どの手を出した参加者が勝ちなのかを記録する
        MatchResult _result;
        public MatchResult Result => _result;

        public JankenInfo(Dictionary<IJankenPlayer, JankenHand> participants, MatchResult result)
        {
            _participants = participants;
            _result = result;
        }

        /// <summary>
        /// 勝利した参加者(IJankenPlayer)を取得する
        /// </summary>
        /// <returns></returns>
        public IJankenPlayer[] GetWinner()
        {
            if (_result == MatchResult.Draw) return null;

            List<IJankenPlayer> winner = new List<IJankenPlayer>();
            foreach (var p in _participants)
            {
                //p.Valueはその参加者が出した手を表す
                if ((int) p.Value == (int) _result)
                {
                    winner.Add(p.Key);
                }
            }
            return winner.ToArray();
        }

        /// <summary>
        /// 指定した参加者が出した手を取得する
        /// </summary>
        public JankenHand GetHand(IJankenPlayer participant)
        {
            
            if (_participants.TryGetValue(participant, out JankenHand hand))
            {
                return hand;
            }
            else
            {
                throw new System.Exception();
            }

        }
    }

}