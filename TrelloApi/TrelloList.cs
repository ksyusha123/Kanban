using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class TrelloListClient
    {
        private string _apiKey;
        private string _userToken;

        public TrelloListClient(string userToken, string apiKey)
        {
            _apiKey = apiKey;
            _userToken = userToken;
        }

        private void Authorize()
        {
            TrelloClient.ApiKey = _apiKey;
            TrelloClient.Token = _userToken;
        }

        public async Task<TrelloList> LoadAsync(string id)
        {
            Authorize();
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}", "GET",
                    new List<(string title, string value)> { ("Accept", "application/json") });
            return TrelloClient.DeserializeJson<TrelloList>(response);
        }

        public async Task<TrelloList> CreateAsync(string name, string boardId)
        {
            Authorize();
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{boardId}/lists", "POST",
                        new[] { ("Accept", "application/json") }, new[] { ("name", name) });
            return TrelloClient.DeserializeJson<TrelloList>(response);
        }

        public async Task ArchiveAsync(string id)
        {
            Authorize();
            TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}/closed", "PUT", parameters: new[] { ("value", "true") });
        }

        public async Task MoveToBoardAsync(string listId, string boardId)
        {
            Authorize();
            TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{listId}/idBoard", "PUT",
                    parameters: new[] { ("value", boardId) });
        }

        public async Task<IEnumerable<TrelloCard>> GetAllCardsAsync(string id)
        {
            Authorize();
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{id}/cards", "GET",
                new[] { ("Accept", "application/json") });
            return TrelloClient.DeserializeJson<IEnumerable<TrelloCard>>(response);
        }
    }
}
