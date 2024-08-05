using Asp.Versioning;
using AutoMapper;
using Fiap.Api.MonitoramentoAmbiental.Model;
using Fiap.Api.MonitoramentoAmbiental.Services;
using Fiap.Api.MonitoramentoAmbiental.ViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.ControleIrrigacaoViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.SensorViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fiap.Api.MonitoramentoAmbiental.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public class ControleIrrigacaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IControleIrrigacaoService _controleIrrigacaoService;

        public ControleIrrigacaoController(IControleIrrigacaoService controleIrrigacaoService, IMapper mapper)
        {
            _controleIrrigacaoService = controleIrrigacaoService;
            _mapper = mapper;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<ControleIrrigacaoAllViewModel>> Get()
        {
            var controles = _controleIrrigacaoService.ListarControlesIrrigacoes();
            var viewModelList = _mapper.Map<IEnumerable<ControleIrrigacaoAllViewModel>>(controles);

            if (viewModelList == null || !viewModelList.Any())
            {
                return NotFound("Não há controles de irrigação cadastrados.");
            }
            return Ok(viewModelList);
        }

        [MapToApiVersion(2)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<ControleIrrigacaoPaginacaoReferenceViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var controleIrrigacoes = _controleIrrigacaoService.ListarControlesIrrigacoesReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<ControleIrrigacaoAllViewModel>>(controleIrrigacoes);
            var viewModel = new ControleIrrigacaoPaginacaoReferenceViewModel
            {
                ControleIrrigacoes = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().ControleId
            };
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<ControleIrrigacaoAllViewModel> Get(int id)
        {
            var controle = _controleIrrigacaoService.ObterControleIrrigacaoPorId(id);
            if (controle == null)
            {
                return NotFound($"Controle de irrigação com ID {id} não encontrado.");
            }

            var viewModel = _mapper.Map<ControleIrrigacaoAllViewModel>(controle);
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "operador, gerente")]
        public ActionResult Post([FromBody] ControleIrrigacaoAllViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var controleModel = _mapper.Map<ControleIrrigacaoModel>(viewModel);

                _controleIrrigacaoService.CriarControleIrrigacao(controleModel);

                viewModel.ControleId = controleModel.ControleId;

                return CreatedAtAction(nameof(Get), new { id = controleModel.ControleId }, viewModel);
            }
            catch (ArgumentException ex)
            {
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
        public IActionResult Put([FromRoute] int id, [FromBody] ControleIrrigacaoAllViewModel viewModel)
        {
            if (id != viewModel.ControleId)
            {
                return BadRequest("O ID do controle na requisição não corresponde ao ID fornecido na rota.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var controleModel = _controleIrrigacaoService.ObterControleIrrigacaoPorId(id);
            if (controleModel == null)
            {
                return NotFound($"Controle de irrigação com ID {id} não encontrado.");
            }

            _mapper.Map(viewModel, controleModel);

            try
            {
                _controleIrrigacaoService.AtualizarControleIrrigacao(controleModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao processar a requisição: {ex.Message}");
            }

            return Ok($"Controle de irrigação com ID {id} foi atualizado com sucesso.");
        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete(int id)
        {
            var controleModel = _controleIrrigacaoService.ObterControleIrrigacaoPorId(id);
            if (controleModel == null)
            {
                return NotFound($"Controle de irrigação com ID {id} não encontrado.");
            }

            _controleIrrigacaoService.DeletarControleIrrigacao(id);

            return Ok($"Controle de irrigação com ID {id} foi excluído com sucesso.");
        }

    }

} //FIM
