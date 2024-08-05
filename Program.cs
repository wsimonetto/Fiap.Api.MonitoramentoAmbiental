using Fiap.Api.MonitoramentoAmbiental.Data;
using Microsoft.EntityFrameworkCore;
using Fiap.Api.MonitoramentoAmbiental.Controllers;
using AutoMapper;
using Fiap.Api.MonitoramentoAmbiental.Data.Repository;
using Fiap.Api.MonitoramentoAmbiental.Model;
using Fiap.Api.MonitoramentoAmbiental.Services;
using Fiap.Api.MonitoramentoAmbiental.ViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.LeituraViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.PrevisaoChuvaViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.ControleIrrigacaoViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ADICIONAR SERVICES PARA O CONTAINER
builder.Services.AddControllersWithViews();

// CONEXÃO COM O DATA BASE
#region Configuracao do Data Base
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Registro IServicesCollection

//REPOSITORY
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<ILeituraRepository, LeituraRepository>();
builder.Services.AddScoped<IPrevisaoChuvaRepository, PrevisaoChuvaRepository>();
builder.Services.AddScoped<IControleIrrigacaoRepository, ControleIrrigacaoRepository>();

//SERVICES
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddScoped<ILeituraService, LeituraService>();
builder.Services.AddScoped<IPrevisaoChuvaService, PrevisaoChuvaService>();
builder.Services.AddScoped<IControleIrrigacaoService, ControleIrrigacaoService>();

#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c =>
{
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    //SENSOR
    c.CreateMap<SensorModel, SensorAllViewModel>();
    c.CreateMap<SensorAllViewModel, SensorModel>();

    //LEITURA
    c.CreateMap<LeituraModel, LeituraViewModel>();
    c.CreateMap<LeituraViewModel, LeituraModel>();
    c.CreateMap<LeituraCreateAndEditViewlModel, LeituraModel>()
                .ForMember(dest => dest.SensorId, opt => opt.MapFrom(src => src.SensorId))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
                .ForMember(dest => dest.DataHora, opt => opt.MapFrom(src => src.DataHora));

    //ALERTA
    c.CreateMap<AlertaModel, AlertaAllViewModel>();
    c.CreateMap<AlertaAllViewModel, AlertaModel>();

    //PREVISAOCHUVA
    c.CreateMap<PrevisaoChuvaModel, PrevisaoChuvaAllViewModel>();
    c.CreateMap<PrevisaoChuvaAllViewModel, PrevisaoChuvaModel>();

    //CONTROLEIRRIGACAO
    c.CreateMap<ControleIrrigacaoModel, ControleIrrigacaoAllViewModel>();
    c.CreateMap<ControleIrrigacaoAllViewModel, ControleIrrigacaoModel>();

}
);
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Autenticacao
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                "f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false,

        };

    });

#endregion

#region Versionamento
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Liberar acesso para o Test
public partial class Program { }