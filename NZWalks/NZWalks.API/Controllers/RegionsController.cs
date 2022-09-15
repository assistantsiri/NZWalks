using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository , IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public  async Task<IActionResult> GetAllRegions()
        {
            /* var regions = new List<Region>()
             {
                 new Region
                 {
                     Id=Guid.NewGuid(),
                     Name="WellingTone",
                     Code="WLG",
                     Area=227755,
                     Lat=-1.234,
                     Long=299.56,
                     Population=5000000

                 },
                 new Region
                 {
                     Id=Guid.NewGuid(),
                     Name="AuckLand",
                     Code="AKD",
                     Area=220055,
                     Lat=-2.234,
                     Long=399.56,
                     Population=6000000

                 },

             };*/

           var regions = await regionRepository.GetAll();




            // Return DTO Regions

           /* var regionsDTO=new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    //Id = region.Id
                    Id=region.Id,
                    Code = region.Code,
                     Name= region.Name,
                     Lat = region.Lat,
                     Area = region.Area,
                     Long = region.Long,
                     Population = region.Population,

                };
                regionsDTO.Add(regionDTO);
            }); */

            var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }
       
    }
}
