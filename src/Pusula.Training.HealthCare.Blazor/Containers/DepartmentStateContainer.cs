using Pusula.Training.HealthCare.Departments
    ;
namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class DepartmentStateContainer
    {
        //Department bilgilerinin blazor tarafında taşınması işlemi için gerekti
        public DepartmentDto SelectedDepartment { get; set; } = null!;
    }
}
