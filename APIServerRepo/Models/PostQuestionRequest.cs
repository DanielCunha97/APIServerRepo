using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Application.WebAPI.Models
{
    public class PostQuestionRequest
    {
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
        public List<string> Choices
        {
            get; set;
        }

    }
}
