using FluentValidation;
using GestionNotes.Core.Models;

namespace GestionNotes.Application.Validators;

public class NoteValidator : AbstractValidator<NoteModel>
{
    public NoteValidator()
    {
        RuleFor(x => x.EleveId).NotEmpty().WithMessage("L'élève est requis");
        RuleFor(x => x.MatiereId).GreaterThan(0).WithMessage("La matière est requise");
        RuleFor(x => x.Valeur)
            .InclusiveBetween(0, 20)
            .WithMessage("La note doit être comprise entre 0 et 20");
        RuleFor(x => x.Semestre)
            .InclusiveBetween(1, 2)
            .WithMessage("Le semestre doit être 1 ou 2");
        RuleFor(x => x.Annee)
            .NotEmpty().WithMessage("L'année académique est requise")
            .MaximumLength(9).WithMessage("Format année invalide (ex: 2025-2026)");
    }
}
