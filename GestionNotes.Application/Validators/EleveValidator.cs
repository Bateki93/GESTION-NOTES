using FluentValidation;
using GestionNotes.Core.Models;

namespace GestionNotes.Application.Validators;

public class EleveValidator : AbstractValidator<EleveModel>
{
    public EleveValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");
        RuleFor(x => x.Prenom)
            .NotEmpty().WithMessage("Le prénom est requis")
            .MaximumLength(100).WithMessage("Le prénom ne peut pas dépasser 100 caractères");
        RuleFor(x => x.Matricule)
            .NotEmpty().WithMessage("Le matricule est requis")
            .MaximumLength(20).WithMessage("Le matricule ne peut pas dépasser 20 caractères");
    }
}
