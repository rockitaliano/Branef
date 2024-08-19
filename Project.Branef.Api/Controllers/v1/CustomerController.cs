using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Branef.Application.Commands.v1.Customer.Create;
using Project.Branef.Application.Commands.v1.Customer.Delete;
using Project.Branef.Application.Commands.v1.Customer.Update;
using Project.Branef.Application.Core;
using Project.Branef.Application.Queries.v1.Customer.GetAll;
using System.Net;

namespace Project.Branef.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CustomerController(IMediator mediator, ILoggerFactory loggerFactory)
        {
            _mediator = mediator;
            _logger = loggerFactory.CreateLogger<CustomerController>();
        }

        /// <summary>
        /// Método responsável em criar um cliente de acordo com os parâmetros.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">cliente criado com sucesso. </response>
        /// <response code="400">cliente já existente. </response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return !response.Success ? BadRequest(response) : Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in CreateCustomerCommandHandler of the Customer {command.CompanyName} | CorrelationId: {command.CorrelationId}");
                throw;
            }
        }

        /// <summary>
        /// Método responsável em atualizar um cliente de acordo com os parâmetros.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">cliente criado com sucesso. </response>
        /// <response code="400">cliente já existente. </response>
        /// <response code="404">cliente não existe. </response>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Success)
                    return Ok(response);
                else return response.Message.Contains("Not Found") ? NotFound(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in UpdateCustomerCommandHandler of the Customer {command.CompanyName} | CorrelationId: {command.CorrelationId}");
                throw;
            }
        }

        /// <summary>
        /// Método responsável em buscar a lista dos clientes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Informações dos clientes. </response>
        [HttpGet()]
        [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllCustomerQuery query)
        {
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when listing Customer | CorrelationId: {query.CorrelationId}");
                throw;
            }
        }

        /// <summary>
        /// Método responsável em deletar um cliente de acordo com os parâmetros.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">cliente deletado com sucesso. </response>
        /// <response code="400">cliente inválido. </response>
        /// <response code="404">cliente não existe. </response>
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseBase), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteCustomerCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Success)
                    return Ok(response);
                else return response.Message.Contains("Not Found") ? NotFound(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteCustomerCommandHandler of the Customer {command.Id} | CorrelationId: {command.CorrelationId}");
                throw;
            }
        }
    }
}
