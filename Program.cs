//--------------------------------------------------------------------------------------------------------------------------
//CRUD with Authorization by Policy with search & filter & pagination metadata. Documentation completed for sample methods.|
//--------------------------------------------------------------------------------------------------------------------------

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
builder.Services.AddSwaggerGen(setup =>
{
    var xmlCommentsFile = $"comments.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory,xmlCommentsFile); 
    setup.IncludeXmlComments(xmlCommentsFullPath);

    setup.AddSecurityDefinition("ProductAPI-BearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API."
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ProductAPI-BearerAuth" }
            }, new List<string>() }
    });
});
#if DEBUG
builder.Services.AddTransient<IMailService,MailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif
builder.Services.AddDbContext<ProductDb>(dbContextOptions => dbContextOptions.
                    UseSqlite(builder.Configuration["ConnectionStrings:ProductDbCS"]));
builder.Services.AddScoped<IProductRepo, ProductRepo>();

builder.Host.UseSerilog();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TestPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("mail", "developer@example.com");
    });
});
builder.Services.AddApiVersioning(versionConfiguration =>
{
    versionConfiguration.AssumeDefaultVersionWhenUnspecified = true;
    versionConfiguration.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    versionConfiguration.ReportApiVersions = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
