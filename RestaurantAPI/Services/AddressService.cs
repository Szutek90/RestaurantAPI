using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Services
{
    public interface IAddressService
    {
        void Delete(int id);
    }

    public class AddressService : IAddressService
    {
        private readonly RestaurantDbContext _dbContext;

        public AddressService(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete( int id)
        {
            var address = _dbContext.Address.FirstOrDefault(x => x.Id == id);
            if (address is null) throw new NotFoundException("Not founbd corresponding address to delete");
            _dbContext.Address.Remove(address);
            _dbContext.SaveChanges();
        }
    }
}
