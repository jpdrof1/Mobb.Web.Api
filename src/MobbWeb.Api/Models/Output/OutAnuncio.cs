namespace MobbWeb.Api.Models.Output
{
  public class OutAnuncio
  {
    public int idAnuncio { get; set; }
    public int idPessoa { get; set; }    
    public string? tituloAnuncio { get; set; }
    public string? descricaoAnuncio { get; set; }
    public decimal valorServicoAnuncio { get; set; }
    public int horasServicoAnuncio { get; set; }
    public int idCategoriaAnuncio {get; set;}
    public string? nomeCategoriaAnuncio {get; set;}
    public int idCidade {get; set;}
    public string? nomeCidade {get; set;}
    public int idEstado {get; set;}
    public string? nomeEstado {get; set;}
    public string? UrlImagemAnuncio {get; set;}
  }
}