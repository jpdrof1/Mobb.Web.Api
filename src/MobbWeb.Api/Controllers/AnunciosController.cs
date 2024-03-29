using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobbWeb.Api.Models.Input;
using MobbWeb.Api.Repositories.Interfaces;

namespace MobbWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AnunciosController : ControllerBase
{

  private readonly IAnuncioRepository _anuncioRepository;

  public AnunciosController(IAnuncioRepository anuncioRepository)
  {
    _anuncioRepository = anuncioRepository;
  }

  #region Insere anúncio
  [Authorize]
  [HttpPost]
  [Route("insere-anuncio")]
  public async Task<ActionResult<dynamic>> InsereAnuncio([FromBody] Anuncio anuncio)
  {
    try
    {
      await _anuncioRepository.InsereAnuncio(anuncio.idPessoa,
                                             anuncio.idCategoriaAnuncio,
                                             anuncio.idCidade,
                                             anuncio.tituloAnuncio,
                                             anuncio.descricaoAnuncio,
                                             anuncio.valorServicoAnuncio,
                                             anuncio.horasServicoAnuncio,
                                             anuncio.telefoneContatoAnuncio,
                                             anuncio.urlImagensAnuncio);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(400, ex.Message);
    }
  }
  #endregion

  #region Verifica se existe anúncios  
  [AllowAnonymous]
  [HttpGet]
  [Route("verifica-anuncios/{idEstado}/{idCidade}/{idCategoriaAnuncio}")]
  public async Task<bool> VerificaAnuncios(int idEstado,
                                           int idCidade,
                                           int idCategoriaAnuncio)
  {
    try
    {
      return await _anuncioRepository.VerificaAnuncios(idEstado, idCidade, idCategoriaAnuncio);
    }
    catch
    {
      throw new Exception("Ocorreu um erro na requisição");
    }
  }
  #endregion

