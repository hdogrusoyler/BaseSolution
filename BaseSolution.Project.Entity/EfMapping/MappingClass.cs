using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Project.Entity.EfMapping
{
    public class TitleMap : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.Property(e => e.Text)
                   .IsRequired();

            builder.HasOne(c => c.Category)
                .WithMany(u => u.Titles)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); //NoAction //Cascade //Restrict 
        }
    }

    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Text)
                   .IsRequired();

            builder.HasMany(b => b.Titles)       
                .WithOne(d => d.Category)            
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
    //dotnet ef migrations add ...
    //dotnet ef database update
}
