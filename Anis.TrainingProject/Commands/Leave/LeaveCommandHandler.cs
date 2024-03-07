using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using Anis.TrainingProject.Exceptions;
using MediatR;


namespace Anis.TrainingProject.Commands.Leave
{
    public class LeaveCommandHandler(IEventStore eventStore): IRequestHandler<LeaveCommand,Response> 
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<Response> Handle(LeaveCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.UserId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);
            if (lastEvent == null)
                throw new NotFoundException("This user is not a member");

            else if (Invitation.IsValid(command, lastEvent))
            {
                var invitation = Invitation.Leave(command);
                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response
                {
                    Id = invitation.Id,
                    Message = "You left successfully"
                };
           
        }
            throw new InvalidOperationException("Failed to join member: invalid invitation state.");

        }
        }
}
