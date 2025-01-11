using FluentValidation.Results;

namespace backend.Services.ErrorService;

public class ErrorService: IErrorService
{
    public Dictionary<string, IEnumerable<string>> SanitazeError(IEnumerable<ValidationFailure> errors)
    {
        var sanitazedErros = errors
            .GroupBy(err => err.PropertyName)
            .ToDictionary(
                err => err.Key,
                err => err.Select(e => e.ErrorMessage)
            );
        
        return sanitazedErros;
    }
}