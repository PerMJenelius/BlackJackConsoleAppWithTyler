using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleAppBlackJack
{
    class Tyler
    {
        static List<Hand> tylersHands = new List<Hand>();
        static string dataPath = "C:/Projekt/XML/BlackJackConsoleAppWithTyler/tyler.xml";
        static Player tyler = GetTyler();

        public static Player GetTyler()
        {
            string xmlData = File.ReadAllText(dataPath);
            List<Player> tylerList = XMLConvert.XMLToObject(xmlData);

            return tylerList[0];
        }

        public static List<Hand> GetTylersHands()
        {
            return tylersHands;
        }

        public static void AskForAction(List<Hand> inputHands)
        {
            tylersHands[0].DealerHand = inputHands[0].DealerHand;
            Game.EvaluateHands(tylersHands);

            List<Hand> allHands = new List<Hand>();
            foreach (var hand in inputHands)
            {
                allHands.Add(hand);
            }
            foreach (var hand in tylersHands)
            {
                allHands.Add(hand);
            }

            do
            {
                int count = tylersHands.Count;

                for (int i = 0; i < count; i++)
                {
                    int action = ChooseAction(tylersHands[i], allHands);

                    switch (action)
                    {
                        case 1: tylersHands[i] = Hit(tylersHands[i]); break;
                        case 2: tylersHands[i].Stand = true; break;
                        case 3: tylersHands[i] = Split(tylersHands[i]); break;
                        case 4: tylersHands[i] = DoubleOrHit(tylersHands[i]); break;
                        case 5: tylersHands[i] = DoubleOrStand(tylersHands[i]); break;
                        case 6: tylersHands[i] = Game.Insurance(tylersHands[i]); break;
                        default: tylersHands[i].Stand = true; break;
                    }
                    Game.EvaluateHands(tylersHands);
                }
            } while (!Game.CheckForStand(tylersHands) && !Game.CheckForLose(tylersHands));
        }

        private static int ChooseAction(Hand inputHand, List<Hand> allHands)
        {
            int cardCount = CountCards(allHands);
            int action = 0;

            do
            {
                int dealerCard = inputHand.DealerHand[0].Value == 1 ? 11 : inputHand.DealerHand[0].Value;
                bool pair = inputHand.PlayerHand.Count == 2 && inputHand.PlayerHand[0].Value == inputHand.PlayerHand[1].Value;
                bool soft = inputHand.PlayerHandSoftValue > inputHand.PlayerHandValue;

                //CountCards
                if (dealerCard == 11 && cardCount >= 1)
                {
                    inputHand = Insurance(inputHand);
                }

                if (pair)
                {
                    action = Pair(inputHand, dealerCard, cardCount);
                }
                else if (soft)
                {
                    Soft(inputHand, dealerCard, cardCount);
                }
                else
                {
                    Solid(inputHand, dealerCard, cardCount);
                }

                inputHand = Game.EvaluateHands(inputHand);
            } while (!inputHand.Stand);

            return action;
        }

        private static int Solid(Hand inputHand, int dealerCard, int count)
        {
            int tylerHand = inputHand.PlayerHandValue;
            int action = 0;

            if (tylerHand <= 7)
            {
                action = 1;
            }
            else if (tylerHand == 8)
            {
                if (dealerCard <= 4)
                {
                    action = 1;
                }
                else if (dealerCard >= 5 && dealerCard <= 6)
                {
                    action = 4;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerHand == 9)
            {
                if (dealerCard <= 6)
                {
                    action = 4;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerHand >= 10 && tylerHand <= 11)
            {
                //CountCards
                if (tylerHand == 10 && dealerCard >= 10 && count >= 5)
                {
                    action = 2;
                }
                else if (dealerCard <= 9)
                {
                    action = 4;
                }
                else if (dealerCard >= 10)
                {
                    action = 1;
                }
            }
            else if (tylerHand == 12)
            {
                //CountCards
                if (dealerCard == 2 && count >= 5)
                {
                    action = 2;
                }
                //CountCards
                else if (dealerCard == 3 && count >= 1)
                {
                    action = 2;
                }
                //CountCards
                else if (dealerCard == 5 && count <= -1)
                {
                    action = 1;
                }
                //CountCards
                else if (dealerCard == 6 && count <= -5)
                {
                    action = 1;
                }
                else if (dealerCard >= 2 && dealerCard <= 3)
                {
                    action = 1;
                }
                else if (dealerCard >= 4 && dealerCard <= 6)
                {
                    action = 2;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerHand >= 13 && tylerHand <= 16)
            {
                //CountCards
                if (tylerHand == 13 && dealerCard <= 3 && count <= -1)
                {
                    action = 1;
                }
                //CountCards
                else if (tylerHand == 13 && dealerCard >= 4 && dealerCard <= 5 && count <= -5)
                {
                    action = 1;
                }
                //CountCards
                else if (tylerHand == 14 && dealerCard <= 3 && count <= -5)
                {
                    action = 1;
                }
                //CountCards
                else if (tylerHand == 14 && dealerCard == 11 && count >= 10)
                {
                    action = 2;
                }
                //CountCards
                else if (tylerHand == 15 && dealerCard == 2 && count <= -5)
                {
                    action = 1;
                }
                //CountCards
                else if (tylerHand == 15 && dealerCard == 9 && count >= 10)
                {
                    action = 2;
                }
                //CountCards
                else if (tylerHand == 15 && dealerCard >= 10 && count >= 5)
                {
                    action = 2;
                }
                //CountCards
                else if (tylerHand == 16 & dealerCard == 8 && count >= 10)
                {
                    action = 2;
                }
                //CountCards
                else if (tylerHand == 16 && dealerCard == 9 && count >= 5)
                {
                    action = 2;
                }
                //CountCards
                else if (tylerHand == 16 && dealerCard == 10 && count >= 1)
                {
                    action = 2;
                }
                //CountCards
                else if (tylerHand == 16 && dealerCard == 11 && count >= 5)
                {
                    action = 2;
                }
                else if (dealerCard <= 6)
                {
                    action = 2;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerHand >= 17)
            {
                //CountCards
                if (tylerHand == 17 && dealerCard == 11 && count <= -5)
                {
                    action = 1;
                }
                else
                {
                    action = 2;
                }
            }

            return action;
        }

        private static int Soft(Hand inputHand, int dealerCard, int count)
        {
            int tylerHand = inputHand.PlayerHandSoftValue;
            int action = 0;

            if (tylerHand >= 13 && tylerHand <= 16)
            {
                if (dealerCard <= 3)
                {
                    action = 1;
                }
                else if (dealerCard >= 4 && dealerCard <= 6)
                {
                    action = 4;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerHand == 17)
            {
                if (dealerCard <= 6)
                {
                    action = 4;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerHand == 18)
            {
                if (dealerCard == 2)
                {
                    action = 2;
                }
                else if (dealerCard >= 3 && dealerCard <= 6)
                {
                    action = 5;
                }
                else if (dealerCard >= 7 && dealerCard <= 8)
                {
                    action = 2;
                }
                else if (dealerCard >= 9)
                {
                    action = 1;
                }
            }
            else if (tylerHand == 19)
            {
                if (dealerCard <= 5)
                {
                    action = 2;
                }
                else if (dealerCard == 6)
                {
                    action = 5;
                }
                else if (dealerCard >= 7)
                {
                    action = 2;
                }
            }
            else if (tylerHand >= 20)
            {
                action = 2;
            }

            return action;
        }

        private static int Pair(Hand inputHand, int dealerCard, int count)
        {
            int tylerPair = inputHand.PlayerHand[0].Value;
            int action = 0;

            //CountCards
            if (dealerCard >= 3 && dealerCard <= 4 && count >= 10)
            {
                action = 2;
            }
            //CountCards
            else if (dealerCard >= 5 && dealerCard <= 6 && count >= 5)
            {
                action = 2;
            }
            else if (tylerPair == 2)
            {
                if (dealerCard <= 7)
                {
                    action = 3;
                }
                else if (dealerCard >= 8)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 3)
            {
                if (dealerCard > 1 && dealerCard <= 8)
                {
                    action = 3;
                }
                else if (dealerCard >= 9)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 4)
            {
                if (dealerCard <= 3)
                {
                    action = 1;
                }
                else if (dealerCard >= 4 && dealerCard <= 6)
                {
                    action = 3;
                }
                else if (dealerCard >= 7)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 5)
            {
                if (dealerCard <= 9)
                {
                    action = 4;
                }
                else if (dealerCard >= 10)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 6)
            {
                if (dealerCard <= 7)
                {
                    action = 3;
                }
                else if (dealerCard >= 8)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 7)
            {
                if (dealerCard <= 8)
                {
                    action = 3;
                }
                else if (dealerCard == 9)
                {
                    action = 1;
                }
                else if (dealerCard == 10)
                {
                    action = 2;
                }
                else if (dealerCard == 11)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 8)
            {
                if (dealerCard <= 9)
                {
                    action = 3;
                }
                else if (dealerCard >= 10)
                {
                    action = 1;
                }
            }
            else if (tylerPair == 9)
            {
                if (dealerCard <= 6)
                {
                    action = 3;
                }
                else if (dealerCard == 7)
                {
                    action = 2;
                }
                else if (dealerCard > 7 && dealerCard <= 9)
                {
                    action = 3;
                }
                else if (dealerCard >= 10)
                {
                    action = 2;
                }
            }
            else if (tylerPair == 10)
            {
                action = 2;
            }
            else if (tylerPair == 1)
            {
                if (dealerCard <= 10)
                {
                    action = 3;
                }
                else if (dealerCard == 11)
                {
                    action = 1;
                }
            }

            return action;
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

        private static Hand DoubleOrStand(Hand inputHand)
        {
            if (inputHand.PlayerHand.Count == 2)
            {
                inputHand = Game.Double(inputHand);
            }
            else
            {
                inputHand.Stand = true;
            }

            return inputHand;
        }

        private static Hand DoubleOrHit(Hand inputHand)
        {
            if (inputHand.PlayerHand.Count == 2)
            {
                inputHand = Game.Double(inputHand);
            }
            else
            {
                inputHand = Hit(inputHand);
            }

            return inputHand;
        }

        private static Hand Hit(Hand inputHand)
        {
            inputHand.PlayerHand = Game.DealCard(inputHand.PlayerHand, 1);
            return inputHand;
        }

        private static Hand Split(Hand inputHand)
        {
            List<Hand> hands = Game.Split(inputHand);
            tyler.Bankroll -= hands[1].Bet;
            return inputHand;
        }

        private static Hand Insurance(Hand inputHand)
        {
            inputHand = Game.Insurance(inputHand);
            tyler.Bankroll -= (0.5 * inputHand.Bet);
            return inputHand;
        }

        public static void AskForBet(Hand inputHand)
        {
            tylersHands.Clear();
            Hand tylersHand = new Hand();
            tylersHand.PlayerHand = Game.DealCard(tylersHand.PlayerHand, 2);
            tylersHand.DealerHand = inputHand.DealerHand;
            tylersHand.Bet = 5;
            tyler.Bankroll -= tylersHand.Bet;
            tylersHands.Add(tylersHand);
            Game.EvaluateHands(tylersHands);
        }

        internal static void EndGame(List<Hand> inputHands)
        {
            Game.EvaluateHands(tylersHands);

            for (int i = 0; i < tylersHands.Count; i++)
            {
                double result = Game.CompareHands(tylersHands[i]);
                tylersHands[i].TransactionAmount = result == 1 ? tylersHands[i].Bet + tylersHands[i].Insurance : result * tylersHands[i].Bet;
                tyler.Bankroll += tylersHands[i].TransactionAmount;

                tyler.Hands.Add(tylersHands[i]);
                SaveTyler();

                switch (result)
                {
                    case 0: Console.WriteLine("Tyler loses."); break;
                    case 2: Console.WriteLine("Tyler wins"); break;
                    case 2.5: Console.WriteLine("Tyler gets a blackjack"); break;
                    case 1:
                        {
                            if (tylersHands[i].Insurance > 0 && tylersHands[0].DealerHand.Count == 2 && tylersHands[0].DealerHandSoftValue == 21)
                            {
                                Console.WriteLine("Tyler's insurance pays out.");
                            }
                            else if (tylersHands[i].Insurance > 0 && tylersHands[i].PlayerHand.Count == 2 && tylersHands[i].PlayerHandSoftValue == 21)
                            {
                                Console.WriteLine("Tyler's insurance pays out.");
                            }
                            else
                            {
                                Console.WriteLine("Tyler gets a draw!");
                            };
                            break;
                        }
                }
            }

            Says(inputHands);
        }

        private static void SaveTyler()
        {
            List<Player> tylerList = new List<Player>
            {
                tyler
            };
            string xmlData = XMLConvert.ObjectToXml(tylerList);
            File.WriteAllText(dataPath, xmlData);
        }

        internal static void SetDealerHand(Hand hand)
        {
            foreach (var handy in tylersHands)
            {
                handy.DealerHand = hand.DealerHand;
            }
        }

        public static void Says(List<Hand> inputHands)
        {
            Console.WriteLine();

            for (int i = 0; i < inputHands.Count; i++)
            {
                Console.Write("Tyler says: ");

                var result = Game.CompareHands(inputHands[i]);

                if (result >= 2)
                {
                    Console.Write("Congratulations! ");
                }
                else if (result <= 1)
                {
                    Console.Write("Too bad! ");
                }
            }
            Console.WriteLine();
        }
    }
}