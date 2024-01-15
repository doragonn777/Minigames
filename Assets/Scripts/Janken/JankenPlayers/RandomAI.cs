using UnityEngine;

namespace JankenGame
{
    public class RandomAI : IJankenPlayer
    {
        public JankenHand GetHand()
        {
            //それぞれ3分の1の確率で手を出す
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

