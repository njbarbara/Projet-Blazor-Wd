using Blazored.LocalStorage;
using Blazored.Modal;
using BlazorSAE.Components;
using BlazorSAE.Services.Implementations;
using BlazorSAE.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System.Globalization;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));


builder.Services.AddMudServices();

//ajout du service de blazored modal
builder.Services.AddBlazoredModal();

//definition de la résolution de nos services
builder.Services.AddScoped<IDataService, DataLocalService>();

builder.Services.AddScoped<IBanService, BanDataLocalService>();

builder.Services.AddScoped<IEditUserService, UserDataService>();


// Add the controller of the app
builder.Services.AddControllers();

// Add the localization to the app and specify the resources path
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

// Configure the localtization
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Set the default culture of the web site
    options.DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"));

    // Declare the supported culture
    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fr-FR") };
    options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fr-FR") };
});


// client http singleton pour toutes l'application
builder.Services.AddHttpClient();

// Local storage 
builder.Services.AddBlazoredLocalStorage();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Get the current localization options
var options = ((IApplicationBuilder)app).ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

if (options?.Value != null)
{
    // use the default localization
    app.UseRequestLocalization(options.Value);
}

// Add the controller to the endpoint
app.MapControllers();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//404
app.MapFallback(context =>
{
    context.Response.Redirect("/404");
    return Task.CompletedTask;
});

app.Run();
