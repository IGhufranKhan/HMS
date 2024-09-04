using HMS.Abstractions;
using HMS.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HMS.Services
{
    public class AddressService : IAddressService
    {
        private readonly HmsContext _hmsContext;
        public AddressService(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }
        public void AddAddress(Address address)
        {
            _hmsContext.Addresses.Add(address);
            _hmsContext.SaveChanges();
        }

        public void DeleteAddress(Address address)
        {
            _hmsContext.Addresses.Remove(address);
            _hmsContext.SaveChanges();
        }

        public void DeleteAddress(Guid id)
        {
            Address? address = GetAddressById(id);
            DeleteAddress(address);
        }

        public Address? GetAddressById(Guid id)
        {
            return _hmsContext.Addresses.FirstOrDefault(m => m.Id == id);
        }

        public List<Address> GetAddresss()
        {
            var _addresss = _hmsContext.Addresses.ToList();
            //_addresss = _addresss.Where(x => x.Address == )
            return _addresss;
        }
        public List<Address> GetAddresss(string serachName)
        {
            return _hmsContext.Addresses.Where(x => x.Country.Contains(serachName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public void UpdateAddress(Address address)
        {
            _hmsContext.Addresses.Update(address);
            _hmsContext.SaveChanges();
        }
    }
}
