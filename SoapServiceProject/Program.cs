using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapServiceProject.Services.Banking;
using SoapServiceProject.Services.Inventory;

var builder = WebApplication.CreateBuilder(args);

// Register CoreWCF services and metadata services.
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<BankingService>();
    serviceBuilder.AddService<InventoryService>();

    serviceBuilder.AddServiceEndpoint<BankingService, IBankingService>(
        new BasicHttpBinding(), "/BankingService.svc");

    serviceBuilder.AddServiceEndpoint<InventoryService, IInventoryService>(
        new BasicHttpBinding(), "/InventoryService.svc");

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

    serviceBuilder.ConfigureServiceHostBase<InventoryService>(host =>
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