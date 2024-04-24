using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required ]
        [MinLength(3 , ErrorMessage ="There should be 3 letters in code")]
        [MaxLength(3 , ErrorMessage ="There should be 3 letters in code")]
        public string Code { get; set; }

        [Required]
        [MaxLength(10 , ErrorMessage ="Name should be less than 10 Character")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
