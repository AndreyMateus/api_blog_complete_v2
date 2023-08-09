using blog.Data;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
// Desabilitando a auto validação do aspnet, precisamos desabilitar para que possamos usar a nossa padronização de requisitação(ResultViewModel)

builder.Services.AddDbContext<AppDBContext>();

// Faz com que seja uma nova instância a cada requisição.
//builder.Services.AddTransient();
// Faz com que seja uma nova instância a cada chamada do tipo.
//builder.Services.AddScoped();
// Faz com que seja uma única instância em toda a aplicação.
//builder.Services.AddSingleton();


var app = builder.Build();


app.MapControllers();

app.Run();
