using APIServer.Application.Core.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Application.Query.Handlers.Questions
{
    public class QuestionsQueryHandler :
        IQueryHandler<GetQuestionsQuery, Task<List<QuestionDto>>
    {
        private readonly IBillingQueryProvider _customerQueryProvider;

        public QuestionsQueryHandler(IBillingQueryProvider customerQueryProvider)
        {
            _customerQueryProvider = customerQueryProvider;
        }

        public async virtual Task<BillingPreviewDto> Handle(GetBillingPreviewQuery query)
        {
            var customer = await _customerQueryProvider.GetAsync(query.BillingPreviewRunId);

            return customer;
        }
    }
}
