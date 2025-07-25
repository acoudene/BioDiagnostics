// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coud�ne       | Creation

using BioDiagnostics.WebApp.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization();

builder.Services.AddViewModels();
builder.Services.AddBffClients(new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddMudServices();

var host = builder.Build();

await host.SetDefaultCulture();

await host.RunAsync();
