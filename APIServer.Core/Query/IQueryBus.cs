using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Core.Query
{
    public interface IQueryBus
    {
        TQueryResult Send<TQuery, TQueryResult>(TQuery query) where TQuery : IQuery;
    }
}
