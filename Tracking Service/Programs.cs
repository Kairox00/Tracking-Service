using Tracking_Service;
using Tracking_Service.Consumers;
using Tracking_Service.Receivers;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.InstallMassTransit(builder.Configuration);


var app = builder.Build();
//Receiver.Run();
app.Run();