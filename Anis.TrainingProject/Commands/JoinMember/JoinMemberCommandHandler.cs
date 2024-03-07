using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using Anis.TrainingProject.Exceptions;
using MediatR;




namespace Anis.TrainingProject.Commands.JoinMember
{
    public class JoinMemberCommandHandler(IEventStore eventStore): IRequestHandler<JoinMemberCommand,Response> 
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<Response> Handle(JoinMemberCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.MemberId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);
            if (lastEvent is null || Invitation.IsValid(command, lastEvent))
            {
                var invitation = Invitation.JoinMember(command);
                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response {
                Id=invitation.Id,
                Message= "Member Joined successfully"
                };

            }
            throw new InvalidOperationException("Failed to join member: invalid invitation state.");

        }
        }
}
