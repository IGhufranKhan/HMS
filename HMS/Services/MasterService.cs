using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace HMS.Services
{
    public class MasterService : IMasterService
    {
        private readonly HmsContext _hmsContext;
        public MasterService(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }

        public List<SelectListItem> GetPatientNames()
        {
            return _hmsContext.Patients
                              .Where(x => x.IsActive == true && x.Name != null)
                              .Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name })
                              .ToList();
        }


        public List<SelectListItem> GetDoctorDropdownList()
        {
            return _hmsContext.Doctors
                      .Where(x => x.IsActive == true)
                      .Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name })                    
                      .ToList();  

        }
        public List<SelectListItem> GetDepartmentName()
        {
            return _hmsContext.Departments
                              .Where(x => x.IsActive == true || x.IsActive == null && x.Name != null)
                              .Select(x => new SelectListItem{Value = x.Id.ToString(), Text = x.Name })
                              .ToList();

        }


        
    }
}
