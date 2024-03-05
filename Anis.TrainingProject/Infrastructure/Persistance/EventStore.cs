using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Events;
using Anis.TrainingProject.Infrastructure.MessageBus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq;

namespace Anis.TrainingProject.Infrastructure.Persistance
{
    public class EventStore(ApplicationDbContext context, ServiceBusPublisher publisher) : IEventStore
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ServiceBusPublisher _publisher = publisher;

        public async Task CommitAsync(Invitation invitation, CancellationToken cancellationToken)
        {

            var events = invitation.GetUncommittedEvents();
            var messages = events.Select(x => new OutboxMessage(x));
            await _context.Events.AddRangeAsync(events, cancellationToken);
            await _context.OutboxMessages.AddRangeAsync(messages, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _publisher.StartPublishing();
        }

        public async Task<Event?> GetLastEventByAggregateIdAsync(string aggregateId, CancellationToken cancellationToken)
        {
          
             return await _context.Events
                .Where(e => e.AggregateId == aggregateId)
                .OrderByDescending(e => e.DateTime)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
              
            
            
        }

    }
}
