using ApiServiceBus.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServiceBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusController : ControllerBase
    {
        private ServiceQueueBus _service;
        private ServiceTopicBus _topicos;

        public ServiceBusController(ServiceQueueBus service, ServiceTopicBus topicos)
        {
            _service = service;
            _topicos = topicos;
        }

        [HttpGet("GetQueueMessages")]
        public async Task<IActionResult> GetQueueMessages()
        {
            List<string> mensajes = await _service.ReceiveMessagesAsync();
            if (mensajes.Count > 0)
                return Ok(mensajes);

            return BadRequest("No existen mensajes en la cola de Service Bus");
        }

        [HttpGet("GetTopicsMessages")]
        public async Task<IActionResult> GetTopicsMessages()
        {
            List<string> mensajes = await _topicos.ReceiveMessageAsync();
            if (mensajes.Count > 0)
                return Ok(mensajes);

            return BadRequest("No existen mensajes en el Tópico de Service Bus");
        }

        [HttpPost("PostQueueMessage")]
        public async Task<IActionResult> PostQueueMessage(string mensaje)
        {
            if (mensaje != "" && mensaje != null)
            {
                await _service.SendMessageAsync(mensaje);

                return Ok("El mensaje a la cola fue enviado exitosamente!");
            }

            return BadRequest("No se pudo enviar el mensaje a la cola");
        }

        [HttpPost("PostTopicMessage")]
        public async Task<IActionResult> PostTopicMessage(string mensaje)
        {
            if (mensaje != "" && mensaje != null)
            {
                await _topicos.SendMessageAsync(mensaje);
                return Ok("El mensaje al Service Bus Topics fue enviado exitosamente!");
            }

            return BadRequest("No se pudo enviar el mensaje a la cola");
        }
    }
}
