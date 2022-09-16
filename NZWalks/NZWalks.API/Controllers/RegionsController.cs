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

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
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

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        // Create Method

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetRegionsById")]
        public async Task<IActionResult> GetRegionsById(Guid id)
        {
            var region = await regionRepository.Get(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);

        }




       [HttpPost]
       public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request to Domain Model

            var region= new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            }; 
            //Pass details to repository

            region=await regionRepository.AddAsync(region);
            //Convert back to Dto

            var regionDTO = new Models.DTO.Region
            {
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population

            };

            return CreatedAtAction(nameof(GetRegionsById), new { id = regionDTO.Id }, regionDTO);

        }



        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteAsync( Guid id)
        {
            //Get Region from Database
            var region=await regionRepository.Delete(id);
            // if null not found

            if(region==null)
            {
                return NotFound();
            }
            // Convert response back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id=region.Id,
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            // return response
            return Ok(regionDTO);

        }




        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region = new Models.Domain.Region
            {
               // Code = updateRegionRequest.Code,
               
                // Convert DTO to Domain model
                Area = updateRegionRequest.Area,
                Name = updateRegionRequest.Name,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population

            };

            //Update region from Repository
          region =await regionRepository.Update(id, region);

            //if null then not found

            if(region== null)
            {
                return NotFound();
            }


            // Convert Domain back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population

            };


            return Ok(regionDTO);




        }


    }

}
