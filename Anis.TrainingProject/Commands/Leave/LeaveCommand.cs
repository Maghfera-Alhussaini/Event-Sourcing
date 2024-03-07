using MediatR;
namespace Anis.TrainingProject.Commands.Leave
{
    public class LeaveCommand: IRequest<Response>
    {
        public required string AccountId {  get; init; }
        public required string SubscriptionId {  get; init; }       
        public required string UserId {  get; init; }

    }
   
}
