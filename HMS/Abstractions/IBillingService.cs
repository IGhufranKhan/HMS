using HMS.Models;

namespace HMS.Abstractions
{
    public interface IBillingService
    {
        void AddBilling(Billing billing);

        void DeleteBilling(Billing billing);

        void DeleteBilling(Guid id);

        Billing? GetBillingById(Guid id);

        List<Billing> GetBillings();
        void UpdateBilling(Billing billing);
    }
}
