using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrelloApi
{
    public class TrelloCard
    {
        //TODO
        public string Id { get; set; }
        public bool Closed { get; set; }
        public string Desc { get; set; }
        public string IdBoard { get; set; }
        public string IdList { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> IdMembers { get; set; }

        internal TrelloCard() { }

        public TrelloCard(string id)
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/cards/{id}", "GET",
                new List<(string title, string value)> { ("Accept", "application/json") });
            var proxy = TrelloClient.DeserializeJson<TrelloCard>(response);
            TrelloClient.Copy(proxy, this);
        }

        public async Task<TrelloCard> ReplaceToListAsync(string listId)
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/cards/{Id}", "PUT",
                new[] {("Accept", "application/json")}, new[] {("idList", listId)});
            return TrelloClient.DeserializeJson<TrelloCard>(response);
        }

        public async Task<string> DeleteAsync()
        {
            TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/cards/{Id}", "DELETE");
            return null;
        }
    }
}
