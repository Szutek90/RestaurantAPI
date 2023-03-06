using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("/api/address")]
    public class AddressController:ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController( IAddressService addressService)
        {
            _addressService = addressService;
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            _addressService.Delete(id);
            return Ok();
        }
    }
}
