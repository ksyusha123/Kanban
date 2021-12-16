using System;
using System.Linq;
using TrelloApi;

namespace TestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            TrelloClient.Token = "1610c44f0d446765b29a8459127e82571f128cf9fad01a960c11db55448ac0d5";
            var board = new TrelloBoard("cS6ZHBdL");
            var list = board.GetAllLists().First();
            list.GetAllCards();
        }
    }
}
