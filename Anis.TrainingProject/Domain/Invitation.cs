using Anis.TrainingProject.Abstractions;
using Anis.TrainingProject.Commands.SendInvitation;
using Anis.TrainingProject.Events;
using Anis.TrainingProject.Extensions;
using Anis.TrainingProject.Exceptions;
using Anis.TrainingProject.Infrastructure.Persistance;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Anis.TrainingProject.Commands.CancelInvitation;
using Anis.TrainingProject.Commands.AcceptInvitation;
using Anis.TrainingProject.Commands.RejectInvitation;
using System.Runtime.CompilerServices;
using Anis.TrainingProject.Commands.JoinMember;
using System.Numerics;
using Anis.TrainingProject.Commands.RemoveMember;
using Anis.TrainingProject.Commands.Leave;
using Anis.TrainingProject.Commands.ChangePermission;

namespace Anis.TrainingProject.Domain
{
    public class Invitation
    {
        public string Id { get; set; }
        public bool IsPending { get; set; } = false;
        public bool IsMember {  get; set; }=false;
        public List<Event> _uncommittedEvents { get; } = [];

        public IReadOnlyList<Event> GetUncommittedEvents() => _uncommittedEvents;

        private Invitation()
        {
            Id = Guid.NewGuid().ToString();
        }
     
        public void ApplyChanges(Event @event)
        {
             Mutate(@event);
            _uncommittedEvents.Add(@event);

            if(@event is InvitationAccepted)
            {
               var @joinevent= EventExtensions.ToJoinEvent((InvitationAccepted)@event);
                _uncommittedEvents.Add(@joinevent);
            }
           
        }
      
             
        public static Invitation SendInvitation(SendInvitationCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
            return invitation;

        }
        public static Invitation CancelInvitation(CancelInvitationCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
            return invitation;

        }
        public static Invitation AcceptInvitation(AcceptInvitationCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
           
            return invitation;

        }
        public static Invitation RejectInvitation(RejectInvitationCommand command)
        {
           var invitation = new Invitation();
           invitation.ApplyChanges(command.ToEvent());
           return invitation;

        }
        public static Invitation JoinMember(JoinMemberCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
            return invitation;

        }
        public static Invitation RemoveMember(RemoveMemberCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
            return invitation;

        }
        public static Invitation Leave(LeaveCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
            return invitation;

        }
        public static Invitation ChangePermission(ChangePermissionCommand command)
        {
            var invitation = new Invitation();
            invitation.ApplyChanges(command.ToEvent());
            return invitation;

        }


        public static bool IsValid(SendInvitationCommand command, Event @event)
        {

            switch (@event)
            {
                case InvitationSent invitationSent: throw new AlreadyExistsException("This invitation is pending..");
                case InvitationAccepted invitationAccepted: throw new AlreadyExistsException("This user is a member in your subscription");
                case InvitationCancelled invitationCancelled:
                case InvitationRejected invitationRejected:
                    return true;
                default:
                    return false;

            }
        }
        public static bool IsValid(CancelInvitationCommand command, Event @event)
        {

            switch (@event)
            {
                case InvitationSent invitationSent:
                    return true;
                case InvitationCancelled invitationCancelled:
                    throw new AlreadyExistsException("you've already cancelled the invitation");
                default:
                    return false;

            }
        }
          public static bool IsValid(AcceptInvitationCommand command, Event @event)
          {
            switch (@event)
            {
                case InvitationSent invitationSent:
                    return true;
                case InvitationAccepted invitationAccepted :
                    throw new AlreadyExistsException("you've already accept the invitation");
                default:
                    return false;

            }
        }
         public static bool IsValid(RejectInvitationCommand command, Event @event)
         {
            switch (@event)
            {
                case InvitationSent invitationSent:
                    return true;
                case InvitationRejected invitationRejected:
                    throw new AlreadyExistsException("you've already rejected the invitation");
                default:
                    return false;

            }
         }
        public static bool IsValid(JoinMemberCommand command, Event @event)
        {
            switch (@event)
            {
                case MemberRemoved memberRemoved:
                case MemberLeft memberLeft:
                case InvitationRejected invitationRejected:
                case InvitationCancelled invitationCancelled:
                case InvitationSent invitationSent:
                    return true;
                case MemberJoined memberJoined:
                case InvitationAccepted invitationAccepted:
                case PermissionChanged permissionChanged:
                    throw new AlreadyExistsException("This user is already a member");
                default:
                    return false;

            }
        }
        public static bool IsValid(RemoveMemberCommand command, Event @event)
        {
            switch (@event)
            {              
                case MemberJoined memberJoined:
                case InvitationAccepted invitationAccepted:
                    return true;
                case MemberRemoved memberRemoved:
                    throw new AlreadyExistsException("You've already remove this user");
                default:
                    return false;

            }
        }
        public static bool IsValid(LeaveCommand command, Event @event)
        {
            switch (@event)
            {
                case MemberJoined memberJoined:
                case InvitationAccepted invitationAccepted:
                    return true;
                case MemberLeft memberLeft:
                    throw new AlreadyExistsException("You've already left");
                default:
                    return false;

            }
        }
        public static bool IsValid(ChangePermissionCommand command, Event @event)
        {
            switch (@event)
            {
                case MemberJoined memberJoined:
                case InvitationAccepted invitationAccepted:
                case PermissionChanged permissionChanged:
                    return true;
                default:
                    return false;

            }
        }
        protected void Mutate(Event @event)
        {
            switch (@event)
            {
                case InvitationSent e:
                    Mutate(e);
                    break;
                case InvitationAccepted e:
                    Mutate(e);
                    break;
                case InvitationCancelled e:
                    Mutate(e);
                    break;
                case InvitationRejected e:
                    Mutate(e);
                    break;
                case MemberJoined e:
                    Mutate(e);
                    break;
                case MemberRemoved e:
                    Mutate(e);
                    break;
                case MemberLeft e:
                    Mutate(e);
                    break;
                case PermissionChanged e:
                    break;
            }
        }
        public void Mutate(InvitationSent @event)
        {
            IsPending = true;
            IsMember = false;
        }
        public void Mutate(InvitationAccepted @event)
        {
            IsPending = false;
            IsMember = true;
        }
        public void Mutate(InvitationCancelled @event)
        {
            IsPending = false;
            IsMember = false;
        }
        public void Mutate(InvitationRejected @event)
        {
            IsPending = false;
            IsMember = false;
        }
        public void Mutate(MemberJoined @event)
        {
            IsPending = false;
            IsMember = true;
        }
        public void Mutate(MemberRemoved @event)
        {
            IsPending = false;
            IsMember = false;
        }
        public void Mutate(MemberLeft @event)
        {
            IsPending = false;
            IsMember = false;
        }

    }
    }

