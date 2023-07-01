using WebjarProj.Services.Implementations;
using WebjarProj.Services.Interfaces;
using WebjarProj.Data;
using WebjarProj.Mapping;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Webjar API",
            Version = "v1",
            Description = "An API to perform Ecommerce operations",
            Contact = new OpenApiContact
            {
                Name = "Amin Ansari",
                Email = "3carin@gmail.com",
            },
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

// Register DbContext
builder.Services.AddDbContext<WebjarDbContext>();

// Register Services Interface
builder.Services.AddScoped<IAddonService, AddonService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "swagger";
});
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();