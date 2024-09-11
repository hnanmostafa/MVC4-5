using Demo.DataAccessLayer.Models;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IDepartmentRepository
    {
        int Create(Department entity);
        int Delete(Department entity);
        Department? Get(int id);
        IEnumerable<Department> GetAll();
        int Update(Department entity);
    }
}