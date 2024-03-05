using Anis.TrainingProject.Commands.SendInvitation;
using Anis.TrainingProject.Commands.CancelInvitation ;
using System.Windows.Input;
using Anis.TrainingProject.Commands.AcceptInvitation;
using Anis.TrainingProject.Commands.RejectInvitation;

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
         UserId = request.UserId
     };
        public static RejectInvitationCommand ToCommand(this RejectInvitationRequest request)
   => new()
   {
       AccountId = request.AccountId,
       SubscriptionId = request.SubscriptionId,
       MemberId = request.MemberId,
       UserId = request.UserId
   };




    }
}
