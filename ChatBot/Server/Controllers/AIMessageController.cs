using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIMessageController
    {
        private readonly IAIMessageService _aiMessageService;

        public AIMessageController(IAIMessageService aiMessageService)
        {
            _aiMessageService = aiMessageService;
        }


    }
}
