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

namespace Anis.TrainingProject.Domain
{
    public class Invitation
    {
        public string Id { get; set; }
        public bool IsPending { get; set; } = false;
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
        protected void Mutate(Event @event)
        {
            switch(@event)
            {
                case InvitationSent invitationSent:
                    IsPending= true;
                    break;
                default:
                    IsPending = false;
                    break;

            }
        }


    }
    }

