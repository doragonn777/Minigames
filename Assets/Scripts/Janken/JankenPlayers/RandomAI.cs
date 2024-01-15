using UnityEngine;

namespace JankenGame
{
    public class RandomAI : IJankenPlayer
    {
        public JankenHand GetHand()
        {
            //‚»‚ê‚¼‚ê3•ª‚Ì1‚ÌŠm—¦‚Åè‚ğo‚·
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

