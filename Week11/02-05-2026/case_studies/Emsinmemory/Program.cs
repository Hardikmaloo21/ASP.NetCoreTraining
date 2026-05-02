var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI( c =>
{
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();