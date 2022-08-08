using realtimeChat;
using realtimeChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://sergey-realtime-chat.herokuapp.com/")
                .AllowAnyHeader()
                .WithMethods()
                .AllowCredentials();
        });
});

builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseWebSockets();

app.UseEndpoints(endnpoints =>
{
    endnpoints.MapHub<ChatHub>("/chat");
}
);

app.Run();

