using AFLPlayerRatingsWebApi;
using AFLPlayerRatingsWebApi.Data;
using AFLPlayerRatingsWebApi.Interfaces;
using AFLPlayerRatingsWebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers( );
builder.Services.AddTransient<Seed>( );
builder.Services.AddAutoMapper( AppDomain.CurrentDomain.GetAssemblies() );
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>( );
builder.Services.AddScoped<IPositionRepository, PositionRepository>( );
builder.Services.AddScoped<ITeamRepository, TeamRepository>( );
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>( );
builder.Services.AddScoped<IReviewRepository, ReviewRepository>( );
builder.Services.AddCors( options => options.AddPolicy( "AllowAll", p => p.AllowAnyOrigin( ).AllowAnyMethod( ).AllowAnyHeader( ) ) );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );
builder.Services.AddDbContext<DataContext>( options =>
{
    options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection" ) );
} );

var app = builder.Build( );

SeedData( app );

void SeedData( IHost app )
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>( );

    using( var scope = scopedFactory.CreateScope( ) )
    {
        var service = scope.ServiceProvider.GetService<Seed>( );
        service.SeedDataContext( );
    }
}

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment( ) )
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseStaticFiles( new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider( @"E:\to-delete" ),
    RequestPath = "/StaticFiles"
} );

app.UseHttpsRedirection( );

app.UseAuthorization( );
app.UseCors( "AllowAll" );

app.MapControllers( );

app.Run( );
