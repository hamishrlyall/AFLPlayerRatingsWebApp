namespace AFLPlayerRatingsWebApi.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? HomeGround { get; set; }
    }

    public class TeamToCreateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
