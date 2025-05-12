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
    public class InterestConfiguration : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.HasKey(i => i.InterestId); // Визначення основного ключа

            builder.Property(i => i.Name)
                .HasMaxLength(100) // Максимальна довжина для назви інтересу
                .IsRequired(); // Це поле обов'язкове
        }
    }
}
