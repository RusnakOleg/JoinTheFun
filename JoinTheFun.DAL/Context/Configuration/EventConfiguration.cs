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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId); // Визначення основного ключа

            builder.Property(e => e.Title)
                .HasMaxLength(255) // Обмеження на максимальну кількість символів
                .IsRequired();

            builder.HasOne(e => e.Creator) // Кожна подія належить одному користувачу
                .WithMany(u => u.CreatedEvents) // Кожен користувач може створити багато подій
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Cascade); // При видаленні користувача, видаляються всі його події
        }
    }
}
