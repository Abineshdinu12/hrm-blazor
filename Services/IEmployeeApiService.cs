using IstreamBlazor.Models;

namespace IstreamBlazor.Services;

public interface IEmployeeApiService
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<EmployeeResponseDto?> CreateAsync(CreateEmployeeDto dto);
    Task<EmployeeResponseDto?> UpdateAsync(UpdateEmployeeDto dto);
    Task<bool> DeleteAsync(int id);
    Task<EmployeeDetailDto?> SaveEmployeeDetailsAsync(EmployeeDetailDto dto);
    Task<EmployeeSalaryDto?> GetSalaryAsync(int employeeId);
    Task<bool> SaveSalaryAsync(EmployeeSalaryDto dto);
    Task<EmployeePayrollDto?> GetPayrollAsync(int employeeId);
    Task<bool> SavePayrollAsync(EmployeePayrollDto dto);
    Task<bool> DeleteAllDataAsync(int id);
}
