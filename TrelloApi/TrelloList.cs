using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApi
{
    public class  TrelloList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Closed { get; set; }
        public double Pos { get; set; }
        public string IdBoard { get; set; }
        public bool Subscribed { get; set; }
    }

    public class TrelloListClient
    {
        private TrelloClient client;

        public TrelloListClient(TrelloClient client)
        {
            this.client = client;
        }

        public async Task<TrelloList> LoadAsync(string id)
        {
            var response = await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}", "GET",
                    new List<(string title, string value)> { ("Accept", "application/json") }));
            return client.DeserializeJson<TrelloList>(response);
        }

        public async Task<TrelloList> CreateAsync(string name, string boardId)
        {
            var response = await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{boardId}/lists", "POST",
                        new[] { ("Accept", "application/json") }, new[] { ("name", name) }));
            return client.DeserializeJson<TrelloList>(response);
        }

        public async Task ArchiveAsync(string id)
        {
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}/closed", "PUT", parameters: new[] { ("value", "true") }));
        }

        public async Task MoveToBoardAsync(string listId, string boardId)
        {
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/lists/{listId}/idBoard", "PUT",
                    parameters: new[] { ("value", boardId) }));
        }

        public async Task<IEnumerable<TrelloCard>> GetAllCardsAsync(string id)
        {
            var response = await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}/cards", "GET",
                new[] { ("Accept", "application/json") }));
            return client.DeserializeJson<IEnumerable<TrelloCard>>(response);
        }

        public async Task RenameAsync(string id, string name)
        {
            await Task.Run(() =>
                client.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}", "PUT", null,
                    new[] {("name", name)}));
        }
    }
}
