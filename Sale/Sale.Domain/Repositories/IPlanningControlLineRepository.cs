using Sale.Domain.Entities.Planning;

namespace Sale.Domain.Repositories;

public interface IPlanningControlLineRepository
{
    void Remove(PlanningControlLine entity);
}
