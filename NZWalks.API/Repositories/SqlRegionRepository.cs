using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SqlRegionRepository :  IRegionRepository
    {
        private readonly INDWalksDBContext dbContext;

        public SqlRegionRepository(INDWalksDBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
           await dbContext.Regions.AddAsync(region);
           await dbContext.SaveChangesAsync();
          return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=> x.ID==id);
            if (existingRegion == null) { return null; }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.ID==id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionDomainModel =  await dbContext.Regions.FirstOrDefaultAsync(x => x.ID == id);
            if (regionDomainModel == null)
            {
                return null;
            }

            //map dto to the domain model 
            regionDomainModel.Code = region.Code;
            regionDomainModel.Name = region.Name;
            regionDomainModel.RegionImageURL = region.RegionImageURL;

            await dbContext.SaveChangesAsync();
            return regionDomainModel;

        }
    }
}
