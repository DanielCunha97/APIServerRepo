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
                .Set<QuestionEntity>()
                .Include(it => it.Choices).ToListAsync();

            return dtos.Select(it => Convert(it)).ToList();
        }

        public Task<QuestionDto> GetAsync(string questionId)
        {
            throw new NotImplementedException();
        }

        protected virtual QuestionDto Convert(QuestionEntity question)
        {
            var dto = new QuestionDto
            {
                Id = question.QuestionID,
                Choices = question.Choices.Select(it => new APIServer.Query.Providers.ChoiceDto
                { 
                    ChoiceText = it.Choice,
                    Id = it.ChoiceID,
                    Votes = it.Votes
                }).ToList(),
                Image_url = question.ImageUrl,
                Published_at = question.PublishedAt,
                QuestionText = question.Question,
                Thumb_url = question.Thumb_url
            };

            return dto;
        }
    }
}
