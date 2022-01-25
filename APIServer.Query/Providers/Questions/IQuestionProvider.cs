using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Application.Query.Providers
{
    public interface IQuestionProvider
    {
        Task<List<QuestionDto>> GetAllAsync(Tuple<int, int, string> parameters);

        Task<QuestionDto> GetAsync(Guid questionId);
    }
}
