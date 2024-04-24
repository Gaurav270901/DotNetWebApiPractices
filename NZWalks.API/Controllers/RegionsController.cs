using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly INDWalksDBContext dbContext;
        private readonly IMapper mapper;

        public IRegionRepository RegionRepository { get; }

        public RegionsController(INDWalksDBContext dBContext , IRegionRepository regionRepository , IMapper mapper) 
        {
            this.dbContext = dBContext;
            RegionRepository = regionRepository;
            this.mapper = mapper;
        }

        //while making the method asynchronous we have to use async keyword in signature and 
        //return type for async method is Task 
        //use await call on the line we want to make async

        [HttpGet]
        [Authorize]
        public async  Task<IActionResult> GetAll()
        {
            //get data from database which is domain model
            var regionDomain = await RegionRepository.GetAllAsync();

            //map domain models to DTO 
            //var regionDto = new List<RegionDTO>();
            //foreach (var region in regionDomain)
            //{
            //  regionDto.Add(new RegionDTO()
            //{
            //        ID = region.ID,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageURL = region.RegionImageURL,
            //    });
            //} 

            //automapper will automatically map all the data from regionDomain to regionDTO and store it in regionsDto
            var regionsDto = mapper.Map<List<RegionDTO>>(regionDomain);

            return Ok(regionsDto);
        }

        //get region by id 
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById(Guid id) 
        {
            //find method only take primary key
            //var region = dbContext.Regions.Find(id);

            //get region domain model from database
            var region = await RegionRepository.GetByIdAsync(id); 
            if (region == null)
            {
                return NotFound();
            }
            
            //map region domain model to region dto
            var regionDto = new RegionDTO
            {
                ID = region.ID,
                Code = region.Code,
                Name = region.Name,
                RegionImageURL = region.RegionImageURL,

            };
            return Ok(regionDto);  

        }


        //post method to create new regions
        [HttpPost]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create(AddRegionRequestDto region)
        {

            if(ModelState.IsValid) {
                var regionDomainModel = new Region
                {
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageURL = region.RegionImageURL
                };


                //use domain model to create region
                regionDomainModel = await RegionRepository.CreateAsync(regionDomainModel);

                //map domain model to the dto 
                var regionDto = new RegionDTO
                {
                    ID = regionDomainModel.ID,
                    Code = regionDomainModel.Code,
                    Name = regionDomainModel.Name,
                    RegionImageURL = regionDomainModel.RegionImageURL,
                };

                return CreatedAtAction(nameof(GetById), new { ID = regionDto.ID }, regionDto);
            }
            else
            {
                return BadRequest();
            }
          
           
        }

        // method to update a region
        [HttpPut]
        [Route("{ID:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid ID, [FromBody] UpdateRegionRequestDto region)
        {
            var regionDomainModel = new Region
            {
                Code = region.Code, 
                Name = region.Name,
                RegionImageURL = region.RegionImageURL,
            };
            //first check if region exist or not in domain model
            regionDomainModel = await RegionRepository.UpdateAsync(ID, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map dto to the domain model 
            regionDomainModel.Code  = region.Code;
            regionDomainModel.Name = region.Name;
            regionDomainModel.RegionImageURL = region.RegionImageURL;

            //as regionDomainModel is tracked by dbcontext we just have to save the changes 
            //it will be reflected in database
            await dbContext.SaveChangesAsync();


            //convert domain model to dto 
            var regionDto = new RegionDTO
            {
                ID = regionDomainModel.ID,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = region.RegionImageURL,
            };

            return Ok(regionDto);
        }


        //delete a region
        [HttpDelete]
        [Route("{ID:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute]Guid ID)
        {
            var regions = await RegionRepository.DeleteAsync(ID);
            if (regions == null)
            {
                return NotFound();
            }

            return Ok(regions);
        }
    }
}

//DTO are data transfer objects
//use to transfer data between different layers
//containst the subset of property in the domain model
//DTO is used to show data to the client
//we never send domain model to the client we always send DTO 