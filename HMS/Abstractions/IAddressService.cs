using HMS.Models;

namespace HMS.Abstractions
{
    public interface IAddressService
    {
        void AddAddress(Address address);
        void UpdateAddress(Address address);


        void DeleteAddress(Address address);

        void DeleteAddress(Guid id);

        Address? GetAddressById(Guid id);

        List<Address> GetAddresss();
        List<Address> GetAddresss(string serachName);
    }
}
