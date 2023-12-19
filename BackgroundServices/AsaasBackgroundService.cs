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

                    var myService = serviceProvider.GetRequiredService<IPlanoFornecedorService>();
                    myService.AtualizaStatusPlanoFornecedorConformeAsaas();
                }

                await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
            }
        }
    }
}
