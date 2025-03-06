using AccountingSystem.Client.Pages;
using AccountingSystem.Components;
using AccountingSystem.Data;
using AccountingSystem.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CircuitOptions>(options => { options.DetailedErrors = true; });

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<GroupService>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazoredModal();
builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AccountingSystem.Client._Imports).Assembly);

app.Run();
