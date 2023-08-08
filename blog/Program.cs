using blog.Data;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
// Desabilitando a auto validação do aspnet, precisamos desabilitar para que possamos usar a nossa padronização de requisitação(ResultViewModel)

builder.Services.AddDbContext<AppDBContext>();

var app = builder.Build();


app.MapControllers();

app.Run();
