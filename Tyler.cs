using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppBlackJack
{
    class Tyler
    {
        static List<Hand> tylersHands = new List<Hand>();
        static string dataPath = "C:/Projekt/XML/BlackJackConsoleAppWithTyler/tyler.xml";

        public static Player GetTyler()
        {
            string xmlData = File.ReadAllText(dataPath);
            List<Player> tylerList = XMLConvert.XMLToObject(xmlData);

            return tylerList[0];
        }

        public static void AskForAction(List<Hand> inputHands)
        {
            tylersHands[0].DealerHand = inputHands[0].DealerHand;
            Game.EvaluateHands(tylersHands);

            List<Hand> allHands = inputHands;
            foreach (var hand in tylersHands)
            {
                allHands.Add(hand);
            }
            int count = CountCards(allHands);

            for (int i = 0; i < tylersHands.Count; i++)
            {
                int dealerCard = tylersHands[i].DealerHand[0].Value == 1 ? 11 : tylersHands[i].DealerHand[0].Value;
                bool pair = tylersHands[i].PlayerHand.Count == 2 && tylersHands[i].PlayerHand[0].Value == tylersHands[i].PlayerHand[1].Value;
                bool soft = tylersHands[i].PlayerHandSoftValue > tylersHands[i].PlayerHandValue;

                //CountCards
                if (count >= 1)
                {
                    Insurance();
                }

                if (pair)
                {
                    int tylerPair = tylersHands[i].PlayerHand[0].Value;

                    if (tylerPair == 2)
                    {
                        if (dealerCard <= 7)
                        {
                            Split();
                        }
                        else if (dealerCard >= 8)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 3)
                    {
                        if (dealerCard > 1 && dealerCard <= 8)
                        {
                            Split();
                        }
                        else if (dealerCard >= 9)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 4)
                    {
                        if (dealerCard <= 3)
                        {
                            Hit();
                        }
                        else if (dealerCard >= 4 && dealerCard <= 6)
                        {
                            Split();
                        }
                        else if (dealerCard >= 7)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 5)
                    {
                        if (dealerCard <= 9)
                        {
                            DoubleOrHit();
                        }
                        else if (dealerCard >= 10)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 6)
                    {
                        if (dealerCard <= 7)
                        {
                            Split();
                        }
                        else if (dealerCard >= 8)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 7)
                    {
                        if (dealerCard <= 8)
                        {
                            Split();
                        }
                        else if (dealerCard == 9)
                        {
                            Hit();
                        }
                        else if (dealerCard == 10)
                        {
                            Stand();
                        }
                        else if (dealerCard == 11)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 8)
                    {
                        if (dealerCard <= 9)
                        {
                            Split();
                        }
                        else if (dealerCard >= 10)
                        {
                            Hit();
                        }
                    }
                    else if (tylerPair == 9)
                    {
                        if (dealerCard <= 6)
                        {
                            Split();
                        }
                        else if (dealerCard == 7)
                        {
                            Stand();
                        }
                        else if (dealerCard > 7 && dealerCard <= 9)
                        {
                            Split();
                        }
                        else if (dealerCard > 9)
                        {

                        }
                    }
                    else if (tylerPair == 10)
                    {
                        Stand();
                    }
                    else if (tylerPair == 1)
                    {
                        if (dealerCard <= 10)
                        {
                            Split();
                        }
                        else if (dealerCard == 11)
                        {
                            Hit();
                        }
                    }
                }
                else if (soft)
                {
                    int tylerHand = tylersHands[i].PlayerHandSoftValue;

                    if (tylerHand >= 13 && tylerHand <= 16)
                    {
                        if (dealerCard <= 3)
                        {
                            Hit();
                        }
                        else if (dealerCard >= 4 && dealerCard <= 6)
                        {
                            DoubleOrHit();
                        }
                        else if (dealerCard >= 7)
                        {
                            Hit();
                        }
                    }
                    else if (tylerHand == 17)
                    {
                        if (dealerCard <= 6)
                        {
                            DoubleOrHit();
                        }
                        else if (dealerCard >= 7)
                        {
                            Hit();
                        }
                    }
                    else if (tylerHand == 18)
                    {
                        if (dealerCard == 2)
                        {
                            Stand();
                        }
                        else if (dealerCard >= 3 && dealerCard <= 6)
                        {
                            DoubleOrStand();
                        }
                    }
                    else if (tylerHand == 19)
                    {
                        if (dealerCard <= 5)
                        {
                            Stand();
                        }
                        else if (dealerCard == 6)
                        {
                            DoubleOrStand();
                        }
                        else if (dealerCard >= 7)
                        {
                            Stand();
                        }
                    }
                    else if (tylerHand >= 20)
                    {
                        Stand();
                    }
                }
                else
                {
                    int tylerHand = tylersHands[i].PlayerHandValue;

                    if (tylerHand <= 7)
                    {
                        Hit();
                    }
                    else if (tylerHand == 8)
                    {
                        if (dealerCard <= 4)
                        {
                            Hit();
                        }
                        else if (dealerCard >= 5 && dealerCard <= 6)
                        {
                            DoubleOrHit();
                        }
                        else if (dealerCard >= 7)
                        {

                        }
                    }
                    else if (tylerHand == 9)
                    {
                        if (dealerCard <= 6)
                        {
                            DoubleOrHit();
                        }
                        else if (dealerCard >= 7)
                        {
                            Hit();
                        }
                    }
                    else if (tylerHand >= 10 && tylerHand <= 11)
                    {
                        if (dealerCard <= 9)
                        {
                            DoubleOrHit();
                        }
                        else if (dealerCard >= 10)
                        {
                            Hit();
                        }
                    }
                    else if (tylerHand == 12)
                    {
                        //CountCards
                        if (dealerCard == 2 && count >= 5)
                        {
                            Stand();
                        }
                        //CountCards
                        else if (dealerCard == 3 && count >= 1)
                        {
                            Stand();
                        }
                        if (dealerCard >= 2 && dealerCard <= 3)
                        {
                            Hit();
                        }
                        else if (dealerCard >= 4 && dealerCard <= 6)
                        {
                            Stand();
                        }
                        else if (dealerCard >= 7)
                        {
                            Hit();
                        }
                    }
                    else if (tylerHand >= 13 && tylerHand <= 16)
                    {
                        //CountCards
                        if (tylerHand == 15 && dealerCard >= 10 && count >= 5)
                        {
                            Stand();
                        }
                        //CountCards
                        else if (tylerHand == 16 && dealerCard == 10 && count >= 1)
                        {
                            Stand();
                        }
                        //CountCards
                        else if (tylerHand == 16 && dealerCard == 9 && count >= 5)
                        {
                            Stand();
                        }
                        //CountCards
                        else if (tylerHand == 16 && dealerCard == 11 && count >= 5)
                        {
                            Stand();
                        }
                        else if (dealerCard <= 6)
                        {
                            Stand();
                        }
                        else if (dealerCard >= 7)
                        {
                            Hit();
                        }
                    }
                    else if (tylerHand >= 17)
                    {
                        Stand();
                    }
                }
            }
        }

        private static void Insurance()
        {
            //Check for prerequisites;
        }

        internal static int CountCards(List<Hand> inputHands)
        {
            List<Card> revealedCards = new List<Card>();
            int count = 0;

            foreach (var card in inputHands[0].DealerHand)
            {
                revealedCards.Add(card);
            }
            foreach (var hand in inputHands)
            {
                foreach (var card in hand.PlayerHand)
                {
                    revealedCards.Add(card);
                }
            }

            foreach (var card in revealedCards)
            {
                if (card.Value == 1)
                {
                    --count;
                }
                if (card.Value > 1 && card.Value <= 6)
                {
                    ++count;
                }
                else if (card.Value == 10)
                {
                    --count;
                }
            }

            return count;
        }

        internal static void PrintTyler()
        {
            Console.WriteLine("Tyler has:");

            for (int i = 0; i < tylersHands.Count; i++)
            {
                if (tylersHands.Count > 1)
                {
                    Console.Write($"({i + 1}) ");
                }

                for (int j = 0; j < tylersHands[i].PlayerHand.Count; j++)
                {
                    Console.Write($"{tylersHands[i].PlayerHand[j].Rank} of {tylersHands[i].PlayerHand[j].Suit}");

                    if (j < (tylersHands[i].PlayerHand.Count - 2))
                    {
                        Console.Write(", ");
                    }
                    else if (j < (tylersHands[i].PlayerHand.Count - 1))
                    {
                        Console.Write(" and ");
                    }
                }

                Console.Write($" ({tylersHands[i].PlayerHandValue}");

                if (tylersHands[i].PlayerHandSoftValue > tylersHands[i].PlayerHandValue && tylersHands[i].PlayerHandSoftValue <= 21)
                {
                    Console.Write($" or {tylersHands[i].PlayerHandSoftValue}");
                }

                Console.WriteLine(")");
            }
            Console.WriteLine();
        }

        private static void DoubleOrStand()
        {
            Console.WriteLine("Double or stand");
        }

        private static void DoubleOrHit()
        {
            Console.WriteLine("Double or hit");
        }

        private static void Hit()
        {
            Console.WriteLine("Hit");
        }

        private static void Split()
        {
            Console.WriteLine("Split");
        }

        private static void Stand()
        {
            Console.WriteLine("Stand");
        }

        public static void AskForBet(Hand inputHand)
        {
            tylersHands.Clear();

            Hand tylersHand = new Hand();
            tylersHand.PlayerHand = Game.DealCard(tylersHand.PlayerHand, 2);
            tylersHand.DealerHand = inputHand.DealerHand;
            tylersHand.Bet = 5;
            tylersHands.Add(tylersHand);
            Game.EvaluateHands(tylersHands);
        }
    }
}
