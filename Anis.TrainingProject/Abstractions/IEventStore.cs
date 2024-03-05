
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Events;

namespace Anis.TrainingProject.Abstractions
{
    public interface IEventStore
    {
      
        Task<Event?> GetLastEventByAggregateIdAsync(string aggregateId, CancellationToken cancellationToken);
        Task CommitAsync(Invitation invitation, CancellationToken cancellationToken);
    }
}
