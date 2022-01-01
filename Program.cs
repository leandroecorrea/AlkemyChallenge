using Microsoft.OpenApi.Models;
using DisneyAPI.Models;
using DisneyAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DisneyContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{
   options.TokenValidationParameters = new TokenValidationParameters(){
      ValidateActor = true,
      ValidateAudience = true,
      ValidateLifetime = true, 
      ValidateIssuerSigningKey = true,
      ValidIssuer = builder.Configuration["Jwt:Issuer"],
      ValidAudience = builder.Configuration["Jwt:Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
   };
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<ICharacterServices, CharacterServices>();
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IMailingServices, MailingServices>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "Disney API",
         Description = "DisneyWorld API",
         Version = "v1" });
      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
         Scheme = "Bearer", 
         BearerFormat =  "JWT",
         In = ParameterLocation.Header,
         Name = "Authorization",
         Description = "Bearer Authentication",
         Type = SecuritySchemeType.Http,
      });      
      c.AddSecurityRequirement(new OpenApiSecurityRequirement{
         {
         new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
               Id = "Bearer",
               Type = ReferenceType.SecurityScheme
            }            
         },
         new List<string>()
         }
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "DisneyWorld API V1");
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
