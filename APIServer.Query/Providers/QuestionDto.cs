using APIServer.Query.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Application.Query.Providers
{
    public class QuestionDto
    {
        public Guid Id
        {
            get; set;
        }
        public string QuestionText
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
        public IList<ChoiceDto> Choices
        {
            get; set;
        }
    }
}
