using DotNetEnv;
using ExpressVoituresApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExpressVoituresApp.Models.Repositories;
using ExpressVoituresApp.Models.Services;


var builder = WebApplication.CreateBuilder(args);

// Charge le fichier .env
Env.Load();

// --- Connexion à la base ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// --- Configuration Identity + roles ---
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

// --- Ajout Authentification + Autorisation ---
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Repositories
builder.Services.AddScoped<IAnnonceRepository, AnnonceRepository>();
builder.Services.AddScoped<IAchatRepository, AchatRepository>();
builder.Services.AddScoped<IVehiculeRepository, VehiculeRepository>();
builder.Services.AddScoped<IReparationRepository, ReparationRepository>();
builder.Services.AddScoped<IVenteRepository, VenteRepository>();

// Services
builder.Services.AddScoped<IAnnonceService, AnnonceService>();
builder.Services.AddScoped<IVehiculeService, VehiculeService>();
builder.Services.AddScoped<IAchatService, AchatService>();
builder.Services.AddScoped<IReparationService, ReparationService>();
builder.Services.AddScoped<IVenteService, VenteService>();

var app = builder.Build();

// --- Initialisation du r�le Admin au d�marrage ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    const string adminRole = "Admin";

    // --- R�cup�re variables d'environnement ---
    var adminEmail = Environment.GetEnvironmentVariable("AdminEmail") ?? "admin@expressvoitures.com";
    var adminPassword = Environment.GetEnvironmentVariable("AdminPassword")
        ?? throw new InvalidOperationException("AdminPassword not set.");

    // --- Cr�er r�le si inexistant ---
    if (!await roleManager.RoleExistsAsync(adminRole))
        await roleManager.CreateAsync(new IdentityRole(adminRole));

    // --- Cr�er utilisateur admin si inexistant ---
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, adminRole);
    }
}

// --- Middleware pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
