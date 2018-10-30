using pruebaNetOracle.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace pruebaNetOracle.Controllers
{
    [Produces("application/json")]
    public class EmployeeController: Controller
    {
        IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository _employeeRepository){
            employeeRepository = _employeeRepository;
        }

        [Route("api/GetEmployeeList")]
        public ActionResult GetEmployeeList()
        {
              var result = employeeRepository.GetEmployeeList();
              if(result== null) {
                  return NotFound();
              }
              return Ok(result);
        }

         [Route("api/GetEmployeeDetails/{empId}")]
         public ActionResult GetEmployeeDetails(int empId)
         {
             var result = employeeRepository.GetEmployeeDetails(empId);
             if(result==null) {
                    return NotFound();
             } 
             return Ok(result);
         }

         [Route("api/GetEmpleados/{name}")]
         public ActionResult GetEmpleados(string name)
         {
             var result = employeeRepository.GetEmpleados(name);
             if(result==null) {
                 return NotFound();
             }
             return Ok(result);
         }
    }
}