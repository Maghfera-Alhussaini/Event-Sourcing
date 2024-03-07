using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using Anis.TrainingProject.Exceptions;
using MediatR;




namespace Anis.TrainingProject.Commands.RemoveMember
{
    public class RemoveMemberCommandHandler(IEventStore eventStore): IRequestHandler<RemoveMemberCommand,Response> 
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<Response> Handle(RemoveMemberCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.MemberId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);
            if (lastEvent == null)
                throw new NotFoundException("Not found member");

            else if (Invitation.IsValid(command, lastEvent))
            {
                var invitation = Invitation.RemoveMember(command);
                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response
                {
                    Id = invitation.Id,
                    Message = "Member removed successfully"
                };


            }
            throw new InvalidOperationException("Failed to remove member: invalid invitation state.");

        }
        }
}
