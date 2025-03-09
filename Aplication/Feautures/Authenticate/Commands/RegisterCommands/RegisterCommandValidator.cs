using Application.Feautures.Usuarios.Commands.RegisterCommand;
using FluentValidation;

namespace Application.Feautures.Authenticate.Commands.RegisterCommands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Nombre)
               .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
               .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
               .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
               .MaximumLength(100).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres")
                .Equal(p => p.Password).WithMessage("{PropertyName} debe ser igual a Password.");
        }
    }
}
