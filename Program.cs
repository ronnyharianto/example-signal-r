using SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

string _defaultCorsPolicyName = "WADIG-CIST";
string[] corsOrigin = ["http://cms-rfid.astra.co.id:3000"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddCors(
				options => options.AddPolicy(
					_defaultCorsPolicyName,
					cosrBuilder => cosrBuilder
						.AllowAnyMethod()
						.WithOrigins(corsOrigin)
						.AllowAnyHeader()
						.AllowCredentials()
				)
			);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

var webSocketOptions = new WebSocketOptions
{
	KeepAliveInterval = TimeSpan.FromMilliseconds(60000)
};
webSocketOptions.AllowedOrigins.Add("http://cms-rfid.astra.co.id:3000");
app.UseWebSockets(webSocketOptions);
app.UseCors(_defaultCorsPolicyName);

app.Run();
