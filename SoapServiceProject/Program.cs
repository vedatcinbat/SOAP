using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapServiceProject.Services.Banking;

var builder = WebApplication.CreateBuilder(args);

// Register CoreWCF services and metadata services.
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    // Register your service.
    serviceBuilder.AddService<BankingService>();

    // Add the SOAP endpoint using BasicHttpBinding.
    serviceBuilder.AddServiceEndpoint<BankingService, IBankingService>(
        new BasicHttpBinding(), "/BankingService.svc");

    // Configure the service host to add a metadata behavior.
    // This enables WSDL to be generated via HTTP GET.
    serviceBuilder.ConfigureServiceHostBase<BankingService>(host =>
    {
        var smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
        if (smb == null)
        {
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
        }
        else
        {
            smb.HttpGetEnabled = true;
        }
    });
});

app.Run();