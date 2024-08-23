using HMS.Abstractions;
using HMS.Data;
using HMS.Models;

namespace HMS.Services
{
    public class BillingService : IBillingService
    {
        private static List<Billing> _billings = Seeds.Billing();
        public void AddBilling(Billing billing)
        {
            _billings.Add(billing);
        }

        public void DeleteBilling(Billing billing)
        {
            _billings.Remove(billing);
        }

        public void DeleteBilling(Guid id)
        {
            Billing? billing = GetBillingById(id);

            DeleteBilling(billing);
        }

        public Billing? GetBillingById(Guid id)
        {
            return _billings.FirstOrDefault(m => m.Id == id);
        }

        public List<Billing> GetBillings()
        {
            _billings = Seeds.Billing();
            return _billings;
        }
    }
}
