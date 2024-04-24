using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        public IWalkRepository WalkRepository { get; }

        public WalksController(IMapper mapper , IWalkRepository walkRepository )
        {
            this.mapper = mapper;
            WalkRepository = walkRepository;
        }

        


        //create a walk
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddWalkRequestDto newWalk)
        {
            //map this dto to domain model
            var walkDomainModel = mapper.Map<Walk>(newWalk);

            await WalkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //get all the walks
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await WalkRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await WalkRepository.GetByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //update walk by id

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateById([FromRoute]Guid id , [FromBody]UpdatedWalkRequestDto updatedWalk)
        {
            var walkDomainModel = mapper.Map<Walk>(updatedWalk);
            walkDomainModel = await WalkRepository.UpdateByIdAsync(id , walkDomainModel);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel)); 
        }


        //delete a walk by id

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await WalkRepository.DeleteByIdAsync(id);
            if (walkDomainModel == null)
                return NotFound();
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
