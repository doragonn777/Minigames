using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace JankenGame
{
    public struct JankenInfo
    {
        //����񂯂�̎Q���҂ƁA�e�Q���҂��o��������L�^����
        Dictionary<IJankenPlayer, JankenHand> _participants;
        public Dictionary<IJankenPlayer, JankenHand> Participants => _participants;

        //����񂯂�̌��ʁA�ǂ̎肪���������i���邢�͈����������j��\��
        public enum MatchResult
        {
            RockWin,       //�O�[
            ScissorsWin,   //�`���L
            PaperWin,      //�p�[
            Draw        //��������
        }

        //�ǂ̎���o�����Q���҂������Ȃ̂����L�^����
        MatchResult _result;
        public MatchResult Result => _result;

        public JankenInfo(Dictionary<IJankenPlayer, JankenHand> participants, MatchResult result)
        {
            _participants = participants;
            _result = result;
        }

        /// <summary>
        /// ���������Q����(IJankenPlayer)���擾����
        /// </summary>
        /// <returns></returns>
        public IJankenPlayer[] GetWinner()
        {
            if (_result == MatchResult.Draw) return null;

            List<IJankenPlayer> winner = new List<IJankenPlayer>();
            foreach (var p in _participants)
            {
                //p.Value�͂��̎Q���҂��o�������\��
                if ((int) p.Value == (int) _result)
                {
                    winner.Add(p.Key);
                }
            }
            return winner.ToArray();
        }

        /// <summary>
        /// �w�肵���Q���҂��o��������擾����
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