using System;
using System.Collections.Generic;
using System.Text;

namespace APIServer.Domain
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get; set;
        }
    }
}
