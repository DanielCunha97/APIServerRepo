using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Application.WebAPI.Models
{
    public class PutQuestionRequest
    {
        public Guid Id
        {
            get; set;
        }
        public string Image_url
        {
            get; set;
        }
        public string Thumb_url
        {
            get; set;
        }
        public string Question
        {
            get; set;
        }
        public IList<ChoiceRequest> Choices
        {
            get; set;
        }

        public class ChoiceRequest
        {
            public string Choice
            {
                get; set;
            }
            public int Votes
            {
                get; set;
            }
        }

    }
}
