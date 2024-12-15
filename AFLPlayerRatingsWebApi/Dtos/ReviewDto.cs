namespace AFLPlayerRatingsWebApi.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public required string Title { get; set; }
        public string? Text { get; set; }
    }
}
