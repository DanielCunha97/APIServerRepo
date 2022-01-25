using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Core.Query
{
    public class InProcessQueryBus : IQueryBus
    {
        private readonly Func<Type, IQueryHandler> _getQueryHandler;

        public InProcessQueryBus(Func<Type, IQueryHandler> getQueryHandler)
        {
            _getQueryHandler = getQueryHandler;
        }

        public virtual TQueryResult Send<TQuery, TQueryResult>(TQuery query) where TQuery : IQuery
        {
            var queryHandler = (IQueryHandler<TQuery, TQueryResult>)_getQueryHandler(typeof(IQueryHandler<TQuery, TQueryResult>));

            if (queryHandler == null)
            {
                throw new InvalidOperationException("Get Query Handler Error(Query handler is null)");
            }

            return queryHandler.Handle(query);
        }
    }
}
