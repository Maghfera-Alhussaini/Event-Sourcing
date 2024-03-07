using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Domain;
using Anis.TrainingProject.Extentions;
using Anis.TrainingProject.Exceptions;
using MediatR;
using System.Diagnostics.Eventing.Reader;

namespace Anis.TrainingProject.Commands.ChangePermission
{
    public class ChangePermissionCommandHandler(IEventStore eventStore): IRequestHandler<ChangePermissionCommand,Response> 
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<Response> Handle(ChangePermissionCommand command, CancellationToken cancellationToken)
        {

            string aggregateId = $"{command.SubscriptionId}_{command.MemberId}";
            var lastEvent = await _eventStore.GetLastEventByAggregateIdAsync(aggregateId, cancellationToken);
            if (lastEvent is null)
            {
                throw new NotFoundException("User not found");
            }
                
            else if( Invitation.IsValid(command, lastEvent))
            {
                var invitation = Invitation.ChangePermission(command);
                await _eventStore.CommitAsync(invitation, cancellationToken);
                return new Response {
                Id=invitation.Id,
                Message= "Permission changed successfully"
                };

            }
            throw new InvalidOperationException("Failed to change permission: invalid invitation state.");

        }
        }
}
