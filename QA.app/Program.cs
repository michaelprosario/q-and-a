
using DocumentStore.Core.Interfaces;
using DocumentStore.Core.Services.DocumentStore.Core.Services;
using Havit.Blazor.Components.Web;            
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QA.Infra;
using QA.Core.Entities;
using QA.Core.Services;
using System;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHxServices(); 

// infra ...
var connectionString = builder.Configuration.GetConnectionString("DefaultPgConnection");
var migrationsAssembly = typeof(QAContext).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddDbContext<QAContext>(options =>
{
    options.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly));
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IQAQueryRepository), typeof(QAQueryRepository));

// core services ...
builder.Services.AddScoped(typeof(IDocumentsService<>), typeof(DocumentsService<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
