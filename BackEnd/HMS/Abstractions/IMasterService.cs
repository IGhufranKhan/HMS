using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Abstractions
{
    public interface IMasterService
    {
        List<SelectListItem> GetPatientNames();
        List<SelectListItem> GetDoctorDropdownList();
        List<SelectListItem> GetDepartmentName();
    }
}
