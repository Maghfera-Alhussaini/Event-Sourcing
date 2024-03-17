using Anis.TrainingProject.Extentions;
using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using MediatR;
using Polly;

namespace Anis.TrainingProject.Services
{
    public class InvitationsService(IMediator mediator, ILogger<InvitationsService> logger) : Invitations.InvitationsBase
    {
        private readonly IMediator _mediator=mediator;
        private readonly AsyncPolicy _policy = ConfigureRetries(logger);

       //send invitation 
        public override async Task<Response> SendInvitation(SendInvitationRequest request, ServerCallContext context)
        {
           
                var command = request.ToCommand();
                var response = await _policy.ExecuteAsync(() => _mediator.Send(command));
                return response;               
        }
        //cancel invitation
        public override async Task<Response> CancelInvitation(CancelInvitationRequest request, ServerCallContext context)
        {
          
                var command = request.ToCommand();
                var response = await _policy.ExecuteAsync(() => _mediator.Send(command));
                return response;
          
   
        }
        public override async Task<Response> AcceptInvitation(AcceptInvitationRequest request, ServerCallContext context)
        {
          
                var command = request.ToCommand();
                var response = await _policy.ExecuteAsync(() => _mediator.Send(command));
                return response;
                     
        }
        //JoinMember
        public override async Task<Response> JoinMember(JoinMemberRequest request, ServerCallContext context)
        {
          
                var command = request.ToCommand();
                var response = await _policy.ExecuteAsync(() => _mediator.Send(command));
                return response;
         

        }
        public override async Task<Response> RemoveMember(RemoveMemberRequest request, ServerCallContext context)
        {
          
                var command = request.ToCommand();
                var response = await _policy.ExecuteAsync(() => _mediator.Send(command));
                return response;
         

        }
        public override async Task<Response> Leave(LeaveRequest request, ServerCallContext context)
        {
          
                var command = request.ToCommand();
                var response = await _policy.ExecuteAsync(() => _mediator.Send(command));
                return response;
            
        

        }
        private static AsyncPolicy ConfigureRetries(ILogger logger) =>
          Policy.Handle<DbUpdateException>()
              .WaitAndRetryAsync(new[]
              {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3),
              }, onRetry: (exception, _, attempt, _) => logger.LogWarning(exception, "Call failed, Retry attempt: {RetryAttempt}", attempt));

    }
}
