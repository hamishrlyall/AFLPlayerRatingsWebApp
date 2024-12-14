using AFLPlayerRatingsWebApi.Models;

namespace AFLPlayerRatingsWebApi.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        //public TeamDto Team { get; set; }
        //public ICollection<ReviewDto> Reviews { get; set; }
    }
}
