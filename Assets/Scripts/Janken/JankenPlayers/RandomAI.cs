using UnityEngine;

namespace JankenGame
{
    public class RandomAI : IJankenPlayer
    {
        public JankenHand PlayedHand { get; set; }

        public JankenHand GetHand()
        {
            //‚»‚ê‚¼‚ê3•ª‚Ì1‚ÌŠm—¦‚Åè‚ğo‚·
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

