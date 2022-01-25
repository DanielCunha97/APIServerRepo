using APIServer.Application.Query.Providers;
using APIServer.Persistence.Context;
using APIServer.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.Persistence.Query
{
    public class QuestionProvider : IQuestionProvider
    {
        private readonly ApplicationContext _dataContext;

        public QuestionProvider(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async virtual Task<List<QuestionDto>> GetAllAsync(Tuple<int, int, string> parameters)
        {
            var dtos = await _dataContext
                .Set<Question>()
                .Include(it => it.Choices).ToListAsync();

            return dtos.Select(it => Convert(it)).ToList();
        }

        public Task<QuestionDto> GetAsync(string questionId)
        {
            throw new NotImplementedException();
        }

        protected virtual QuestionDto Convert(Question question)
        {
            var dto = new QuestionDto
            {
                Id = question.Id,
                Choices = question.Choices.Select(it => new APIServer.Query.Providers.ChoiceDto
                { 
                    ChoiceText = it.ChoiceText,
                    Id = it.Id,
                    Votes = it.Votes
                }).ToList(),
                Image_url = question.Image_url,
                Published_at = question.Published_at,
                QuestionText = question.QuestionText,
                Thumb_url = question.Thumb_url
            };

            return dto;
        }
    }
}
