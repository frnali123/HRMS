using HRMS.Database;
using HRMS.Mapping;
using HRMS.Model;
using HRMS.Repository;
using HRMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add Application DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));


//Add AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());


// Employee Add Repository
builder.Services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
//Add EMployee service
builder.Services.AddScoped<EmployeService>();
// EmployeeInboxes Repository

//Attendace Repository
builder.Services.AddScoped<IAttendanceRepository, SqlAttendaceRepository>();
builder.Services.AddScoped<AttendanceService>();


//Add leave Repository
builder.Services.AddScoped<ILeaveRepository, SqlLeaveRepository>();
builder.Services.AddScoped<LeaveService>();
//Add meeting Repository
builder.Services.AddScoped<IMeetingRepository, SqlMeetingRepository>();
builder.Services.AddScoped<MeetingService>();
//Add Message Repository
builder.Services.AddScoped<IMessageRepository, SqlMessageRepository>();
builder.Services.AddScoped<MessageService>();
//Add Feedback Repository
builder.Services.AddScoped<IFeedbackRepository, SqlFeedbackRepository>();
builder.Services.AddScoped<FeedbackService>();
//Add Performance Repository
builder.Services.AddScoped<IPerformanceEvaluationRepository, SqlPerformanceEvaluationRepository>();
builder.Services.AddScoped<PerformanceService>();
//Add token Repository
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<AuthService>();
// Repository ko register karein
builder.Services.AddScoped<IEmployeeDashboardRepository, SqlEmployeeDashboardRepository>();
// Service ko register karein (YE LINE AAPNE MISS KI HAI)
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<PasswordHasher<User>>();
builder.Services.AddHttpContextAccessor();

//Add jwt token
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
    });
//Add identity service
//builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().
//    AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("WebApi").
//    AddEntityFrameworkStores<ApplicationAuthDbContext>().AddDefaultTokenProviders();
//Add Identity services
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();
//builder.Services.Configure<IdentityOptions>(options =>
//{
//  options.Password.RequireNonAlphanumeric = false;
//  options.Password.RequireUppercase = false;
//  options.Password.RequireDigit = false;
//  options.Password.RequiredLength = 6;
//  options.Password.RequiredUniqueChars = 1;
//  options.Password.RequireLowercase = false;

//});

//Authorize all SwaggerGen
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo { Title = "HRMS API", Version = "v1" });


  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "bearer" //
  });


  options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
var allowedOrigins = builder.Configuration.GetValue<string>("allowedOrigins")!.Split(",");
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
