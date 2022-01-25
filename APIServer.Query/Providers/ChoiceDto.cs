using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Query.Providers
{
    public class ChoiceDto
    {
        public Guid Id
        {
            get; set;
        }

        public string ChoiceText
        {
            get; set;
        }
        public int Votes
        {
            get; set;
        }
    }
}
