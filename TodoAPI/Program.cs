using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at 
https://aka.ms/aspnetcore/swashbucklehttps://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<TodoAPI.Services.ITodoService, TodoAPI.Services.TodoService>();
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
		p.WithOrigins("http://localhost:5174")   
		 .AllowAnyHeader()
		 .AllowAnyMethod()));

builder.Services.AddSwaggerGen(o =>
{
	o.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "X-API-KEY",
		Type = SecuritySchemeType.ApiKey,
		Description = "API Key needed to access the endpoints. X-API-KEY: {API Key}"
	});

	o.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
				{
						new OpenApiSecurityScheme
						{
								Reference = new OpenApiReference
								{
										Type = ReferenceType.SecurityScheme,
										Id = "ApiKey"
								}
						},
						Array.Empty<string>()
				}
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.UseMiddleware<TodoAPI.Middleware.RequestLoggingMiddleware>();
app.UseMiddleware<TodoAPI.Middleware.CheckApiKeyMiddleware>();

app.MapControllers();

app.Run();
