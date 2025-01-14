using FluentValidation.Results;

namespace backend.Services.ErrorService;

public interface IErrorService
{
    public Dictionary<string, IEnumerable<string>> SanitazeError(IEnumerable<ValidationFailure> errors);
}