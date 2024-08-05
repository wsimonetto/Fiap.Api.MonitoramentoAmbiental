using Microsoft.AspNetCore.Mvc;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.LeituraViewModels;
using Fiap.Api.MonitoramentoAmbiental.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Fiap.Api.MonitoramentoAmbiental.Model;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.AlertaViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels;

namespace Fiap.Api.MonitoramentoAmbiental.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public class LeituraController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILeituraService _leituraService;
        private readonly ISensorService _sensorService;

        public LeituraController(IMapper mapper, ILeituraService leituraService, ISensorService sensorService)
        {
            _mapper = mapper;
            _leituraService = leituraService;
            _sensorService = sensorService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<LeituraViewModel>> Get()
        {
            var leituras = _leituraService.ListarLeituras();

            if (leituras == null || !leituras.Any())
            {
                return NotFound("Não há leituras cadastradas");
            }

            var leituraViewModels = leituras.Select(leitura =>
            {
                var sensor = _sensorService.ObterSensorPorId(leitura.SensorId);

                return new LeituraViewModel
                {
                    LeituraId = leitura.LeituraId,
                    Valor = leitura.Valor,
                    DataHora = leitura.DataHora,
                    SensorId = leitura.SensorId,
                    TipoSensor = sensor?.TipoSensor, // null conditional operator para evitar exceções se sensor for null
                    Localizacao = sensor?.Localizacao // null conditional operator para evitar exceções se sensor for null
                };
            }).ToList();

            return Ok(leituraViewModels);
        }

        [MapToApiVersion(2)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<LeituraPaginacaoReferenceViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var leituras = _leituraService.ListarLeiturasReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<LeituraViewModel>>(leituras);
            var viewModel = new LeituraPaginacaoReferenceViewModel
            {
                Leituras = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().LeituraId
            };
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<LeituraViewModel> Get(int id)
        {
            var leitura = _leituraService.ObterLeituraPorId(id);

            if (leitura != null)
            {
                var sensor = _sensorService.ObterSensorPorId(leitura.SensorId); // Busca o sensor associado à leitura
                var viewModel = new LeituraViewModel
                {
                    LeituraId = leitura.LeituraId,
                    Valor = leitura.Valor,
                    DataHora = leitura.DataHora,
                    SensorId = leitura.SensorId,
                    TipoSensor = sensor.TipoSensor,
                    Localizacao = sensor.Localizacao
                };
                return Ok(viewModel);
            }
            else
            {
                return NotFound($"Leitura {id} não foi localizada.");
            }
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "operador, gerente")]
        public ActionResult Post([FromBody] LeituraCreateAndEditViewlModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var leituraModel = _mapper.Map<LeituraModel>(viewModel);
                _leituraService.CriarLeitura(leituraModel);

                // Após criar a leitura com sucesso, busca o tipo do sensor e a descrição correspondentes ao SensorId da leitura criada
                var sensor = _sensorService.ObterSensorPorId(leituraModel.SensorId);

                // Cria um objeto LeituraViewModel com os dados da leitura e os dados do sensor
                var leituraViewModel = new LeituraViewModel
                {
                    LeituraId = leituraModel.LeituraId,
                    Valor = leituraModel.Valor,
                    DataHora = leituraModel.DataHora,
                    SensorId = leituraModel.SensorId,
                    TipoSensor = sensor.TipoSensor,
                    Localizacao = sensor.Localizacao
                };

                return CreatedAtAction(nameof(Get), new { id = leituraViewModel.LeituraId }, leituraViewModel);
            }
            catch (ArgumentException ex)
            {
                // captura a exceção de argumento inválido no repository de leitura Add e retorna BadRequest com a mensagem de erro
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao processar a requisição: {ex.Message}");
            }
        }

        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public IActionResult Put([FromRoute] int id, [FromBody] LeituraCreateAndEditViewlModel viewModel)
        {

            var leitura = _leituraService.ObterLeituraPorId(id);

            try
            {
                if (viewModel.LeituraId == id)
                {
                    var leituraModel = _mapper.Map<LeituraModel>(viewModel);
                    _leituraService.AtualizarLeitura(leituraModel);

                    var sensor = _sensorService.ObterSensorPorId(leituraModel.SensorId);

                    var leituraViewModel = new LeituraViewModel
                    {
                        LeituraId = leituraModel.LeituraId,
                        Valor = leituraModel.Valor,
                        DataHora = leituraModel.DataHora,
                        SensorId = leituraModel.SensorId,
                        TipoSensor = sensor.TipoSensor,
                        Localizacao = sensor.Localizacao
                    };

                    return CreatedAtAction(nameof(Get), new { id = leituraViewModel.LeituraId }, leituraViewModel);
                    //return Ok($"Leitura {id} foi Atualizado com Sucesso.");
                }
                else if (leitura == null)
                {
                    return BadRequest($"Leitura com ID {id} não encontrado.");
                }
                else
                {
                    return BadRequest($"A Leirura com ID {id} na requisição não corresponde ao ID fornecido na rota.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete(int id)
        {
            var leitura = _leituraService.ObterLeituraPorId(id);

            if (leitura == null)
            {
                return NotFound($"Leitura {id} não foi localizado.");
            }

            _leituraService.DeletarLeitura(id);

            return Ok($"Leitura com ID {id} excluída com sucesso.");
        }

    }

} //FIM
