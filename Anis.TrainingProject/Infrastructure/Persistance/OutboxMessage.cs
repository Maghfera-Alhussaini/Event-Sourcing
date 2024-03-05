using Anis.TrainingProject.Events;

namespace Anis.TrainingProject.Infrastructure.Persistance
{
    public class OutboxMessage
    {
        private OutboxMessage(int id)
        {
            Id = id;
        }

        public OutboxMessage(Event @event)
        {
            Event = @event;
        }

        public int Id { get; private set; }
        public Event? Event { get; private set; }
    }
}
