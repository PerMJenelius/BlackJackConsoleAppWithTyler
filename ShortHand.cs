using ConsoleAppBlackJack;
using System;
using System.Collections.Generic;

namespace BlackJackConsoleAppWithTyler
{
    public class ShortHand
    {
        public DateTime TimeStamp { get; set; }
        public List<string> DealerHand { get; set; }
        public List<string> PlayerHand { get; set; }
        public string Result { get; set; }
        public double Bet { get; set; }
        public double Winnings { get; set; }
        public bool Insurance { get; set; }
        public bool Double { get; set; }
        public bool Split { get; set; }

        public ShortHand(Hand inputHand)
        {
            TimeStamp = inputHand.TimeStamp;

            DealerHand = new List<string>();
            foreach (var card in inputHand.DealerHand)
            {
                DealerHand.Add($"{card.Rank} of {card.Suit} ({card.Value})");
            }

            PlayerHand = new List<string>();
            foreach (var card in inputHand.PlayerHand)
            {
                PlayerHand.Add($"{card.Rank} of {card.Suit} ({card.Value})");
            }

            switch (inputHand.Result)
            {
                case 0: Result = "Lose"; break;
                case 1: Result = "Even"; break;
                case 2: Result = "Win"; break;
                case 2.5: Result = "Blackjack"; break;
                default: Result = "Undeterminded"; break;
            }

            Bet = inputHand.Bet;
            Winnings = inputHand.TransactionAmount;
            Insurance = inputHand.Insurance > 0;
            Double = inputHand.Double;
            Split = inputHand.Split;
        }

        private ShortHand()
        {

        }
    }
}
