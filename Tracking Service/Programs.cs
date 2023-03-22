using Segment;
using Tracking_Service;
using Tracking_Service.Consumers;
using Tracking_Service.Receivers;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.InstallMassTransit(builder.Configuration);


var app = builder.Build();
Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
//Receiver.Run();
app.Run();