var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// shivam
 //Configure CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
// shivam
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// for angular app

app.Use(async (context, next) =>

{

    await next();

    if (context.Response.StatusCode == 404 && !System.IO.Path.HasExtension(context.Request.Path.Value))

    {

        context.Request.Path = "/index.html";

        await next();

    }

});

app.UseDefaultFiles();

app.UseStaticFiles();

// for angular app

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Enable CORS
app.UseCors();

app.UseRouting();

app.Run();
