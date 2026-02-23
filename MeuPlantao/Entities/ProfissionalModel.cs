using System.ComponentModel.DataAnnotations;
namespace MeuPlantao.Entities;

//Modelo do banco de Profissional(enfermeiro/médico etc...)
public class ProfissionalModel
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Nome { get; set; } = String.Empty;
    [MaxLength(10)]
    public string Crm { get; set; } = String.Empty;
    [MaxLength(9)]
    public string Telefone { get; set; } = String.Empty; 
    public int UserId { get; set; } //FK
    public UserModel? User { get; set; } //navegação
    
}