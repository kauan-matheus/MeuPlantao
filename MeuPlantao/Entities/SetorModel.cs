using System.ComponentModel.DataAnnotations;
namespace MeuPlantao.Entities;

//modelo do setor (Uti, pronto socorro)
public class SetorModel
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Nome { get; set; } = String.Empty;
}