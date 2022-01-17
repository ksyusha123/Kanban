using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApi
{
    public class TrelloMember
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
    }

    public class TrelloMemberClient
    {
        private TrelloClient client;
        public TrelloMemberClient(TrelloClient client)
        {
            this.client = client;
        }

        public async Task<TrelloMember> LoadAsync(string id)
        {
            return await Task.Run(() =>
                client.DeserializeJson<TrelloMember>(client.GetResponseByWebRequest(
                    $"https://api.trello.com/1/members/{id}", "GET", new[] {("Accept", "application/json")})));
        }
    }
}
