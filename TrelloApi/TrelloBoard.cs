using System;
using System.Collections.Generic;
using System.Linq;
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
            TrelloClient.Copy(proxy, this);
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

        public void ArchiveList(string name = null, string id = null)
        {
            var list = GetAllLists().FirstOrDefault(x => x.Id != null && x.Id == id || x.Name != null && x.Name == name);
            if (list is null)
                throw new ArgumentException("List not found");
            TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/lists/{list.Id}/closed", "PUT", parameters: new []{("value", "true")});
        }

        /// <returns>Returns created board as TrelloBoard class object</returns>
        public static TrelloBoard CreateBoard(string name)
        {
            var response = TrelloClient.GetResponseByWebRequest("https://api.trello.com/1/boards/", "POST",
                parameters: new[] {("name", name)});
            return TrelloClient.DeserializeJson<TrelloBoard>(response);
        }

        public IEnumerable<TrelloAction> GetAllActions()
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/actions", "GET");
            return TrelloClient.DeserializeJson<IEnumerable<TrelloAction>>(response);
        }

        public IEnumerable<TrelloMember> GetAllMembers()
        {
            var response = TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/members", "GET");
            return TrelloClient.DeserializeJson<IEnumerable<TrelloMember>>(response);
        }

        /// <param name="privilageType">Type of user privilage for board (Observer available only with the premium version of your account)</param>
        /// <exception cref="ArgumentNullException">username and id cannot be null at the same time</exception>
        public void AddMember(TrelloMemberTypes privilageType, string id = null, string username = null)
        {
            if (username is null && id is null)
                throw new ArgumentNullException(username,"Enter member username or id");
            var prms = new[] {("type", "")};
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

            var finalMemberId = id ?? new TrelloMember(username).Id;
            try
            {
                TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/members/{finalMemberId}", "PUT", parameters: prms);
            }
            catch (WebException)
            {
                throw new ArgumentException();
            }
        }

        public void DeleteMember(string username = null, string id = null)
        {
            if (username is null && id is null)
                throw new ArgumentNullException(username, "Enter member username or id");
            
            var finalMemberId = id ?? new TrelloMember(username).Id;

            TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/members/{finalMemberId}",
                "DELETE");
        }
    }
}