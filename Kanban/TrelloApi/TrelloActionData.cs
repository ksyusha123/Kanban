using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApi
{
    public class TrelloActionData
    {
        public TrelloBoard Board { get; set; }
        public TrelloList List { get; set; }
        public TrelloCard Card { get; set; }
        public string Text { get; set; }
    }
}
