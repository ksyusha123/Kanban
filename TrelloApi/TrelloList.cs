using System;
using System.Collections.Generic;
using System.Text;

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
}
