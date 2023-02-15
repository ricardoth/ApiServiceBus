using ApiServiceBus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string servicebusKey = builder.Configuration.GetConnectionString("ServiceBusKey");
ServiceQueueBus service = new ServiceQueueBus(servicebusKey);
builder.Services.AddTransient<ServiceQueueBus>(z => service);

string serviceTopiKey = builder.Configuration.GetConnectionString("ServiceTopicKey");
ServiceTopicBus serviceTopic = new ServiceTopicBus(serviceTopiKey);
builder.Services.AddTransient<ServiceTopicBus>(z => serviceTopic);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
