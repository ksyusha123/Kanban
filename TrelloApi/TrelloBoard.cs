using System;
using System.Collections.Generic;
using System.Net;

namespace TrelloApi
{
    public class TrelloBoard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Closed { get; set; }
        public string IdOrganization { get; set; }
        public string IdEnterprise { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public bool CanInvite { get; set; }

        internal TrelloBoard() { }
        
        public TrelloBoard(string id)
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{id}", "GET",
                new List<(string title, string value)>{("Accept", "application/json")});
            var proxy = TrelloClient.DeserializeJson<TrelloBoard>(response);
            var properties = typeof(TrelloBoard).GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(this, prop.GetValue(proxy));
            }
        }

        public void Delete()
        {
            TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}", "DELETE");
        }

        public IEnumerable<TrelloList> GetAllLists()
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/lists", "GET",
                new List<(string title, string value)>{("Accept", "application/json")});
            var result = TrelloClient.DeserializeJson<List<TrelloList>>(response);
            return result;
        }
       
        /// <returns>Returns id of created list</returns>
        public string CreateList(string name)
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/lists", "POST",
                new[] {("Accept", "application/json")}, new[] {("name", name)});
            return TrelloClient.DeserializeJson<TrelloList>(response).Id;
        }
    }
}