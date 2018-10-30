namespace pruebaNetOracle.Repositories
{
    public interface IEmployeeRepository
    {
        object GetEmployeeList();
        object GetEmployeeDetails(int empId);
        object GetEmpleados(string name);
    }
}