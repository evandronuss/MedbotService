using System.Web.Http;

namespace MedbotService.Controllers
{
    public class Teste
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class TesteController : ApiController
    {
        [HttpGet]
        public Teste Get()
        {
            return new Teste {
                Name = "teste",
                Age = 20
            };
        }
    }
}
