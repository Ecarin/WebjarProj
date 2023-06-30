using WebjarProj.Services.Implementations;
using WebjarProj.Services.Interfaces;
using WebjarProj.Data;
using WebjarProj.Mapping;
using Microsoft.OpenApi.Models;

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
    app.UseSwaggerUI();
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