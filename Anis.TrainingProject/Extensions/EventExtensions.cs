using Anis.TrainingProject.Commands.SendInvitation;
using Anis.TrainingProject.Commands.CancelInvitation;
using Anis.TrainingProject.Commands.AcceptInvitation;
using Anis.TrainingProject.Events;
using Anis.TrainingProject.Extentions;
using System.Runtime.CompilerServices;
using Anis.TrainingProject.Commands.RejectInvitation;
using Anis.TrainingProject.Commands.JoinMember;
using Anis.TrainingProject.Commands.RemoveMember;
using Anis.TrainingProject.Commands.Leave;
using Anis.TrainingProject.Commands.ChangePermission;

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
                  UserId: command.UserId,
                  PermissionType: command.PermissionType
              ),
              UserId: command.UserId,
              Version: 1
          );
        public static MemberJoined ToJoinEvent(this InvitationAccepted @event) => new(
             AggregateId: @event.AggregateId,
             Sequence: @event.Sequence+1,
             DateTime: DateTime.UtcNow,
             Data: new MemberJoinedData(
                 AccountId: @event.Data.AccountId,
                 SubscriptionId: @event.Data.SubscriptionId,
                 MemberId: @event.Data.MemberId,
                 OwnerId: @event.Data.UserId,
                 UserId: @event.Data.UserId,
                 PermissionType: @event.Data.PermissionType
             ),
             UserId: @event.UserId,
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
        public static MemberJoined ToEvent(this JoinMemberCommand command) => new(
            AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
            Sequence: 1,
            DateTime: DateTime.UtcNow,
            Data: new MemberJoinedData(
                AccountId: command.AccountId,
                SubscriptionId: command.SubscriptionId,
                MemberId: command.MemberId,
                OwnerId: command.OwnerId,
                UserId: command.UserId,
                PermissionType: command.PermissionType
            ),
            UserId: command.UserId,
            Version: 1
        );
        public static MemberRemoved ToEvent(this RemoveMemberCommand command) => new(
          AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
          Sequence: 1,
          DateTime: DateTime.UtcNow,
          Data: new MemberRemovedData(
              AccountId: command.AccountId,
              SubscriptionId: command.SubscriptionId,
              MemberId: command.MemberId,
              OwnerId: command.OwnerId,
              UserId: command.UserId
           
          ),
          UserId: command.UserId,
          Version: 1
      );
        public static MemberLeft ToEvent(this LeaveCommand command) => new(
         AggregateId: $"{command.SubscriptionId}_{command.UserId}",
         Sequence: 1,
         DateTime: DateTime.UtcNow,
         Data: new MemberLeftData(
             AccountId: command.AccountId,
             SubscriptionId: command.SubscriptionId,
             UserId: command.UserId

         ),
         UserId: command.UserId,
         Version: 1
     );
        public static PermissionChanged ToEvent(this ChangePermissionCommand command) => new(
        AggregateId: $"{command.SubscriptionId}_{command.MemberId}",
        Sequence: 1,
        DateTime: DateTime.UtcNow,
        Data: new PermissionChangedData(
            AccountId: command.AccountId,
            SubscriptionId: command.SubscriptionId,
            MemberId: command.MemberId,
            OwnerId: command.OwnerId,
            UserId: command.UserId,
            PermissionType: command.PermissionType

        ),
        UserId: command.UserId,
        Version: 1
    );
    }
}
