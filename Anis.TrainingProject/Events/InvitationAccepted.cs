namespace Anis.TrainingProject.Events
{
    public record InvitationAccepted(
      string AggregateId,
      int Sequence,
      DateTime DateTime,
      InvitationAcceptedData Data,
      string UserId,
      int Version
      ) : Event<InvitationAcceptedData>(AggregateId, Sequence, DateTime, Data, UserId, Version);
    public record InvitationAcceptedData(
       string AccountId,
       string SubscriptionId,
       string MemberId,
       string UserId

  );
}
