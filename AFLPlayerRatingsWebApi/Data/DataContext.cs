using AFLPlayerRatingsWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AFLPlayerRatingsWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions<DataContext> _Options ) : base( _Options )
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerPosition> PlayerPositions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating( ModelBuilder _ModelBuilder )
        {
            _ModelBuilder.Entity<PlayerPosition>( )
                .HasKey( pp => new { pp.PlayerId, pp.PositionId } );
            _ModelBuilder.Entity<PlayerPosition>( )
                .HasOne( p => p.Player )
                .WithMany( pp => pp.PlayerPositions )
                .HasForeignKey( pos => pos.PlayerId );
            _ModelBuilder.Entity<PlayerPosition>( )
                .HasOne( p => p.Position )
                .WithMany( pp => pp.PlayerPositions )
                .HasForeignKey( pos => pos.PositionId );
        }
    }
}
