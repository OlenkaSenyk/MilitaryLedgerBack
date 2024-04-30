using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder) 
        {
            builder.ToTable("News");
            builder.HasKey(news => news.Id);
            builder.Property(news => news.Id).ValueGeneratedOnAdd();
            builder.Property(news => news.Title).HasMaxLength(500).IsRequired();
            builder.Property(news => news.Description).IsRequired();
            builder.Property(news => news.Type).HasMaxLength(50).IsRequired();
            builder.Property(news => news.Date).IsRequired();
            builder.Property(news => news.CreatedAt).IsRequired();
            builder.Property(news => news.CreatedById).IsRequired();
            builder.Property(news => news.LastUpdatedAt).IsRequired();
            builder.Property(news => news.LastUpdatedById).IsRequired();
        }
    }
}
