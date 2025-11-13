using System.Threading.Tasks;

namespace TechFood.Order.Application.Services.Interfaces;

public interface IOrderNumberService
{
    Task<int> GetAsync();
}
