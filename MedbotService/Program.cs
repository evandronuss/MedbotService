using MedbotService.Configuration;
using Topshelf;

namespace MedbotService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configure =>
            {
                configure.Service<OwinService>(service =>
                {
                    service.ConstructUsing(s => new OwinService());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                //Setup Account that window service use to run.
                configure.RunAsLocalSystem();
                configure.StartAutomatically();


                configure.SetServiceName("MedbotService");
                configure.SetDisplayName("MedbotService");
                configure.SetDescription("Medbot Service");
            });
        }
    }
}
