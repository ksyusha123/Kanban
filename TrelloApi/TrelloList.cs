using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApi
{
    public class TrelloList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Closed { get; set; }
        public int Pos { get; set; }
        public string IdBoard { get; set; }
        public bool Subscribed { get; set; }
        
        internal TrelloList() { }

        public TrelloList(string id)
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}", "GET",
                new List<(string title, string value)> { ("Accept", "application/json") });
            var proxy = TrelloClient.DeserializeJson<TrelloList>(response);
            TrelloClient.Copy(proxy, this);
        }


        /// <returns>Returns True if moving was successful, False otherwise</returns>
        public bool MoveToBoard(string boardId)
        {
            try
            {
                TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{Id}/idBoard", "PUT",
                    parameters: new [] {("value", boardId)});
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TrelloCard> GetAllCards()
        {
            //TODO
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{Id}/cards", "GET",
                new[] {("Accept", "application/json")});
            return null;
        }
    }
}
