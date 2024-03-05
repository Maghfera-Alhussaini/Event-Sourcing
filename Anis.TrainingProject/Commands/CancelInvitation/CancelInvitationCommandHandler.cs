using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using Anis.TrainingProject.Exceptions;
using MediatR;



namespace Anis.TrainingProject.Commands.CancelInvitation
{
    public class CancelInvitationCommandHandler(IEventStore eventStore): IRequestHandler<CancelInvitationCommand, Response> 
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<Response> Handle(CancelInvitationCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.MemberId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);
            if (lastEvent == null)
                throw new NotFoundException("No invtation has sent");

            else if (Invitation.IsValid(command, lastEvent)) {
                var invitation = Invitation.CancelInvitation(command);
                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response {
                Id=invitation.Id,
                Message= "invitation Cancelled successfully"
                };

            }
            throw new InvalidOperationException("Failed to cancel invitation: invalid invitation state.");

        }
        }
}
