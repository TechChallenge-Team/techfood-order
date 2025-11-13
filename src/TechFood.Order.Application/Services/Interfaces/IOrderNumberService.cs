using System.Threading.Tasks;

namespace TechFood.Order.Application.Common.Services.Interfaces;

public interface IOrderNumberService
{
    Task<int> GetAsync();
}
