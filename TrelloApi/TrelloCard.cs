using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApi
{
    public class TrelloCard
    {
        public string Id { get; set; }
        public bool Closed { get; set; }
        public string Desc { get; set; }
        public string IdBoard { get; set; }
        public string IdList { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> IdMembers { get; set; }

    }

    public class TrelloCardClient
    {
        private TrelloClient client;
        public TrelloCardClient(TrelloClient client)
        {
            this.client = client;
        }
        
        public async Task<TrelloCard> LoadAsync(string id)
        {
            var response = client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "GET",
                new List<(string title, string value)> { ("Accept", "application/json") });
            return client.DeserializeJson<TrelloCard>(response);
        }

        public async Task<TrelloCard> ReplaceToListAsync(string id, string listId)
        {
            var response = client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "PUT",
                new[] { ("Accept", "application/json") }, new[] { ("idList", listId) });
            return client.DeserializeJson<TrelloCard>(response);
        }

        public async Task DeleteAsync(string id)
        {
            client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "DELETE");
        }

        public async Task<TrelloCard> Create(string listId, string name)
        {
            var response = client.GetResponseByWebRequest("https://api.trello.com/1/cards", "POST",
                new[] { ("Accept", "application/json") }, new[] { ("idList", listId), ("name", name) });
            return client.DeserializeJson<TrelloCard>(response);
        }

        public async Task Rename(string id, string name)
        {
            client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "PUT",
                new[] { ("Accept", "application/json") }, new[] { ("name", name) });
        }
        
        public async Task AddMemberAsync(string id, string memberId)
        {
            //TODO
        }

        public async Task RemoveMember(string id, string memberId)
        {
            //TODO
        }

        public async Task GetAllMembers(string id)
        {
            //TODO
        }
    }
}
