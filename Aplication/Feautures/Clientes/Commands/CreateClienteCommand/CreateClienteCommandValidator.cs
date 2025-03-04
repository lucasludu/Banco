using FluentValidation;
using System.Globalization;

namespace Application.Feautures.Clientes.Commands.CreateClienteCommand
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.FechaNacimiento)
                .NotEmpty().WithMessage("Fecha de nacimiento no puede estar vacía.")
                .Must(fecha => fecha != default(DateTime))
                .WithMessage("Fecha de nacimiento debe ser una fecha válida.");

            RuleFor(p => p.Telefono)
               .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
               .Matches(@"^\d{4}-\d{4}$").WithMessage("{PropertyName} debe cumplir con el formato 0000-0000")
               .MaximumLength(9).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
               .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
               .MaximumLength(100).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");

            RuleFor(p => p.Direccion)
               .NotEmpty().WithMessage("{PropertyName} no puede estar vacio.")
               .MaximumLength(120).WithMessage("{PropertyName} no puede exceder de {MaxLengh} caracteres");
        }


        private bool EsFormatoValido(string fecha)
        {
            return DateTime.TryParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

    }
}
