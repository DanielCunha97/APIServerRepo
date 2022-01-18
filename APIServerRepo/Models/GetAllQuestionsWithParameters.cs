using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Application.WebAPI.Models
{
    public class GetAllQuestionsWithParameters
    {
        public int Limit { get; set; }


        public int Offset { get; set; }


        public string Filter { get; set; }
    }
}
