using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApi
{
    public class TrelloAction
    {
        public string Id { get; set; }
        public string IdMemberCreator { get; set; }
        public TrelloActionData Data { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
    }
}
