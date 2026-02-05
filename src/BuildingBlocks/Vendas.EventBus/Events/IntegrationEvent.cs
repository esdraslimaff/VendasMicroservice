using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.EventBus.Events
{
    public abstract record IntegrationEvent
    {
        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
