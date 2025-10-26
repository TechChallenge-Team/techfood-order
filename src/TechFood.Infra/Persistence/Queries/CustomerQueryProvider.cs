//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using TechFood.Domain.Enums;
//using TechFood.Infra.Persistence.Contexts;

//namespace TechFood.Infra.Persistence.Queries;

//internal class CustomerQueryProvider(TechFoodContext techFoodContext) : ICustomerQueryProvider
//{
//    public Task<CustomerDto?> GetByDocumentAsync(DocumentType documentType, string document)
//    {
//        return techFoodContext.Customers
//            .AsNoTracking()
//            .Where(c => c.Document.Type == documentType && c.Document.Value == document)
//            .Select(customer => new CustomerDto
//            {
//                Id = customer.Id,
//                DocumentType = customer.Document.Type,
//                DocumentValue = customer.Document.Value,
//                Name = customer.Name.FullName,
//                Email = customer.Email,
//                Phone = customer.Phone != null ? customer.Phone.Number : null
//            })
//            .FirstOrDefaultAsync();
//    }
//}
