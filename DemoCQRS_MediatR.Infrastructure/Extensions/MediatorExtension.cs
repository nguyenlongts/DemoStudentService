using DemoCQRS_MediatR.Domain;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCQRS_MediatR.Infrastructure.Extensions
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventAsync(this IMediator mediator,ModelContext _context)
        {
            var domainEntities = _context.ChangeTracker.Entries<BaseEntity>().Where(x=>x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
            var domainEvents = domainEntities.SelectMany(x=>x.Entity.DomainEvents).ToList();
            if (domainEvents == null || domainEvents.Count() == 0) return;
            domainEntities.ToList().ForEach(entity=>entity.Entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
