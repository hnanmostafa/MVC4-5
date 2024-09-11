using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        /*
         * Ger, Get All, Create, Update, Delete
         * */

        // Dependency Injection
        // Method Injection => Method([FromServices]DataContext dataContext)
        // Property Injection => 
        // [FromServices]
        // Public DataContext dataContext {get; set;}
        

        private readonly DataContext _dataContext;
        // CTOR Injection

        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        //private DataContext dataContext = new DataContext(); //Hard Codded Dependency
        public Department? Get(int id) => _dataContext.Departments.Find(id);
        public IEnumerable<Department> GetAll() => _dataContext.Departments.ToList();

        public int Create(Department entity)
        {
            _dataContext.Departments.Add(entity);
            return _dataContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dataContext.Departments.Update(entity);
            return _dataContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dataContext.Departments.Remove(entity);
            return _dataContext.SaveChanges();
        }

        //return _dataContext.Departments.Local.FirstOrDefault(department => department.Id == id);

    }
}
