 namespace NZWalks.API.Models.DTO
{
    //this class will have the properties we want to expose or show to the client from Region domain model
    public class RegionDTO
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
