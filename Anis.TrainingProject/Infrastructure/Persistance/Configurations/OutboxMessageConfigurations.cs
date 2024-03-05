using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anis.TrainingProject.Infrastructure.Persistance.Configurations
{
    public class OutboxMessageConfigurations: IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasOne(x => x.Event)
                .WithOne()
                .HasForeignKey<OutboxMessage>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
