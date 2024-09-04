using Microsoft.AspNetCore.Mvc.Rendering;

namespace HMS.Abstractions
{
    public interface IMasterService
    {
        IEnumerable<(Guid Id, string Name)> GetPatientNames();
        IEnumerable<SelectListItem> GetDoctorDropdownList();
        IEnumerable<SelectListItem> GetDepartmentName();
    }
}