  #region Lista anúncios
  [AllowAnonymous]
  [HttpGet]
  [Route("lista-anuncios/{idEstado}/{idCidade}/{idCategoriaAnuncio}/{offSet}/{limite}")]
  public async Task<IActionResult> ListaAnuncios(int idEstado,
                                                 int idCidade,
                                                 int idCategoriaAnuncio,
                                                 int offSet,
                                                 int limite,
                                                 string ordenacao = "",
                                                 string titulo = "")
  {
    try
    {
      return Ok(await _anuncioRepository.ListaAnuncios(idEstado,
                                                       idCidade,
                                                       idCategoriaAnuncio,
                                                       offSet,
                                                       limite,
                                                       ordenacao,
                                                       titulo));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  [AllowAnonymous]
  [HttpGet]
  [Route("lista-anuncios/{idAnuncio}")]
  public async Task<IActionResult> ListaAnuncios(int idAnuncio)
  {
    try
    {
      return Ok(await _anuncioRepository.ListaAnuncio(idAnuncio));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  [Authorize]
  [HttpGet]
  [Route("lista-anuncios-pessoa/{idPessoa}/{offSet}/{limite}")]
  public async Task<IActionResult> ListaAnunciosPessoa(int idPessoa,
                                                       int offSet,
                                                       int limite,
                                                       string ordenacao = "",
                                                       string titulo = "")
  {
    try
    {
      return Ok(await _anuncioRepository.ListaAnunciosPessoa(idPessoa,
                                                             offSet,
                                                             limite,
                                                             ordenacao,
                                                             titulo));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

  [Authorize]
  [HttpGet]
  [Route("anuncios-favoritos/{idPessoa}/{offSet}/{limite}")]
  public async Task<IActionResult> ListaAnunciosFavoritos(int idPessoa,
                                                          int offSet,
                                                          int limite,
                                                          string ordenacao = "",
                                                          string titulo = "")
  {
    try
    {
      return Ok(await _anuncioRepository.ListaAnunciosFavoritos(idPessoa,
                                                                offSet,
                                                                limite,
                                                                ordenacao,
                                                                titulo));
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
  
  #endregion  

  #region Lista as categorias dos anúncios
  [AllowAnonymous]
  [HttpGet]
  [Route("lista-categorias-anuncio")]
  public async Task<IActionResult> ListaCategoriasAnuncio()
  {
    try
    {
      return Ok(await _anuncioRepository.ListaCategoriasAnuncio());
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
  #endregion

  #region Lista as imagens dos anúncios
  [AllowAnonymous]
  [HttpGet]
  [Route("lista-imagens-anuncio/{idAnuncio}")]
  public async Task<IActionResult> ListaImagensAnuncio(int idAnuncio)
  {
    try
    {
      return Ok(await _anuncioRepository.ListaImagensAnuncio(idAnuncio));
    }
    catch (Exception ex)
    {
      return StatusCode(400, ex.Message);
    }
  }

  #endregion

  #region Atualiza os dados do anúncio
  [Authorize]
  [HttpPost]
  [Route("atualiza-anuncio")]
  public async Task<dynamic> AtualizaAnuncio([FromBody] Anuncio anuncio)
  {
    try
    {
      await _anuncioRepository.AtualizaAnuncio(anuncio.idAnuncio,
                                               anuncio.tituloAnuncio,
                                               anuncio.descricaoAnuncio,
                                               anuncio.valorServicoAnuncio,
                                               anuncio.horasServicoAnuncio,
                                               anuncio.telefoneContatoAnuncio,
                                               anuncio.idCategoriaAnuncio,
                                               anuncio.idCidade,
                                               anuncio.urlImagensAnuncio,
                                               anuncio.urlImagensAnuncioDel);

      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(400, ex.Message);
    }
  }
  #endregion

  #region Deleta o anúncio
  [Authorize]
  [HttpDelete]
  [Route("deleta-anuncio/{idAnuncio}")]
  public async Task<IActionResult> DeletaAnuncio(int idAnuncio)
  {
    try
    {
      await _anuncioRepository.DeletaAnuncio(idAnuncio);

      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista os comentários do anúncio
  [AllowAnonymous]
  [HttpGet]
  [Route("lista-comentarios-anuncio/{idAnuncio}")]
  public async Task<IActionResult> ListaComentariosAnuncio(int idAnuncio)
  {
    try
    {
      return Ok(await _anuncioRepository.ListaComentariosAnuncio(idAnuncio));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Insere comentário no anúncio
  [Authorize]
  [HttpPost]
  [Route("insere-comentario-anuncio")]
  public async Task<IActionResult> InsereComentario([FromBody] Comentario comentario)
  {
    try
    {
      await _anuncioRepository.InsereComentario(comentario.idAnuncio,
                                                comentario.idPessoa,
                                                comentario.comentario,
                                                comentario.idComentarioAnuncioPai);

      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista os comentários resposta do anúncio
  [AllowAnonymous]
  [HttpGet]
  [Route("retorna-lista-comentarios")]
  public async Task<IActionResult> ListaComentariosAnuncioResposta(int idAnuncio)
  {
    try
    {
      return Ok(await _anuncioRepository.ListaComentariosAnuncioResposta(idAnuncio));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista os ID dos anúncios favoritos da pessoa
  [AllowAnonymous]
  [HttpGet]
  [Route("id-anuncios-favoritos/{idPessoa}")]
  public async Task<IActionResult> ListaIDAnunciosFavoritos(int idPessoa)
  {
    try
    {
      return Ok(await _anuncioRepository.ListaIDAnunciosFavoritos(idPessoa));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Adiciona o anúncio em favoritos da pessoa
  [Authorize]
  [HttpPost]
  [Route("insere-anuncio-favorito/{idPessoa}/{idAnuncio}")]
  public async Task<IActionResult> InsereAnuncioFavorito(int idPessoa,
                                                         int idAnuncio)
  {
    try
    {
      await _anuncioRepository.InsereAnuncioFavorito(idPessoa,
                                                     idAnuncio);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Verifica se o anúncio está favoritado para a pessoa
  [AllowAnonymous]
  [HttpGet]
  [Route("verifica-anuncio-favorito/{idPessoa}/{idAnuncio}")]
  public async Task<bool> VerificaAnuncioFavorito(int idPessoa,
                                                  int idAnuncio)
  {
    try
    {
      return await _anuncioRepository.VerificaAnuncioFavorito(idPessoa,
                                                              idAnuncio);
    }
    catch (Exception)
    {
      throw new Exception("Ocorreu um erro na requisição");
    }
  }
  #endregion

  #region Remove o anúncio dos favoritos da pessoa
  [Authorize]
  [HttpDelete]
  [Route("remove-anuncio-favorito/{idPessoa}/{idAnuncio}")]
  public async Task<IActionResult> RemoveAnuncioFavorito(int idPessoa,
                                                         int idAnuncio)
  {
    try
    {
      await _anuncioRepository.RemoveAnuncioFavorito(idPessoa,
                                                     idAnuncio);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Insere a avaliação no anúncio
  [Authorize]
  [HttpPost]
  [Route("insere-avaliacao-anuncio/{idAnuncio}/{idPessoa}/{avaliacao}")]
  public async Task<IActionResult> InsereAvaliacaoAnuncio(int idAnuncio,
                                                          int idPessoa,
                                                          int avaliacao)
  {
    try
    {
      await _anuncioRepository.InsereAvaliacaoAnuncio(idAnuncio,
                                                      idPessoa,
                                                      avaliacao);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista a avaliação feita pela pessoa no anúncio
  [Authorize]
  [HttpGet]
  [Route("avaliacao-anuncio-pessoa/{idAnuncio}/{idPessoa}")]
  public async Task<decimal> AvaliacaoAnuncioPessoa(int idAnuncio,
                                                int idPessoa)
  {
    try
    {
      return await _anuncioRepository.AvaliacaoAnuncioPessoa(idAnuncio,
                                                             idPessoa);
    }
    catch
    {
      throw new Exception("Ocorreu um erro na requisição");
    }
  }
  #endregion

  #region Deleta o comentário do anúncio
  [Authorize]
  [HttpDelete]
  [Route("deleta-comentario-anuncio/{idComentario}")]
  public async Task<IActionResult> DeletaComentarioAnuncio(int idComentario)
  {
    try
    {
      await _anuncioRepository.DeletaComentarioAnuncio(idComentario);
      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista os dados do relatório dos dados cadastrais dos anúncios
  [Authorize]
  [HttpGet]
  [Route("relatorio-anuncios-cadastrados-pessoa")]
  public async Task<IActionResult> RelAnunciosCadPessoa(int idPessoa,
                                                         int idCategoriaAnuncio,
                                                         DateTime dataCadastroInicial,
                                                         DateTime dataCadastroFinal,
                                                         int avaliacaoInicial,
                                                         int avaliacaoFinal)
  {
    try
    {
      return Ok(await _anuncioRepository.RelAnunciosCadPessoa(idPessoa,
                                                              idCategoriaAnuncio,
                                                              dataCadastroInicial,
                                                              dataCadastroFinal,
                                                              avaliacaoInicial,
                                                              avaliacaoFinal));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }

  }
  #endregion

  #region Lista os dados do relatório das interações nos anúncios da pessoa logada
  [Authorize]
  [HttpGet]
  [Route("relatorio-interacoes-anuncios-cadastrados-pessoa")]
  public async Task<IActionResult> RelInteracoesAnunciosCadPessoa(int idPessoa,
                                                                   int idCategoriaAnuncio,
                                                                   DateTime dataCadastroInicial,
                                                                   DateTime dataCadastroFinal,
                                                                   int avaliacaoInicial,
                                                                   int avaliacaoFinal)
  {
    try
    {
      return Ok(await _anuncioRepository.RelInteracoesAnunciosCadPessoa(idPessoa,
                                                                        idCategoriaAnuncio,
                                                                        dataCadastroInicial,
                                                                        dataCadastroFinal,
                                                                        avaliacaoInicial,
                                                                        avaliacaoFinal));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista os dados do relatório de interações com os anúncios favoritos
  [Authorize]
  [HttpGet]
  [Route("relatorio-interacoes-anuncios-favoritos-pessoa")]
  public async Task<IActionResult> RelInteracoesAnunciosFavPessoa(int idPessoa,
                                                                   int idCategoriaAnuncio,
                                                                   DateTime dataCadastroInicial,
                                                                   DateTime dataCadastroFinal,
                                                                   int avaliacaoInicial,
                                                                   int avaliacaoFinal)
  {
    try
    {
      return Ok(await _anuncioRepository.RelInteracoesAnunciosFavPessoa(idPessoa,
                                                                        idCategoriaAnuncio,
                                                                        dataCadastroInicial,
                                                                        dataCadastroFinal,
                                                                        avaliacaoInicial,
                                                                        avaliacaoFinal));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Lista os dados do relatório estatístico
  [Authorize]
  [HttpGet]
  [Route("relatorio-anuncios-estatistico")]
  public async Task<IActionResult> RelEstatisticasAnuncios(int idCategoriaAnuncio,
                                                            int idEstado,
                                                            int idCidade,
                                                            DateTime dataCadastroInicial,
                                                            DateTime dataCadastroFinal,
                                                            int avaliacaoInicial,
                                                            int avaliacaoFinal)
  {
    try
    {
      return Ok(await _anuncioRepository.RelEstatisticasAnuncios(idCategoriaAnuncio,
                                                                 idEstado,
                                                                 idCidade,
                                                                 dataCadastroInicial,
                                                                 dataCadastroFinal,
                                                                 avaliacaoInicial,
                                                                 avaliacaoFinal));
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Insere as informações das mensagens do pessoa com anúnciante
  [Authorize]
  [HttpPost]
  [Route("insere-mensagem-anuncio/{idAnuncio}/{idPessoa}")]
  public async Task<IActionResult> InsereMensagemAnuncio(int idAnuncio,
                                                         int idPessoa)
  {
    try
    {
      await _anuncioRepository.InsereMensagemAnuncio(idAnuncio,
                                                     idPessoa);

      return Ok();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  #endregion

  #region Verifica se a pessoa realizou interação com o anúncio
  [Authorize]
  [HttpGet]
  [Route("verifica-interacao-anuncio/{idAnuncio}/{idPessoa}")]
  public async Task<bool> VerificaInteracaoAnuncio(int idAnuncio,
                                                   int idPessoa)
  {
    try
    {
      return await _anuncioRepository.VerificaInteracaoAnuncio(idAnuncio, idPessoa);
    }
    catch
    {
      throw new Exception("Ocorreu um erro na requisição");
    }
  }
  #endregion
}