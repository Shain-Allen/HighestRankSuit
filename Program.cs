using System;
using static System.Console;

namespace HighestRankSuit
{
    class Program
    {
        public static void Main(string[] args)
        {
            Player[] players = new Player[4];
            players[0] = new Player("Paul");
            players[1] = new Player("Tom");
            players[2] = new Player("Pat");
            players[3] = new Player("Susan");

            WriteLine("\nHighest Rank Wins! Card Game Simulation");
            WriteLine("=================================================");

            Deck theDeck = new Deck();
            WriteLine($"\nHere's the new deck of cards:\n{theDeck}");

            theDeck.Shuffle();
            WriteLine($"\nHere's the shuffled deck of cards:\n{theDeck}");

            int numRounds = theDeck.Size() / players.Length;

            // Deal the cards
            int i = 0;
            while (theDeck.Size() > 0)
            {
                players[i].AddCardToHand(theDeck.DealCard());
                i = ++i % players.Length;
            }

            // Display each players starting hand
            WriteLine("\nAnd here are our players and their hands:");
            foreach (Player player in players)
                WriteLine(player);

            // Play the game
            for (int round = 1; round <= numRounds; round++)
            {
                WriteLine($"\nStarting round #{round}...");

                Card[] cardsPlayed = new Card[players.Length];
                for (i = 0; i < players.Length; i++)
                {
                    cardsPlayed[i] = players[i].RemoveCardFromHand();
                    WriteLine($"{players[i].Name} played the {cardsPlayed[i]}");
                }

                Rank maxRank = Rank.Ace;
                for (i = 0; i < players.Length; i++)
                {
                    if (cardsPlayed[i].Rank > maxRank)
                        maxRank = cardsPlayed[i].Rank;
                }
                WriteLine($"The maximum rank in this round was {maxRank}");

                for (i = 0; i < players.Length; i++)
                {
                    if (cardsPlayed[i].Rank == maxRank)
                    {
                        players[i].AddPoint();
                        WriteLine($"{players[i].Name} got a point!");
                    }
                }

                WriteLine($"Round #{round} is complete.");
            }

            WriteLine("\n============== Game Over! =================\n");

            WriteLine("Final Scores:");
            WriteLine("--------------------------");
            foreach (Player player in players)
            {
                WriteLine($"{player.Name} has {player.Score} points");
            }

            int winningScore = 0;
            foreach (Player player in players)
            {
                if (player.Score > winningScore)
                    winningScore = player.Score;
            }

            string ampersand = "";
            string winnerNames = "";
            foreach (Player player in players)
            {
                if (player.Score == winningScore)
                {
                    winnerNames += $"{ampersand}{player.Name}";
                    ampersand = " & ";
                }
            }

            WriteLine();
            if (winnerNames.Contains("&"))
                WriteLine($"It's a tie! With {winningScore} points, the winners are {winnerNames}!");
            else
                WriteLine($"The winner is {winnerNames} with {winningScore} points!");
            WriteLine();
        }

    }

}
