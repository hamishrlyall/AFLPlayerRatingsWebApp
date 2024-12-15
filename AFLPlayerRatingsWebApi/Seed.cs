//using AFLPlayerRatingsWebApi.Data;
//using AFLPlayerRatingsWebApi.Models;
//using System.Net;

//namespace AFLPlayerRatingsWebApi
//{
//    public class Seed
//    {
//        private readonly DataContext DbContext;

//        public Seed( DataContext _Context )
//        {
//            DbContext = _Context;
//        }

//        public void SeedDataContext( )
//        {
//            var reviewers = new List<Reviewer>( );

//            var lingy = new Reviewer( ) { FirstName = "Cameron", LastName = "Ling" };
//            reviewers.Add( lingy );

//            var cornes = new Reviewer( ) { FirstName = "Kane", LastName = "Cornes" };
//            reviewers.Add( cornes );

//            var whateley = new Reviewer( ) { FirstName = "Gerard", LastName = "Whateley" };
//            reviewers.Add( whateley );

//            var garry = new Reviewer( ) { FirstName = "Garry", LastName = "Lyon" };
//            reviewers.Add( garry );

//            var baileySmith = new Reviewer( ) { FirstName = "Bailey", LastName = "Smith" };
//            reviewers.Add( baileySmith );

//            var me = new Reviewer( ) { FirstName = "Hamish", LastName = "Lyall" };
//            reviewers.Add( me );

//            var kingy = new Reviewer( ) { FirstName = "David", LastName = "King" };
//            reviewers.Add( kingy );

//            if( !DbContext.Reviewers.Any() )
//            {
//                DbContext.Reviewers.AddRange( reviewers );
//            }


//            var teams = new List<Team>( );
//            var geelong = new Team( ) { Name = "Geelong Cats", HomeGround = "GMHBA Stadium" };
//            teams.Add( geelong );

//            var melbourne = new Team( ) { Name = "Melbourne Demons", HomeGround = "MCG" };
//            teams.Add( melbourne );

//            var collingwood = new Team( ) { Name = "Collingwood Magpies", HomeGround = "MCG" };
//            teams.Add( collingwood );

//            var bulldogs = new Team( ) { Name = "Western Bulldogs", HomeGround = "Marvel Stadium" };
//            teams.Add( bulldogs );


//            if( !DbContext.Teams.Any( ) )
//            {
//                DbContext.Teams.AddRange( teams );


//                //                var teams = new List<Team>( )
//                //                {
//                //                    new Team( )
//                //                    {
//                //                        Name = "Geelong Cats",
//                //                        HomeGround = "GMHBA Stadium",
//                //                        Players = new List<Player>
//                //                        {
//                //,
//                //                        }
//                //                    },
//                //                    new Team( )
//                //                    {
//                //                        Name = "Melbourne Demons",
//                //                        HomeGround = "MCG",
//                //                        Players = new List<Player>
//                //                        {
//                //                            new Player( )
//                //                            {
//                //                                Name = "Kysiah Pickett",
//                //                                BirthDate = new DateTime(2001, 6, 2 ),
//                //                                PlayerPositions = new List<PlayerPosition>( )
//                //                                {
//                //                                    new PlayerPosition { Position = new Position( ) {Name = "Forward"}}
//                //                                },
//                //                                Reviews = new List<Review>( )
//                //                                {
//                //                                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is the next Jeff Farmer.", Rating = 5,
//                //                                    Reviewer = garry },
//                //                                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is a thug.", Rating = 3,
//                //                                    Reviewer = baileySmith },
//                //                                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is a gun!", Rating = 4,
//                //                                    Reviewer = me },
//                //                                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is overrated.", Rating = 1,
//                //                                    Reviewer = cornes },
//                //                                }
//                //                            }
//                //                        }
//                //                    },
//                //                    new Team( )
//                //                    {
//                //                        Name = "Collingwood Magpies",
//                //                        HomeGround = "MCG",
//                //                        Players = new List<Player>
//                //                        {
//                //                            new Player( )
//                //                            {
//                //                                Name = "Darcy Moore",
//                //                                BirthDate = new DateTime(1996, 1, 25 ),
//                //                                PlayerPositions = new List<PlayerPosition>( )
//                //                                {
//                //                                    new PlayerPosition { Position = new Position( ) {Name = "Defender"}}
//                //                                },
//                //                                Reviews = new List<Review>( )
//                //                                {
//                //                                    new Review { Title="Darcy Moore", Text="Darcy Moore is the greatest captain ever.", Rating = 5,
//                //                                    Reviewer = new Reviewer() { FirstName = "Eddie", LastName = "McGuire" } },
//                //                                    new Review { Title="Darcy Moore", Text="Darcy Moore is overrated.", Rating = 1,
//                //                                    Reviewer = new Reviewer(){ FirstName = "Kane", LastName = "Cornes"} },
//                //                                }
//                //                            }
//                //                        }
//                //                    },
//                //                    new Team( )
//                //                    {
//                //                        Name = "Western Bulldogs",
//                //                        HomeGround = "Marvel Stadium",
//                //                        Players = new List<Player>
//                //                        {
//                //                            new Player( )
//                //                            {
//                //                                Name = "Tim English",
//                //                                BirthDate = new DateTime(2001, 6, 2 ),
//                //                                PlayerPositions = new List<PlayerPosition>( )
//                //                                {
//                //                                    new PlayerPosition { Position = new Position( ) {Name = "Ruckman"}}
//                //                                },
//                //                                Reviews = new List<Review>( )
//                //                                {
//                //                                    new Review { Title="Tim English", Text="Tim English is a good ruckman.", Rating = 4,
//                //                                    Reviewer = kingy },
//                //                                    new Review { Title="Tim English", Text="Tim English is overrated.", Rating = 1,
//                //                                    Reviewer = cornes },
//                //                                }
//                //                            }
//                //                        }
//                //                    },
//                //                };
//                //                DbContext.Teams.AddRange( teams );

