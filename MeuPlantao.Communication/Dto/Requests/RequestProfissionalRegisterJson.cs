using System.ComponentModel.DataAnnotations;

namespace MeuPlantao.Communication.Dto.Requests;

public class RequestProfissionalRegisterJson
{
    [MaxLength(100)]
    public string Nome { get; set; } = String.Empty;
    [MaxLength(10)]
    public string Crm { get; set; } = String.Empty;
    [MaxLength(9)]
    public string Telefone { get; set; } = String.Empty; 
}