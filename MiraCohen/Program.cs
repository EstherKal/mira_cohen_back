using Microsoft.Extensions.Options;
using Repository;
using Microsoft.AspNetCore.Builder;
using Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// ����� ������������ JWT �-Configuration
builder.Services.Configure<MiraCohenDatabaseSettings>(
    builder.Configuration.GetSection("MiraCohenDatabaseSettings"));

builder.Services.AddSingleton<MiraCohenDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MiraCohenDatabaseSettings>>().Value);

builder.Services.AddSingleton<IContext, MyDBContext>();
builder.Services.AddSingleton<JwtTokenService>();

// ����� Authentication �-JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//������ ��� ����� �� �� �������� ������� ����� �� ���� JWT Bearer Authentication ����� �� 
//��� ���� ������. ��� ����� ������� ������ ���� ������� ������ ��������� ���, . JWT 
//�� ��� ������� �� ���� ���� ����� ���� ������� �� ��������� ������
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Jwt:Key"]))
    };
});
//������ ������ �� �����

//var key = new byte[32];
//using (var rng = new RNGCryptoServiceProvider())
//{
//    rng.GetBytes(key);
//}
// ���� ����� ��Base64 ��� ����� ���� ������ �� ����� ������������
//var base64Key = Convert.ToBase64String(key);
//Console.WriteLine($"Generated Base64 Key: {base64Key}");

// ����� ��� ��������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

var app = builder.Build();

// ����� �-Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ����� �-HTTPS
app.UseHttpsRedirection();

// ����� �-Authentication �-Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "hello Mira Cohen");
app.Run();
