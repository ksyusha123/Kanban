using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApi
{
    public class TrelloMember
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }

        internal TrelloMember() { }
        
        /// <param name="id">also username allowed</param>
        public TrelloMember(string id)
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/members/{id}", "GET",
                new[] {("Accept", "application/json")});
            var proxy = TrelloClient.DeserializeJson<TrelloMember>(response);
            TrelloClient.Copy(proxy, this);
        }
    }
}
