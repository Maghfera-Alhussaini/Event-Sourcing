using Anis.TrainingProject.Commands.SendInvitation;
using Anis.TrainingProject.Commands.CancelInvitation ;
using System.Windows.Input;
using Anis.TrainingProject.Commands.AcceptInvitation;
using Anis.TrainingProject.Commands.RejectInvitation;
using Anis.TrainingProject.Commands.JoinMember;
using Anis.TrainingProject.Commands.RemoveMember;
using Anis.TrainingProject.Commands.Leave;
using Anis.TrainingProject.Commands.ChangePermission;


namespace Anis.TrainingProject.Extentions
{
    public static class CommandsExtensions
    {
        public static SendInvitationCommand ToCommand(this SendInvitationRequest request)
      => new()
      {
          AccountId= request.AccountId,
          SubscriptionId= request.SubscriptionId,
          MemberId= request.MemberId,
          UserId= request.UserId,
          PermissionType= request.PermissionType

      };
        public static CancelInvitationCommand ToCommand(this CancelInvitationRequest request)
     => new()
     {
         AccountId = request.AccountId,
         SubscriptionId = request.SubscriptionId,
         MemberId = request.MemberId,
         UserId = request.UserId
     };
        public static AcceptInvitationCommand ToCommand(this AcceptInvitationRequest request)
     => new()
     {
         AccountId = request.AccountId,
         SubscriptionId = request.SubscriptionId,
         MemberId = request.MemberId,
         UserId = request.UserId,
         PermissionType= request.PermissionType
     };
        public static RejectInvitationCommand ToCommand(this RejectInvitationRequest request)
   => new()
   {
       AccountId = request.AccountId,
       SubscriptionId = request.SubscriptionId,
       MemberId = request.MemberId,
       UserId = request.UserId
   };
        public static JoinMemberCommand ToCommand(this JoinMemberRequest request)
    => new()
     {
      AccountId = request.AccountId,
      SubscriptionId = request.SubscriptionId,
      MemberId = request.MemberId,
      OwnerId = request.OwnerId,
      UserId = request.UserId,
      PermissionType = request.PermissionType
    };
        public static RemoveMemberCommand ToCommand(this RemoveMemberRequest request)
        => new()
        {
        AccountId = request.AccountId,
        SubscriptionId = request.SubscriptionId,
        MemberId = request.MemberId,
        OwnerId = request.OwnerId,
        UserId = request.UserId
   
  };
        public static LeaveCommand ToCommand(this LeaveRequest request)
      => new()
      {
          AccountId = request.AccountId,
          SubscriptionId = request.SubscriptionId,
          UserId = request.UserId

      };
        public static ChangePermissionCommand ToCommand(this ChangePermissionRequest request)
     => new()
     {
         AccountId = request.AccountId,
         SubscriptionId = request.SubscriptionId,
         MemberId = request.MemberId,
         OwnerId = request.OwnerId,
         UserId = request.UserId,
         PermissionType = request.PermissionType

     };





    }
}
