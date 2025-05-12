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
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.Id); // Визначення основного ключа

            builder.Property(p => p.Description)
                .HasMaxLength(500) // Обмеження на максимальну кількість символів
                .IsRequired(); // Це поле обов'язкове

            builder.HasOne(p => p.ApplicationUser)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // При видаленні користувача, видаляється і профіль
        }
    }
}
