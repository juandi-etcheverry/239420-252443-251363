
using Domain;

namespace DataAccess.Interfaces
{
    public interface IColorRepository
    {
        IList<Color> GetAllColors();
    }
}
