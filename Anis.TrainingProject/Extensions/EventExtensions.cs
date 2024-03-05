using Anis.TrainingProject.Commands.SendInvitation;
using Anis.TrainingProject.Commands.CancelInvitation;
using Anis.TrainingProject.Commands.AcceptInvitation;
using Anis.TrainingProject.Events;
using Anis.TrainingProject.Extentions;
using System.Runtime.CompilerServices;
using Anis.TrainingProject.Commands.RejectInvitation;

namespace Anis.TrainingProject.Extensions
{
    public static class EventExtensions
    {
       
        public static InvitationSent ToEvent(this SendInvitationCommand command) => new(
               AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
               Sequence: 1,
               DateTime: DateTime.UtcNow,
               Data: new InvitationSentData(
                   AccountId: command.AccountId,
                   SubscriptionId: command.SubscriptionId,
                   MemberId: command.MemberId,
                   UserId: command.UserId,
                   PermissionType: command.PermissionType
              
               ),
               UserId: command.UserId,
               Version: 1
           );

        public static InvitationCancelled ToEvent(this CancelInvitationCommand command) => new(
              AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
              Sequence: 1,
              DateTime: DateTime.UtcNow,
              Data: new InvitationCancelledData(
                  AccountId: command.AccountId,
                  SubscriptionId: command.SubscriptionId,
                  MemberId: command.MemberId,
                  UserId: command.UserId
              ),
              UserId: command.UserId,
              Version: 1
          );
        public static InvitationAccepted ToEvent(this AcceptInvitationCommand command) => new(
              AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
              Sequence: 1,
              DateTime: DateTime.UtcNow,
              Data: new InvitationAcceptedData(
                  AccountId: command.AccountId,
                  SubscriptionId: command.SubscriptionId,
                  MemberId: command.MemberId,
                  UserId: command.UserId
              ),
              UserId: command.UserId,
              Version: 1
          );
       public static InvitationRejected ToEvent(this RejectInvitationCommand command) => new(
              AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
              Sequence: 1,
              DateTime: DateTime.UtcNow,
              Data: new InvitationRejectedData(
                  AccountId: command.AccountId,
                  SubscriptionId: command.SubscriptionId,
                  MemberId: command.MemberId,
                  UserId: command.UserId
              ),
              UserId: command.UserId,
              Version: 1
          );
    }
}
