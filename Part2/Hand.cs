using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    internal class Hand : IComparable<Hand>
    {
        public long Bet { get; set; }
        public int[] Cards { get; set; }

        public Hand(string cardString, string betString)
        {
            if (cardString.Length != 5)
            {
                throw new ArgumentException($"Bad card amount in {cardString}", nameof(cardString));
            }

            long bet = long.Parse(betString);
            int[] cards = new int[5];

            var cardChars = cardString.ToCharArray();

            for (int i = 0; i < cardChars.Length; i++)
            {
                int value;
                if (!int.TryParse(cardChars[i].ToString(), out value))
                {
                    switch (cardChars[i])
                    {
                        case 'T':
                            value = 10;
                            break;
                        case 'J':
                            value = 0;
                            break;
                        case 'Q':
                            value = 12;
                            break;
                        case 'K':
                            value = 13;
                            break;
                        case 'A':
                            value = 14;
                            break;
                        default: throw new ArgumentException($"Bad card value in {cardString}", nameof(cardString));
                    }
                }
                cards[i] = value;
            }

            Bet = bet;
            Cards = cards;
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) return 1;

            var compareCardCombo = GetCardComboValue(Cards).CompareTo(GetCardComboValue(other.Cards));
            if (compareCardCombo != 0) return compareCardCombo;

            for (int i = 0; i < Cards.Length; i++)
            {
                var cardCompare = Cards[i].CompareTo(other.Cards[i]);
                if (cardCompare != 0)
                {
                    return cardCompare;
                }
            }

            return 0;
        }

        public static int GetCardComboValue(int[] cards)
        {
            var counts = new Dictionary<int, int>();
            int jokers = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == 0)
                {
                    jokers++;
                }
                else
                {
                    counts[cards[i]] = cards.Count(c => c == cards[i]);
                }
            }

            int max = counts.Values.Count() >= 1 ? counts.Values.Max() : 0;
            int second = counts.Count() >= 2 ? second = counts.Values.OrderByDescending(i => i).Skip(1).First() : 0;

            if (max + jokers == 5)
                return 7; // 5 of a kind
            if (max + jokers == 4)
                return 6; // 4 of a kind
            if (max + second + jokers == 5)
                return 5; // Full house
            if (max + jokers == 3)
                return 4; // 3 of a kind
            if (max + second + jokers == 4)
                return 3; // two pair
            if (max + jokers == 2)
                return 2; // pair
            return 1; // high card
        }
    }
}
