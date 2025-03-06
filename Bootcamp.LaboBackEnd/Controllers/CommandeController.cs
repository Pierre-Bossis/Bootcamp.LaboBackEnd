using Bootcamp.LaboBackEnd.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bootcamp.LaboBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly ICommandeService _commandeService;

        public CommandeController(ICommandeService commandeService)
        {
            _commandeService = commandeService;
        }
    }
}
