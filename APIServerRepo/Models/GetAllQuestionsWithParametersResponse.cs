using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Application.WebAPI.Models
{
    public class GetAllQuestionsWithParametersResponse
    {
        public IList<QuestionModel> Property1
        {
            get; set;
        }

        public class QuestionModel
        {
            public int Id
            {
                get; set;
            }
            public string Question
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
            public DateTime Published_at
            {
                get; set;
            }
            public IList<ChoiceModel> Choices
            {
                get; set;
            }
        }

        public class ChoiceModel
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
