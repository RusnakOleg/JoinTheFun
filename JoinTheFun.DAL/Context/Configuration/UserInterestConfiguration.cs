using JoinTheFun.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.DAL.Context.Configuration
{
    public class UserInterestConfiguration : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder.HasKey(ui => new { ui.InterestId, ui.ProfileId }); // Складений ключ для InterestId та ProfileId

            builder.HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.InterestId)
                .OnDelete(DeleteBehavior.Cascade); // При видаленні інтересу, видаляються зв'язки з користувачами

            builder.HasOne(ui => ui.Profile)
                .WithMany(p => p.Interests)
                .HasForeignKey(ui => ui.ProfileId)
                .OnDelete(DeleteBehavior.Cascade); // При видаленні профілю, видаляються зв'язки з інтересами
        }
    }
}
