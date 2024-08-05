using Asp.Versioning;
using AutoMapper;
using Fiap.Api.MonitoramentoAmbiental.Model;
using Fiap.Api.MonitoramentoAmbiental.Services;
using Fiap.Api.MonitoramentoAmbiental.ViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.Api.MonitoramentoAmbiental.Controllers
{

    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public class SensorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISensorService _sensorService;
        
        public SensorController(
            IMapper mapper,
            ISensorService sensorService)
        {
            _mapper = mapper;
            _sensorService = sensorService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<SensorAllViewModel>> Get()
        {
            var sensores = _sensorService.ListarSensores();
            var viewModelList = _mapper.Map<IEnumerable<SensorAllViewModel>>(sensores);

            if (viewModelList == null || !viewModelList.Any())
            {
                return NotFound("Não há Sensores cadastrados");
            }
            else
            {
                return Ok(viewModelList);
            }
        }

        [MapToApiVersion(2)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<SensorPaginacaoReferenceViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var sensores = _sensorService.ListarSensoresReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<SensorAllViewModel>>(sensores);
            var viewModel = new SensorPaginacaoReferenceViewModel
            {
                Sensores = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().SensorId
            };
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<SensorAllViewModel> Get(int id)
        {
            var sensor = _sensorService.ObterSensorPorId(id);

            if (sensor != null)
            {
                var viewModel = _mapper.Map<SensorAllViewModel>(sensor);
                return Ok(viewModel);
            }
            else
            {
                return NotFound($"Sensor {id} não foi localizado.");
            }
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "operador, gerente")]
        public ActionResult Post([FromBody] SensorAllViewModel viewModel)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             var sensorModel = _mapper.Map<SensorModel>(viewModel);
             _sensorService.CriarSensor(sensorModel);

             return CreatedAtAction(nameof(Get), new { id = sensorModel.SensorId }, sensorModel);
         }

        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public IActionResult Put([FromRoute] int id, [FromBody] SensorAllViewModel viewModel)
        {
            var sensor = _sensorService.ObterSensorPorId(id);

            if (viewModel.SensorId == id)
            {
                var sensorModel = _mapper.Map<SensorModel>(viewModel);
                _sensorService.AtualizarSensor(sensorModel);
                return Ok($"Sensor {id} foi Atualizado com Sucesso.");
            }
            else if (sensor == null)
            {
                return BadRequest($"Sensor com ID {id} não encontrado.");
            }
            else
            {
                return BadRequest("O ID do Sensor na requisição não corresponde ao ID fornecido na rota.");
            }

        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete(int id)
        {
            var sensor = _sensorService.ObterSensorPorId(id);

            if (sensor == null)
            {
                return NotFound($"Sensor {id} não foi localizado.");
            }

            _sensorService.DeletarSensor(id);

            return Ok($"Sensor com ID {id} excluído com sucesso.");
        }

    }

} //FIM
