using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Models;
using System.Net;

namespace AFLPlayerRatingsWebApi
{
    public class Seed
    {
        private readonly DataContext DbContext;

        public Seed( DataContext _Context )
        {
            DbContext = _Context;
        }

        public void SeedDataContext( )
        {
            var teams = new List<Team>( );
            if( !DbContext.Teams.Any( ) )
            {
                var geelong = new Team( ) { Name = "Geelong Cats", HomeGround = "GMHBA Stadium", Players = new List<Player>( ) };
                teams.Add( geelong );
                var melbourne = new Team( ) { Name = "Melbourne Demons", HomeGround = "MCG", Players = new List<Player>( ) };
                teams.Add( melbourne );
                var collingwood = new Team( ) { Name = "Collingwood Magpies", HomeGround = "MCG", Players = new List<Player>( ) };
                teams.Add( collingwood );
                var bulldogs = new Team( ) { Name = "Western Bulldogs", HomeGround = "Marvel Stadium", Players = new List<Player>( ) };
                teams.Add( bulldogs );

                var lingy = new Reviewer( ) { FirstName = "Cameron", LastName = "Ling", Reviews = new List<Review>() };
                var cornes = new Reviewer( ) { FirstName = "Kane", LastName = "Cornes", Reviews = new List<Review>( ) };
                var whateley = new Reviewer( ) { FirstName = "Gerard", LastName = "Whateley", Reviews = new List<Review>( ) };
                var garry = new Reviewer( ) { FirstName = "Garry", LastName = "Lyon", Reviews = new List<Review>( ) };
                var baileySmith = new Reviewer( ) { FirstName = "Bailey", LastName = "Smith", Reviews = new List<Review>( ) };
                var me = new Reviewer( ) { FirstName = "Hamish", LastName = "Lyall", Reviews = new List<Review>( ) };
                var kingy = new Reviewer( ) { FirstName = "David", LastName = "King", Reviews = new List<Review>( ) };
                var eddie = new Reviewer( ) { FirstName = "Eddie", LastName = "McGuire", Reviews = new List<Review>( ) };

                var midfielder = new Position( ) { Name = "Midfielder" };
                var forward = new Position( ) { Name = "Forward" };
                var defender = new Position( ) { Name = "Defender" };
                var ruckman = new Position( ) { Name = "Ruckman" };

                var dangerfield = new Player( )
                {
                    Name = "Patrick Dangerfield",
                    BirthDate = new DateTime( 1990, 4, 5 ),
                    Team = geelong,
                    Reviews = new List<Review>( ),
                    PlayerPositions = new List<PlayerPosition>( )
                };
                geelong.Players.Add( dangerfield );

                var kozzy = new Player( )
                {
                    Name = "Kysiah Pickett",
                    BirthDate = new DateTime( 2001, 6, 2 ),
                    Team = melbourne,
                    Reviews = new List<Review>( ),
                    PlayerPositions = new List<PlayerPosition>( )
                };
                melbourne.Players.Add( kozzy );

                var moore = new Player( )
                {
                    Name = "Darcy Moore",
                    BirthDate = new DateTime( 1996, 1, 25 ),
                    Team = collingwood,
                    Reviews = new List<Review>( ),
                    PlayerPositions = new List<PlayerPosition>( )
                };
                collingwood.Players.Add( moore );

                var english = new Player( )
                {
                    Name = "Tim English",
                    BirthDate = new DateTime( 2001, 6, 2 ),
                    Team = bulldogs,
                    Reviews = new List<Review>( ),
                    PlayerPositions = new List<PlayerPosition>( )
                };
                bulldogs.Players.Add( english );


                var reviews = new List<Review>( );
                var dangerfieldReview1 = new Review( )
                {
                    Title = "Patrick Dangerfield",
                    Text = "Patrick Dangerfield is one of the modern day greats.",
                    Rating = 5,
                    Reviewer = lingy,
                    Player = dangerfield
                };
                dangerfield.Reviews.Add( dangerfieldReview1 );

                var dangerfieldReview2 = new Review
                {
                    Title = "Patrick Dangerfield",
                    Text = "Patrick Dangerfield is overrated.",
                    Rating = 2,
                    Reviewer = cornes,
                    Player = dangerfield
                };
                dangerfield.Reviews.Add( dangerfieldReview2 );

                var dangerfieldReview3 = new Review( )
                {
                    Title = "Patrick Dangerfield",
                    Text = "Patrick Dangerfield is almost as good as Gryan Miers.",
                    Rating = 4,
                    Reviewer = whateley,
                    Player = dangerfield
                };
                dangerfield.Reviews.Add( dangerfieldReview3 );

                var kozzyReview1 = new Review
                {
                    Title = "Kysiah Pickett",
                    Text = "Kysiah Pickett is the next Jeff Farmer.",
                    Rating = 5,
                    Reviewer = garry,
                    Player = kozzy
                };
                kozzy.Reviews.Add( kozzyReview1 );

                var kozzyReview2 = new Review
                {
                    Title = "Kysiah Pickett",
                    Text = "Kysiah Pickett is a thug.",
                    Rating = 3,
                    Reviewer = baileySmith,
                    Player = kozzy
                };
                kozzy.Reviews.Add( kozzyReview2 );

                var kozzyReview3 = new Review
                {
                    Title = "Kysiah Pickett",
                    Text = "Kysiah Pickett is a gun!",
                    Rating = 4,
                    Reviewer = me,
                    Player = kozzy
                };
                kozzy.Reviews.Add( kozzyReview3 );

                var kozzyReview4 = new Review( ) {
                    Title = "Kysiah Pickett",
                    Text = "Kysiah Pickett is overrated.",
                    Rating = 1,
                    Reviewer = cornes,
                    Player = kozzy
                };
                kozzy.Reviews.Add( kozzyReview4 );

                var mooreReview1 = new Review( )
                {
                    Title = "Darcy Moore",
                    Text = "Darcy Moore is the greatest captain ever.",
                    Rating = 5,
                    Reviewer = eddie,
                    Player = moore
                };
                moore.Reviews.Add( mooreReview1 );

                var mooreReview2 = new Review( )
                { 
                    Title = "Darcy Moore", 
                    Text = "Darcy Moore is overrated.", 
                    Rating = 1, 
                    Reviewer = cornes,
                    Player = moore
                };
                moore.Reviews.Add( mooreReview2 );

                var englishReview1 = new Review( )
                {
                    Title = "Tim English",
                    Text = "Tim English is a good ruckman.",
                    Rating = 4,
                    Reviewer = kingy,
                    Player = english
                };
                english.Reviews.Add( englishReview1 );

                var englishReview2 = new Review( )
                {
                    Title = "Tim English",
                    Text = "Tim English is overrated.",
                    Rating = 1,
                    Reviewer = cornes,
                    Player= english
                };
                english.Reviews.Add( englishReview2 );

                var dangerMid = new PlayerPosition( )
                {
                    Player = dangerfield,
                    Position = midfielder
                };
                dangerfield.PlayerPositions.Add( dangerMid );

                var kozzyForward = new PlayerPosition( )
                {
                    Player = kozzy,
                    Position = forward
                };
                kozzy.PlayerPositions.Add( kozzyForward );

                var mooreDefender = new PlayerPosition( )
                {
                    Player = moore,
                    Position = defender
                };
                moore.PlayerPositions.Add( mooreDefender );

                var englishRuck = new PlayerPosition( )
                {
                    Player = english,
                    Position = ruckman
                };
                english.PlayerPositions.Add( englishRuck );

                DbContext.Teams.AddRange( teams );
                DbContext.SaveChanges( );
            }
        }
    }
}
