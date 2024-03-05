using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using Anis.TrainingProject.Exceptions;
using MediatR;



namespace Anis.TrainingProject.Commands.AcceptInvitation
{
    public class AcceptInvitationCommandHandler(IEventStore eventStore): IRequestHandler<AcceptInvitationCommand, Response> 
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<Response> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.MemberId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);
            if (lastEvent is null)
                throw new NotFoundException("No invtation has sent");

            else if (Invitation.IsValid(command, lastEvent)) {
                var invitation = Invitation.AcceptInvitation(command);
                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response {
                Id=invitation.Id,
                Message= "invitation Cancelled successfully"
                };

            }
            throw new InvalidOperationException("Failed to accept invitation: invalid invitation state.");

        }
        }
}
