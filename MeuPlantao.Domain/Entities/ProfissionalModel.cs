using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Domain.Entities;

//Modelo do banco de Profissional(enfermeiro/médico etc...)
public class ProfissionalModel
{
    public long Id { get; set; }
    [MaxLength(100)]
    public string Nome { get; set; } = String.Empty;
    [MaxLength(10)]
    public string Crm { get; set; } = String.Empty;
    [MaxLength(9)]
    public string Telefone { get; set; } = String.Empty; 
    public long UserId { get; set; } //FK
    public UserModel User { get; set; } //navegação
    
}