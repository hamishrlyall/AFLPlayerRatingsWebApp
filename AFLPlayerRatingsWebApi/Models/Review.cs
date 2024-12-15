namespace AFLPlayerRatingsWebApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
        public required Reviewer Reviewer { get; set; }
        public required Player Player { get; set; }
    }
}
