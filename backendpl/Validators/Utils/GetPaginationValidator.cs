using FluentValidation;

namespace backend.Validators;

public interface IPagination
{
    int Take { get; set; }
    int Skip { get; set; }
}

public class Pagination : IPagination
{
    public Pagination(int take, int skip)
    {
        Take = take;
        Skip = skip;
    }
    public int Take { get; set; }
    public int Skip { get; set; }
}

internal sealed class GetPaginationValidator:AbstractValidator<IPagination>
{
    public GetPaginationValidator()
    {
        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1).WithMessage("Take que ser maior ou igual a 0")
            .LessThan(40).WithMessage("Take que ser menor que 40");

        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip que ser maior ou igual a 0");
    }
}