using SimpleInjector;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                       .AllowAnyHeader();
            });
    });

var container = new SimpleInjector.Container();
builder.Services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore()
    .AddControllerActivation();
    options.AddLogging();
});

container.Register<IContactRepository, ContactRepository>();
container.Register<IAddressService, AddressService>();

var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Contact, ContactDto>();
    cfg.CreateMap<ContactDto, Contact>();
    cfg.CreateMap<Contact, ContactDetailsDto>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.Services.UseSimpleInjector(container);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

container.Verify();

app.Run();
