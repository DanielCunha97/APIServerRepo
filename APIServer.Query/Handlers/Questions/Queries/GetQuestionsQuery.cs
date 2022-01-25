using APIServer.Core.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Query.Handlers.Questions.Queries
{
    public class GetQuestionsQuery : QueryBase
    {
        public GetQuestionsQuery(int limit, int offset, string filter)
        {
            this.Filter = Filter;
            this.Limit = limit;
            this.Offset = offset;
        }

        public int Limit { get; set; }


        public int Offset {  get; set; }


        public string Filter { get; set; }
    }
}
