using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
    }

    public class TrelloBoardClient
    {
        private TrelloClient client;
        
        public TrelloBoardClient(TrelloClient client)
        {
            this.client = client;
        }

        public async Task<TrelloBoard> LoadAsync(string id)
        {
            var response = client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{id}", "GET",
                new List<(string title, string value)> { ("Accept", "application/json") });
            return client.DeserializeJson<TrelloBoard>(response);
        }

        public async Task DeleteAsync(string id)
        {
            client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{id}", "DELETE");
        }

        public async Task<IEnumerable<TrelloList>> GetAllListsAsync(string id)
        {
            var response = client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{id}/lists", "GET",
                        new List<(string title, string value)> { ("Accept", "application/json") });
            var result = client.DeserializeJson<List<TrelloList>>(response);
            return result;
        }

        public async Task<TrelloBoard> CreatedAsync(string name)
        {
            var response = client.GetResponseByWebRequest("https://api.trello.com/1/boards/", "POST",
                parameters: new[] { ("name", name) });
            return client.DeserializeJson<TrelloBoard>(response);
        }

        public async Task<IEnumerable<TrelloAction>> GetAllActionsAsync(string id)
        {
            var response = client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{id}/actions", "GET");
            return client.DeserializeJson<IEnumerable<TrelloAction>>(response);
        }

        public async Task<IEnumerable<TrelloMember>> GetAllMembersAsync(string id)
        {
            var response = client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{id}/members", "GET");
            return client.DeserializeJson<IEnumerable<TrelloMember>>(response);
        }

        public async Task AddMemberAsync(string boardId, TrelloMemberTypes privilageType, string memberId)
        {
            var prms = new[] { ("type", "") };
            switch (privilageType)
            {
                case TrelloMemberTypes.Admin:
                    prms[0].Item2 = "admin";
                    break;
                case TrelloMemberTypes.Normal:
                    prms[0].Item2 = "normal";
                    break;
                case TrelloMemberTypes.Observer:
                    prms[0].Item2 = "observer";
                    break;
            }

            try
            {
                client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{boardId}/members/{memberId}", "PUT", parameters: prms);
            }
            catch (WebException)
            {
                throw new ArgumentException();
            }
        }

        public async Task DeleteMemberAsync(string boardId, string memberId)
        {
            client.GetResponseByWebRequest($"https://api.trello.com/1/boards/{boardId}/members/{memberId}",
                "DELETE");
        }
    }
}