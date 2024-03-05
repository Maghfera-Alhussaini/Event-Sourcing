using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using MediatR;



namespace Anis.TrainingProject.Commands.SendInvitation
{
    public class SendInvitationCommandHandler: IRequestHandler<SendInvitationCommand, Response> 
    {
        private readonly IEventStore _eventStore;
        public SendInvitationCommandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<Response> Handle(SendInvitationCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.MemberId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);

            if (lastEvent is null || Invitation.IsValid(command,lastEvent)) {
                var invitation = Invitation.SendInvitation(command);

                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response
                {
                    Id= invitation.Id,
                    Message= "invitation Sent successfully"
                };

            }
            throw new InvalidOperationException("Failed to send invitation: invalid invitation state.");

        }
        }
}
