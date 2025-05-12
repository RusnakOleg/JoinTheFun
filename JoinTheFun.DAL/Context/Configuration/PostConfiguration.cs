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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostId); // Визначення основного ключа

            builder.Property(p => p.Content)
                .HasMaxLength(1000) // Обмеження на максимальну кількість символів
                .IsRequired(); // Це поле обов'язкове

            builder.HasOne(p => p.User) // Кожен пост належить одному користувачу
                .WithMany(u => u.Posts) // Кожен користувач може мати багато постів
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // При видаленні користувача, видаляються всі його пости
        }
    }
}
