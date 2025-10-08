using EnglishNow.Repositories;
using EnglishNow.Services;
using EnglishNow.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Configuration.AddEnvironmentVariables();

// Configurar DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IBoletimService, BoletimService>();

// Configurar repositórios baseado no provider configurado
var databaseSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>()!;
var connectionString = databaseSettings.Provider.ToLower() switch
{
    "mysql" => builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("Connection string 'MySqlConnection' não encontrada nos secrets."),
    "sqlserver" => builder.Configuration.GetConnectionString("SqlServerConnection") ?? throw new InvalidOperationException("Connection string 'SqlServerConnection' não encontrada nos secrets."),
    _ => throw new InvalidOperationException($"Provider '{databaseSettings.Provider}' não é suportado. Use 'mysql' ou 'sqlserver'.")
};

// Registrar repositórios baseado no provider
if (databaseSettings.Provider.ToLower() == "mysql")
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString));
    builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(c => new ProfessorRepository(connectionString));
    builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(c => new AlunoRepository(connectionString));
    builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(c => new TurmaRepository(connectionString));
    builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(c => new AlunoTurmaBoletimRepository(connectionString));
}
else if (databaseSettings.Provider.ToLower() == "sqlserver")
{
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorySqlServer>(c => new UsuarioRepositorySqlServer(connectionString));
    builder.Services.AddScoped<IProfessorRepository, ProfessorRepositorySqlServer>(c => new ProfessorRepositorySqlServer(connectionString));
    builder.Services.AddScoped<IAlunoRepository, AlunoRepositorySqlServer>(c => new AlunoRepositorySqlServer(connectionString));
    builder.Services.AddScoped<ITurmaRepository, TurmaRepositorySqlServer>(c => new TurmaRepositorySqlServer(connectionString));
    builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepositorySqlServer>(c => new AlunoTurmaBoletimRepositorySqlServer(connectionString));
}

var app = builder.Build();

// Usando tela de erro customizada mesmo em ambiente de desenvolvimento
app.UseExceptionHandler("/Erro/Index");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
