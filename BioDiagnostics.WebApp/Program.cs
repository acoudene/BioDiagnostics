// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.Api.Filters;
using Core.Api.Handlers;
using BioDiagnostics.Api.BackendForFrontend;
using BioDiagnostics.WebApp.Client.Extensions;
using BioDiagnostics.WebApp.Components;
using BioDiagnostics.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Register the global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

/// <see cref="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/handle-errors?view=aspnetcore-7.0#problem-details"/>
/// <seealso cref="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-7.0&preserve-view=true#pds7"/>
builder.Services.AddProblemDetails();

// Globalization and localization
builder.Services.AddLocalization();

/// Add module to controller scanning, for clarty I have been redundant on controllers even if they share the same assembly 
builder.Services.AddControllersWithViews(options =>
                {
                  options.Filters.Add<HttpCodeConverterExceptionFilter>();
                  options.Filters.Add<LogActionArgumentsFilter>();
                })
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(new AssemblyPart(typeof(RequestToBeReviewedBffController).Assembly)))
                ;

// Add services to the container.
builder.Services.AddRazorComponents(options =>
    options.DetailedErrors = builder.Environment.IsDevelopment())
    .AddInteractiveWebAssemblyComponents();

const string bffApiBaseAddressesKey = "ASPNETCORE_URLS";
string bffApiBaseAddress = ((builder.Configuration[bffApiBaseAddressesKey] ?? string.Empty).Split(";").FirstOrDefault()) ?? string.Empty;

if (string.IsNullOrWhiteSpace(bffApiBaseAddress))
  throw new InvalidOperationException($"Missing value for configuration key: {bffApiBaseAddress}");

builder.Services.AddViewModels();
builder.Services.AddBffClients(new Uri(bffApiBaseAddress));

const string requestToBeReviewedApiBaseAddressKey = "RequestToBeReviewed_API_BASEADDRESS";
string requestToBeReviewedApiBaseAddress = builder.Configuration[requestToBeReviewedApiBaseAddressKey] ?? string.Empty;
if (string.IsNullOrWhiteSpace(requestToBeReviewedApiBaseAddress))
  throw new InvalidOperationException($"Missing value for configuration key: {requestToBeReviewedApiBaseAddressKey}");

builder.Services.AddRequestToBeReviewedApiClient(new Uri(requestToBeReviewedApiBaseAddress));

builder.Services.AddMudServices();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BioDiagnostics.WebApp.Client._Imports).Assembly);

app.MapControllers();

app.Run();
