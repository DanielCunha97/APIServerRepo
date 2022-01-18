using APIServer.Application.Query.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Application.Persistence.Query
{
    public class QuestionProvider : IQuestionProvider
    {
        public Task<List<QuestionDto>> GetAllAsync()
        {
            // criar resource no azure
            // criar app service no azure
            // criar base de dados no azure
            // conecção com a base de dados e retornar as questions
        }

        public Task<QuestionDto> GetAsync(string questionId)
        {
            throw new NotImplementedException();
        }
    }
}
