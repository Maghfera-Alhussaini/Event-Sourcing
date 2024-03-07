
using MediatR;
namespace Anis.TrainingProject.Commands.RemoveMember
{
    public class RemoveMemberCommand: IRequest<Response>
    {
        public required string AccountId {  get; init; }
        public required string SubscriptionId {  get; init; }
        public required string MemberId {  get; init; }
        public required string OwnerId {  get; init; }
        public required string UserId {  get; init; }

    }
   
}
