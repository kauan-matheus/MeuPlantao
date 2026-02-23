namespace MeuPlantao.Model;

//Modelo do banco de Profissional(enfermeiro/médico etc...)
public class ProfissionalModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = String.Empty;
    public string Crm { get; set; } = String.Empty;
    public string Telefone { get; set; } = String.Empty; 
    public int UserId { get; set; } //FK
    public UserModel User { get; set; } //navegação
    
}