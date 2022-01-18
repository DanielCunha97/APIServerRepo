using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Application.Query.Providers
{
    public interface IQuestionProvider
    {
        Task<List<QuestionDto>> GetAllAsync();

        Task<QuestionDto> GetAsync(string questionId);
    }
}
