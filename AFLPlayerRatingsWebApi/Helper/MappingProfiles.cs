using AFLPlayerRatingsWebApi.Dtos;
using AFLPlayerRatingsWebApi.Models;
using AutoMapper;

namespace AFLPlayerRatingsWebApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Player, PlayerDto>( );
            CreateMap<Position, PositionDto>( );
            CreateMap<Team, TeamDto>( );
            CreateMap<Reviewer, ReviewerDto>( );
            CreateMap<Review, ReviewDto>( );
            CreateMap<PlayerToCreateDto, Player>( );
            CreateMap<PositionDto, Position>( );
            CreateMap<TeamDto, Team>( );
            CreateMap<ReviewerDto, Reviewer>( );
            CreateMap<ReviewDto, Review>( );
        }
    }
}
