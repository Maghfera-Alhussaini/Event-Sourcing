using MediatR;
namespace Anis.TrainingProject.Commands.SendInvitation
{
    public class SendInvitationCommand: IRequest<Response>
    {
        public required string AccountId {  get; init; }
        public required string SubscriptionId {  get; init; }
        public required string MemberId {  get; init; }
        public required string UserId {  get; init; }
        public required PermissionType PermissionType {  get; init; }
    }
   
}
