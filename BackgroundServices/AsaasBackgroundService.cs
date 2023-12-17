using HojeEuCaso.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HojeEuCaso.BackgroundServices
{
    public class AsaasBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AsaasBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;

                    // Injete os serviços necessários e execute o método desejado
                    var myService = serviceProvider.GetRequiredService<IPlanoFornecedorService>();
                    myService.Teste();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
