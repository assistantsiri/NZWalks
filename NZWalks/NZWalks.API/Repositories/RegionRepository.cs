using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);  // here compulsary to add Add with aync
            await nZWalksDbContext.SaveChangesAsync();  
            return region;


        }

        public async Task<Region> Delete(Guid id)
        {
             var region=await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<Region> Get(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public  async Task<IEnumerable<Region>> GetAll()
        {
             return await nZWalksDbContext.Regions.ToListAsync();


        }

        public async Task<Region> Update(Guid id, Region region)
        {
           
            var Existingregion=await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(Existingregion == null)
            {
                return null; 
            }
            Existingregion.Code=region.Code;
            Existingregion.Name=region.Name;
            Existingregion.Area=region.Area;
            Existingregion.Lat=region.Lat;
            Existingregion.Long=region.Long;
            Existingregion.Population=region.Population;


            await nZWalksDbContext.SaveChangesAsync();

            return Existingregion;
        }
        

        /* Task<IEnumerable<Region>> IRegionRepository.GetAll()
         {
             throw new NotImplementedException();
         }*/
    }
}
