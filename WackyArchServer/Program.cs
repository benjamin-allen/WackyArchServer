using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using WackyArchServer.Model;
using WackyArchServer.Services;
using Microsoft.AspNetCore.Identity;
using WackyArch.Utilities;
using WackyArch.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AlphaChallengeService>();
builder.Services.AddScoped<BetaChallengeService>();
builder.Services.AddDbContextFactory<WAContext>(options =>
{
    options.UseSqlServer($"Server=localhost;Database=wackyarchserver;Trusted_Connection=True;");
});
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<WAContext>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
