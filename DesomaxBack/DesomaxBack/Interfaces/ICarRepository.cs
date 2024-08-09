using DesomaxBack.Common;
using DesomaxBack.ViewModels;

namespace DesomaxBack.Interfaces
{
    public interface ICarRepository
    {
        Task<JsonReturn> InsertCar(InsertCarViewModel request);
    }
}
