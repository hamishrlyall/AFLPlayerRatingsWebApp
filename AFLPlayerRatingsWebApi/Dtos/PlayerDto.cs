using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? FormattedBirthdate { get; set; }
        public string? Rating { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Headshot { get; set; }
        public required TeamDto Team { get; set; }

    }

    public class PlayerToCreateDto
    {
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? HeadShot { get; set; }
    }
}
