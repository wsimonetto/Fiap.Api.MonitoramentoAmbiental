using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Fiap.Api.MonitoramentoAmbiental.Model;
using Fiap.Api.MonitoramentoAmbiental.Services;
using Fiap.Api.MonitoramentoAmbiental.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.AlertaViewModels;
using Asp.Versioning;

namespace Fiap.Api.MonitoramentoAmbiental.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public class AlertaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAlertaService _alertaService;

        public AlertaController(
            IMapper mapper,
            IAlertaService alertaService
        )
        {
            _mapper = mapper;
            _alertaService = alertaService;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public ActionResult<IEnumerable<AlertaAllViewModel>> Get()
        {
            var alerta = _alertaService.ListarAlertas();
            var viewModelList = _mapper.Map<IEnumerable<AlertaAllViewModel>>(alerta);

            if (viewModelList == null || !viewModelList.Any())
            {
                return NotFound("Não há Alertas cadastrados");
            }
            else
            {
                return Ok(viewModelList);
            }
        }

        [MapToApiVersion(2)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<AlertaPaginacaoReferenceViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var alertas = _alertaService.ListarAlertasReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<AlertaAllViewModel>>(alertas);
            var viewModel = new AlertaPaginacaoReferenceViewModel
            {
                Alertas = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().AlertaId
            };
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<AlertaModel> Get(int id)
        {
            var alerta = _alertaService.ObterAlertasPorId(id);

            if (alerta != null)
            {
                 var viewModel = _mapper.Map<AlertaAllViewModel>(alerta);
                return Ok(viewModel);
            }
            else
            {
               return NotFound($"Alerta {id} não foi localizados.");
            }
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "operador, gerente")]
        public ActionResult Post([FromBody] AlertaAllViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alertaModel = _mapper.Map<AlertaModel>(viewModel);
            _alertaService.CriarAlerta(alertaModel);

            return CreatedAtAction(nameof(Get), new { id = alertaModel.AlertaId }, alertaModel);
        }

        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public IActionResult Put([FromRoute] int id, [FromBody] AlertaAllViewModel viewModel)
        {
            var alerta = _alertaService.ObterAlertasPorId(id);

            if (viewModel.AlertaId == id)
            {
                var alertaModel = _mapper.Map<AlertaModel>(viewModel);
                _alertaService.AtualizarAlerta(alertaModel);

                return Ok($"Alerta {id} foi Atualizada com Sucesso.");
            }
            else if (alerta == null)
            {
                return BadRequest($"Alerta com ID {id} não encontrado.");
            }
            else
            {
                return BadRequest("O ID do Alerta na requisição não corresponde ao ID fornecido na rota.");
            }
        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete(int id)
        {
            var alerta = _alertaService.ObterAlertasPorId(id);

            if (alerta == null)
            {
                return NotFound($"Alerta {id} não foi localizado.");
            }
            _alertaService.DeletarAlerta(id);

            return Ok($"Alerta com ID {id} excluído com sucesso.");
        }

    }

} //FIM
