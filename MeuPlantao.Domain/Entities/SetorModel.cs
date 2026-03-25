using System.ComponentModel.DataAnnotations;
namespace MeuPlantao.Domain.Entities;

//modelo do setor (Uti, pronto socorro)
public class SetorModel
{
    public long Id { get; set; }
    [MaxLength(100)]
    public string Nome { get; set; } = String.Empty;
    public long RepresentanteId { get; set; }
    public UserModel Representante { get; set; } = null!;
}