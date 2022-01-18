using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Application.Core.Query
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}
