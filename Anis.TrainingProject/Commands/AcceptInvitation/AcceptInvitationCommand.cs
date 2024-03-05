using MediatR;
namespace Anis.TrainingProject.Commands.AcceptInvitation
{
    public class AcceptInvitationCommand: IRequest<Response>
    {
        public required string AccountId {  get; init; }
        public required string SubscriptionId {  get; init; }
        public required string MemberId {  get; init; }
        public required string UserId {  get; init; }
      
    }
   
}
