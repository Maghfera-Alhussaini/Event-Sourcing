namespace Anis.TrainingProject.Events
{
    public record PermissionChanged(
     string AggregateId,
     int Sequence,
     DateTime DateTime,
     PermissionChangedData Data,
     string UserId,
     int Version
      ) : Event<PermissionChangedData>(AggregateId, Sequence, DateTime, Data, UserId, Version);
    public record PermissionChangedData(
      string AccountId,
      string SubscriptionId,
      string MemberId,
      string OwnerId,
      string UserId,
      PermissionType PermissionType

 );
}
