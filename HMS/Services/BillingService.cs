using HMS.Abstractions;
using HMS.Models;
using System.Security.Cryptography.Xml;

namespace HMS.Services
{
    public class BillingService : IBillingService
    {
        //private static List<Billing> _billings = Seeds.Billing();
        private readonly HmsContext _hmsContext;
        private readonly IPatientService _patientService;
        public BillingService(HmsContext hmsContext, IPatientService patientService)
        {
            _hmsContext = hmsContext;
            _patientService = patientService;
        }
        public void AddBilling(Billing billing)
        {
            _hmsContext.Billings.Add(billing);
            _hmsContext.SaveChanges();
        }

        public void DeleteBilling(Billing billing)
        {
            _hmsContext.Billings.Remove(billing);
        }

        public void DeleteBilling(Guid id)
        {
            var model = GetBillingById(id);
            if (model != null)
            {
                model.IsActive = false;
                _hmsContext.Billings.Update(model);
                _hmsContext.SaveChanges();
            }
        }

        public Billing? GetBillingById(Guid id)
        {
            return GetBillings().FirstOrDefault(m => m.Id == id);
        }

        public List<Billing> GetBillings()
        {
            var _billings = _hmsContext.Billings.Where(x => x.IsActive == true || x.IsActive == null).ToList();
            var patient = _patientService.GetPatientById(_billings.Select(x => x.PatientId).FirstOrDefault());
            _billings.Select(x => x.Patient == patient);


            return _billings;
        }
        public void UpdateBilling(Billing billing)
        {
            if (billing != null)
            {
                var model = GetBillingById(billing.Id);
                if (model != null)
                {
                    model.PatientId = billing.PatientId;
                    model.DoctorId = billing.DoctorId;
                    model.Amount = billing.Amount;
                    model.BillingDate = billing.BillingDate;
                    model.IsPaid = billing.IsPaid;
                    _hmsContext.Billings.Update(model);
                    _hmsContext.SaveChanges();

                }
                
            }

        }
    }
}
