using Microsoft.EntityFrameworkCore;
using PoupaDev.Api.Persistence;

namespace PoupaDev.Api.Jobs
{
    public class RendimentoAutomaticoJob : IHostedService
    {
        private Timer? _timer;
        public IServiceProvider ServiceProvider;

        public RendimentoAutomaticoJob(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RenderSaldo, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        public void RenderSaldo(object? state)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var context = scope
                    .ServiceProvider
                    .GetService<PoupaDevContext>();

                if (context == null) return;

                var objetivos = context
                    .Objetivos
                    .Include(o => o.Operacoes);

                foreach (var objetivo in objetivos)
                {
                    objetivo.Render();
                }

                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}