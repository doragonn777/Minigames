using UnityEngine;

namespace JankenGame
{
    public class RandomAI : IJankenPlayer
    {
        public JankenHand GetHand()
        {
            //���ꂼ��3����1�̊m���Ŏ���o��
            switch (Random.Range(0, 3))
            {
                case 0:
                    return JankenHand.Rock;
                
                case 1:
                    return JankenHand.Scissors;

                case 2:
                    return JankenHand.Paper;

                default:
                    throw new System.Exception();

            }
        }
    }
}

