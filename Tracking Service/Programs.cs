using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Segment;
using Tracking_Service.Cache;
using Tracking_Service.Consumers;
using Tracking_Service.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add<AuthorizationFilter>();
}).AddNewtonsoftJson();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddHttpContextAccessor();

builder.Services.InstallMassTransit(builder.Configuration);

builder.Services.AddSingleton(typeof(RedisServer));
builder.Services.AddScoped(typeof(ICacheService), typeof(CacheService));

builder.Services.AddScoped(typeof(ISpecHandler), typeof(SpecHandler));


builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
/*builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
});*/

//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//{
//    containerBuilder.RegisterModule(new DefaultCoreModule());
//    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
//});

//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();

app.MapHealthChecks("/hc");

//app.UseWatchDogExceptionLogger();
//app.UseWatchDog(options =>
//{
//  options.WatchPageUsername = "admin";
//  options.WatchPagePassword = "admin";
//});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseShowAllServicesMiddleware();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseCors("corsapp");


app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
Analytics.Client.Identify("Initialized", new Dictionary<string, object>() { { "hello", "world" } });


app.Run();

