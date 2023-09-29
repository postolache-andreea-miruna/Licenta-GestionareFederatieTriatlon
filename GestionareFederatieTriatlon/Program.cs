using GestionareFederatieTriatlon.Configuratii;
using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Hubs;
using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<ChatManager>();
builder.Services.AddSignalR();
//
builder.Services.AddCors();

// Add services to the container.
/*var SpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOrigins,
                       builder =>
                       {
                           builder.WithOrigins("localhost:4200", "http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                       });
});*/
builder.Services.Configure<EmailSetari>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name="Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});


// Conexiune cu baze de date
string conexiuneString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GestionareFederatieTriatlonContext>(
     optiuni =>
     {
         optiuni.UseSqlServer(conexiuneString);
     });

//autorizare pe roluri
builder.Services.AddIdentity<Utilizator, Rol>()
    .AddEntityFrameworkStores<GestionareFederatieTriatlonContext>();

//se foloseste autentificare in aplicatie
//token se transmite in request folosind cuvantul bearer in fata
builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("AuthScheme", options =>
                {
                    options.SaveToken = true;
                    var secret = builder.Configuration.GetSection("Jwt").GetSection("SecretKey").Get<String>();//luam cheia secreta
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminUtilizator", policy => policy.RequireRole("AdminUtilizator")
     .RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
    opt.AddPolicy("SportivUtilizator", policy => policy.RequireRole("SportivUtilizator")
     .RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
    opt.AddPolicy("AntrenorUtilizator", policy => policy.RequireRole("AntrenorUtilizator")
     .RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
});

//install Microsoft.AspNetCore.Mvc.NewtonsoftJson
//ne trebuie pentru cand facem join, ex tabela x are un y si tabela y are mai multi x => ciclare si importul face o sg referinta si se opreste
builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddTransient<IEmailServ, EmailServ>();

builder.Services.AddTransient<IAutentificareManager, AutentificareManager>();
builder.Services.AddTransient<ITokenManager, TokenManager>();

builder.Services.AddTransient<IClubManager, ClubManager>();
builder.Services.AddTransient<IClubRepo,ClubRepo>();

builder.Services.AddTransient<IProbaManager, ProbaManager>();
builder.Services.AddTransient<IProbaRepo, ProbaRepo>();

builder.Services.AddTransient<ITipManager, TipManager>();
builder.Services.AddTransient<ITipRepo, TipRepo>();

builder.Services.AddTransient<ILocatieManager, LocatieManager>();
builder.Services.AddTransient<ILocatieRepo, LocatieRepo>();

builder.Services.AddTransient<ICompetitieManager, CompetitieManager>();
builder.Services.AddTransient<ICompetitieRepo, CompetitieRepo>();

builder.Services.AddTransient<IIstoricManager, IstoricManager>();
builder.Services.AddTransient<IIstoricRepo, IstoricRepo>();

builder.Services.AddTransient<IAntrenorManager, AntrenorManager>();
builder.Services.AddTransient<IAntrenorRepo, AntrenorRepo>();

builder.Services.AddTransient<ISportivManager, SportivManager>();
builder.Services.AddTransient<ISportivRepo, SportivRepo>();

builder.Services.AddTransient<IFormularRepo, FormularRepo>();
builder.Services.AddTransient<IFormularManager, FormularManager>();

builder.Services.AddTransient<IRecenzieRepo, RecenzieRepo>();
builder.Services.AddTransient<IRecenzieManager, RecenzieManager>();

builder.Services.AddTransient<IVideoclipRepo, VideoclipRepo>();
builder.Services.AddTransient<IVideoclipManager, VideoclipManager>();

builder.Services.AddTransient<IComentariuRepo, ComentariuRepo>();
builder.Services.AddTransient<IComentariuManager, ComentariuManager>();

builder.Services.AddTransient<IPostareRepo, PostareRepo>();
builder.Services.AddTransient<IPostareManager, PostareManager>();

builder.Services.AddTransient<INotificareRepo, NotificareRepo>();
builder.Services.AddTransient<INotificareManager, NotificareManager>();

builder.Services.AddTransient<IChatRepo, ChatRepo>();
builder.Services.AddTransient<IChatManager, ChatManager>();

builder.Services.AddTransient<IUtilizatorRepo, UtilizatorRepo>();
builder.Services.AddTransient<IUtilizatorManager, UtilizatorManager>();

builder.Services.AddTransient<IIstoricClubRepo,IstoricClubRepo>();

builder.Services.AddTransient<IReactiePostareManager, ReactiePostareManager>();
builder.Services.AddTransient<IReactiePostareRepo, ReactiePostareRepo>();
var app = builder.Build();
//
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/hubs/chat");

app.Run();
