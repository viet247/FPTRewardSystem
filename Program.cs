using FluentValidation;
using FluentValidation.AspNetCore;
using FPTRewardSystem.API.Data;
using FPTRewardSystem.API.Exceptions;
using FPTRewardSystem.API.Services;
using FPTRewardSystem.API.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Mức log tối thiểu
    .WriteTo.Console() // In ra Console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Tạo file log theo ngày
    .CreateLogger();
// Khai báo với WebApplication dùng Serilog thay cho Logger mặc định
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
// Đăng ký tầng Service vào DI Container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Cấu hình xác thực bằng JWT Bearer Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Thêm dịch vụ vào DI Container
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
var app = builder.Build();
// Kích hoạt Middleware
app.UseExceptionHandler(); // Xử lý lỗi
app.UseAuthentication(); // Ai đang gọi? (Xác thực)
app.UseAuthorization(); // Có quyền làm gì? (Phân quyền)
app.UseSwagger();
app.UseSwaggerUI();
app.UseSerilogRequestLogging(); // Tự động log lại mọi HTTP Request (URL, Method, Status Code, Time)

// app.UseHttpsRedirection();


app.MapControllers();

app.Run();