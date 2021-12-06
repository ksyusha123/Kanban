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

        public void AddMember(string memberUsername, TrelloMemberTypes privilageType, string memberId = null)
        {
            if (memberUsername is null && memberId is null)
                throw new ArgumentNullException(memberUsername,"Enter members's memberUsername or id");
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

            var finalMemberId = memberId ?? TrelloClient.DeserializeJson<TrelloMember>(TrelloClient.GetResponseByWebRequest(
                    $"https://api.trello.com/1/members/{memberUsername}", "GET",
                    new[] {("Accept", "application/json")}))
                .Id;
            try
            {
                TrelloClient.GetResponseByWebRequest($"https://api.trello.com/1/boards/{Id}/members/{finalMemberId}", "PUT", parameters: prms);
            }
            catch (WebException)
            {
                throw new ArgumentException();
            }
        }
    }
}