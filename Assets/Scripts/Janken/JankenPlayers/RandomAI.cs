using UnityEngine;

namespace JankenGame
{
    public class RandomAI : IJankenPlayer
    {
        public JankenHand PlayedHand { get; set; }

        public JankenHand GetHand()
        {
            //���ꂼ��3����1�̊m���Ŏ���o��
            switch (Random.Range(0, 3))
            {
                case 0:
                    PlayedHand = JankenHand.Rock;
                    break;
                
                case 1:
                    PlayedHand = JankenHand.Scissors;
                    break;

                case 2:
                    PlayedHand = JankenHand.Paper;
                    break;

                default:
                    throw new System.Exception();

            }
            return PlayedHand;
        }
    }
}

