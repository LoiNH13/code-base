using Domain.Core.Primitives.Result;

namespace Sale.Application.TimeFrames.Commands.Creates.Create;

public sealed class CreateTimeFrameCommand : ICommand<Result>
{
    public CreateTimeFrameCommand(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public int Year { get; set; }

    public int Month { get; set; }
}
