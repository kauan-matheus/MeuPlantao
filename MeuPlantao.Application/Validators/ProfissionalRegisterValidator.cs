using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MeuPlantao.Communication.Dto.Requests;

namespace MeuPlantao.Application.Validators
{
    public class ProfissionalRegisterValidator : AbstractValidator<RequestProfissionalRegisterJson>
    {
        public ProfissionalRegisterValidator()
        { 
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatorio")
                .MaximumLength(100);

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role é obrigatorio");

            RuleFor(x => x.Crm)
                .NotEmpty().When(x => x.Role == Communication.Enums.ProfissionalRoleEnum.Medico).WithMessage("Para o medico Crm é obrigatorio")
                .MaximumLength(10);
            
            RuleFor(x => x.Coren)
                .NotEmpty().When(x => x.Role == Communication.Enums.ProfissionalRoleEnum.Enfermeiro).WithMessage("Para o enfermeiro Coren é obrigatorio")
                .MaximumLength(10);

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatorio")
                .MaximumLength(9);

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Usuario é obrigatorio");
        }
    }
}