///Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/productsinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#if DEBUG
builder.Services.AddTransient<IMailService,MailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif
builder.Services.AddDbContext<ProductDb>(dbContextOptions => dbContextOptions.
                    UseSqlite(builder.Configuration["ConnectionStrings:ProductDbCS"]));
builder.Services.AddScoped<IProductRepo, ProductRepo>();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
