using System;
using System.Linq;
using TrelloApi;

namespace TestApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new TrelloBoard("cS6ZHBdL");
            var list = board.GetAllLists().First();
            list.GetAllCards();
        }
    }
}