//                //            }
//            }
//            var players = new List<Player>( );

//            var dangerfield = new Player( )
//            {
//                Name = "Patrick Dangerfield",
//                BirthDate = new DateTime( 1990, 4, 5 ),
//                Team = geelong,
//                PlayerPositions = new List<PlayerPosition>( )
//                {
//                    new PlayerPosition( ) { Position = new Position() { Name = "Midfielder" }  }
//                },
//                Reviews = new List<Review>( )
//                {
//                    new Review { Title="Patrick Dangerfield", Text="Patrick Dangerfield is one of the modern day greats.", Rating = 5,
//                    Reviewer = lingy },
//                    new Review { Title="Patrick Dangerfield", Text="Patrick Dangerfield is overrated.", Rating = 2,
//                    Reviewer =  cornes },
//                    new Review { Title="Patrick Dangerfield", Text="Patrick Dangerfield is almost as good as Gryan Miers.", Rating = 4,
//                    Reviewer = whateley }
//                }
//            };
//            players.Add( dangerfield );

//            var kozzy = new Player( )
//            {
//                Name = "Kysiah Pickett",
//                BirthDate = new DateTime( 2001, 6, 2 ),
//                Team = melbourne,
//                PlayerPositions = new List<PlayerPosition>( )
//                                {
//                                    new PlayerPosition { Position = new Position( ) {Name = "Forward"}}
//                                },
//                Reviews = new List<Review>( )
//                {
//                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is the next Jeff Farmer.", Rating = 5,
//                    Reviewer = garry },
//                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is a thug.", Rating = 3,
//                    Reviewer = baileySmith },
//                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is a gun!", Rating = 4,
//                    Reviewer = me },
//                    new Review { Title="Kysiah Pickett", Text="Kysiah Pickett is overrated.", Rating = 1,
//                    Reviewer = cornes },
//                }
//            };
//            players.Add( kozzy );

//            var moore = new Player( )
//            {
//                Name = "Darcy Moore",
//                BirthDate = new DateTime( 1996, 1, 25 ),
//                Team = collingwood,
//                PlayerPositions = new List<PlayerPosition>( )
//                {
//                    new PlayerPosition { Position = new Position( ) {Name = "Defender"}}
//                },
//                Reviews = new List<Review>( )
//                {
//                    new Review { Title="Darcy Moore", Text="Darcy Moore is the greatest captain ever.", Rating = 5,
//                    Reviewer = new Reviewer() { FirstName = "Eddie", LastName = "McGuire" } },
//                    new Review { Title="Darcy Moore", Text="Darcy Moore is overrated.", Rating = 1,
//                    Reviewer = new Reviewer(){ FirstName = "Kane", LastName = "Cornes"} },
//                }
//            };
//            players.Add( moore );

//            var english = new Player( )
//            {
//                Name = "Tim English",
//                BirthDate = new DateTime( 2001, 6, 2 ),
//                Team = bulldogs,
//                PlayerPositions = new List<PlayerPosition>( )
//                {
//                    new PlayerPosition { Position = new Position( ) {Name = "Ruckman"}}
//                },
//                Reviews = new List<Review>( )
//                {
//                    new Review { Title="Tim English", Text="Tim English is a good ruckman.", Rating = 4,
//                    Reviewer = kingy },
//                    new Review { Title="Tim English", Text="Tim English is overrated.", Rating = 1,
//                    Reviewer = cornes },
//                }
//            };
//            players.Add( english);

//            if( !DbContext.Players.Any() )
//            {
//                DbContext.Players.AddRange( players );
//            }

//            DbContext.SaveChanges( );
//        }
//    }
//}
