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
            var response = await Task.Run(()=> client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "GET",
                new List<(string title, string value)> { ("Accept", "application/json") }));
            return client.DeserializeJson<TrelloCard>(response);
        }

        public async Task<TrelloCard> ReplaceToListAsync(string id, string listId)
        {
            var response = await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "PUT",
                new[] { ("Accept", "application/json") }, new[] { ("idList", listId) }));
            return client.DeserializeJson<TrelloCard>(response);
        }

        public async Task DeleteAsync(string id)
        {
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "DELETE"));
        }

        public async Task<TrelloCard> CreateAsync(string listId, string name)
        {
            var response = await Task.Run(() => client.GetResponseByWebRequest("https://api.trello.com/1/cards", "POST",
                new[] { ("Accept", "application/json") }, new[] { ("idList", listId), ("name", name) }));
            return client.DeserializeJson<TrelloCard>(response);
        }

        public async Task RenameAsync(string id, string name)
        {
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "PUT",
                new[] { ("Accept", "application/json") }, new[] { ("name", name) }));
        }
        
        public async Task AddMemberAsync(string id, string memberUsername)
        {
            var member = await new TrelloMemberClient(client).LoadAsync(memberUsername);
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}/idMembers", "POST", null,
                new[] {("value", member.Id)}));
        }

        public async Task RemoveMemberAsync(string id, string memberUsername)
        {
            var member = await new TrelloMemberClient(client).LoadAsync(memberUsername);
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}/idMembers/{member.Id}", "DELETE"));
        }

        public async Task<IEnumerable<TrelloMember>> GetAllMembers(string id)
        {
            var response = await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}/members", "GET"));
            return client.DeserializeJson<IEnumerable<TrelloMember>>(response);
        }

        public async Task UpdateDescription(string id, string desc)
        {
            await Task.Run(() => client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "PUT",
                new[] {("Accept", "application/json")}, new[] {("desc", desc)}));
        }

        public async Task AddCommentAsync(string id, string text)
        {
            await Task.Run(() =>  client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}/actions/comments", "POST",
                new[] {("Accept", "application/json")}, new[] {("text", $"{text}")}));
        }

        public async Task<IEnumerable<TrelloAction>> GetAllCommentsAsync(string id)
        {
            return await Task.Run(() =>
                client.DeserializeJson<IEnumerable<TrelloAction>>(client.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}/actions?filter=commentCard", "GET",
                    new[] { ("Accept", "application/json") })));
        }
    }
}
