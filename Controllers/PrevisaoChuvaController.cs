using Asp.Versioning;
using AutoMapper;
using Fiap.Api.MonitoramentoAmbiental.Model;
using Fiap.Api.MonitoramentoAmbiental.Services;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.AlertaViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels;
using Fiap.Api.MonitoramentoAmbiental.ViewModels.PrevisaoChuvaViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Fiap.Api.MonitoramentoAmbiental.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public class PrevisaoChuvaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPrevisaoChuvaService _previsaoChuvaService;

        public PrevisaoChuvaController(IPrevisaoChuvaService previsaoChuvaService, IMapper mapper)
        {
            _previsaoChuvaService = previsaoChuvaService;
            _mapper = mapper;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<PrevisaoChuvaAllViewModel>> Get()
        {
            var previsoes = _previsaoChuvaService.ListarPrevisoesChuva();
            var viewModelList = _mapper.Map<IEnumerable<PrevisaoChuvaAllViewModel>>(previsoes);

            if (viewModelList == null || !viewModelList.Any())
            {
                return NotFound("Não há Previsões de Chuva cadastradas.");
            }
            return Ok(viewModelList);
        }

        [MapToApiVersion(2)]
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<IEnumerable<PrevisaoChuvaPaginacaoReferenceViewModel>> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var previsoesChuva = _previsaoChuvaService.ListarPrevisoesChuvaReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<PrevisaoChuvaAllViewModel>>(previsoesChuva);
            var viewModel = new PrevisaoChuvaPaginacaoReferenceViewModel
            {
                PrevisoesChuva = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().PrevisaoChuvaId
            };
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<PrevisaoChuvaAllViewModel> Get(int id)
        {
            var previsao = _previsaoChuvaService.ObterPrevisaoChuvaPorId(id);
            if (previsao == null)
            {
                return NotFound($"Previsão de chuva com ID {id} não encontrada.");
            }

            var viewModel = _mapper.Map<PrevisaoChuvaAllViewModel>(previsao);
            return Ok(viewModel);
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize(Roles = "operador, gerente")]
        public ActionResult<PrevisaoChuvaAllViewModel> Post([FromBody] PrevisaoChuvaAllViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var previsaoModel = _mapper.Map<PrevisaoChuvaModel>(viewModel);

            _previsaoChuvaService.CriarPrevisaoChuva(previsaoModel);

            viewModel.PrevisaoChuvaId = previsaoModel.PrevisaoChuvaId;

            return CreatedAtAction(nameof(Get), new { id = previsaoModel.PrevisaoChuvaId }, viewModel);
        }

        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public IActionResult Put([FromRoute] int id, [FromBody] PrevisaoChuvaAllViewModel viewModel)
        {
            if (id != viewModel.PrevisaoChuvaId)
            {
                return BadRequest("O ID da Previsão de Chuva na requisição não corresponde ao ID fornecido na rota.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var previsaoModel = _previsaoChuvaService.ObterPrevisaoChuvaPorId(id);
            if (previsaoModel == null)
            {
                return NotFound($"Previsão de Chuva com ID {id} não encontrada.");
            }

            _mapper.Map(viewModel, previsaoModel);

            try
            {
                _previsaoChuvaService.AtualizarPrevisaoChuva(previsaoModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao processar a requisição: {ex.Message}");
            }

            return Ok($"Previsão de Chuva com ID {id} foi atualizada com sucesso.");
        }

        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete(int id)
        {
            var previsaoModel = _previsaoChuvaService.ObterPrevisaoChuvaPorId(id);
            if (previsaoModel == null)
            {
                return NotFound($"Previsão de Chuva com ID {id} não encontrada.");
            }

            _previsaoChuvaService.DeletarPrevisaoChuva(id);

            return Ok($"Previsão de Chuva com ID {id} foi excluído com sucesso.");
        }

    }

} //FIM
 