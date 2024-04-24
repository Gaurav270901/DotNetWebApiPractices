using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class INDWalksAuthDbContext : IdentityDbContext
    {
        public INDWalksAuthDbContext(DbContextOptions<INDWalksAuthDbContext> options ):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var ReaderId = "6cea37bb-8a37-4842-ad4c-8020b2061dd6";
            var WriterID = "5ccd02ff-1d60-4b2d-8d3a-1c2be4127c7e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ReaderId,
                    ConcurrencyStamp=ReaderId,
                    Name = "Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriterID,
                    ConcurrencyStamp=WriterID,
                    Name = "Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
