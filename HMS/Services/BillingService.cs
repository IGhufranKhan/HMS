using HMS.Abstractions;
using HMS.Models;

namespace HMS.Services
{
    public class BillingService : IBillingService
    {
        //private static List<Billing> _billings = Seeds.Billing();
        private readonly HmsContext _hmsContext;
        public BillingService(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }
        public void AddBilling(Billing billing)
        {
            _hmsContext.Billings.Add(billing);
        }

        public void DeleteBilling(Billing billing)
        {
            _hmsContext.Billings.Remove(billing);
        }

        public void DeleteBilling(Guid id)
        {
            Billing? billing = GetBillingById(id);

            DeleteBilling(billing);
        }

        public Billing? GetBillingById(Guid id)
        {
            return _hmsContext.Billings.FirstOrDefault(m => m.Id == id);
        }

        public List<Billing> GetBillings()
        {
            var _billings = _hmsContext.Billings.ToList();
            return _billings;
        }
    }
}
