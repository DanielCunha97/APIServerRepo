using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain.QuestionAggregate
{
    public interface IQuestionStoreObject
    {
        Guid Id
        {
            get;
        }
        string QuestionText
        {
            get;
        }
        string Image_url
        {
            get;
        }
        string Thumb_url
        {
            get;
        }
        DateTime Published_at
        {
            get; 
        }
    }
}
