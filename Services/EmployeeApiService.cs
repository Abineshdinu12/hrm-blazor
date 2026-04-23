using System.Net.Http.Json;
using IstreamBlazor.Models;

namespace IstreamBlazor.Services;

public class EmployeeApiService : IEmployeeApiService
{
    private readonly HttpClient _http;

    public EmployeeApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<Employee>>>("api/Employees/Getall");
            return response?.Data ?? new List<Employee>();
        }
        catch { return new List<Employee>(); }
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<Employee>>($"api/Employees/GetbyId/{id}");
            return response?.Data;
        }
        catch { return null; }
    }

    public async Task<EmployeeResponseDto?> CreateAsync(CreateEmployeeDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/Employees/CreateEMP", dto);
        if (!res.IsSuccessStatusCode) return null;
        var response = await res.Content.ReadFromJsonAsync<ApiResponse<EmployeeResponseDto>>();
        return response?.Data;
    }

    public async Task<EmployeeResponseDto?> UpdateAsync(UpdateEmployeeDto dto)
    {
        var res = await _http.PutAsJsonAsync("api/Employees/UpdateById", dto);
        if (!res.IsSuccessStatusCode) return null;
        var response = await res.Content.ReadFromJsonAsync<ApiResponse<EmployeeResponseDto>>();
        return response?.Data;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/Employees/DeleteBy/{id}");
        return res.IsSuccessStatusCode;
    }

    public async Task<EmployeeDetailDto?> SaveEmployeeDetailsAsync(EmployeeDetailDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/Employees/saveemployeedetails", dto);
        if (!res.IsSuccessStatusCode) return null;
        var response = await res.Content.ReadFromJsonAsync<ApiResponse<EmployeeDetailDto>>();
        return response?.Data;
    }

    public async Task<EmployeeSalaryDto?> GetSalaryAsync(int employeeId)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<EmployeeSalaryDto>>($"api/Employees/getemployeesalary/{employeeId}");
            return response?.Data;
        }
        catch { return null; }
    }

    public async Task<bool> SaveSalaryAsync(EmployeeSalaryDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/Employees/saveemployeesalary", dto);
        return res.IsSuccessStatusCode;
    }

    public async Task<EmployeePayrollDto?> GetPayrollAsync(int employeeId)
    {
        try
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<EmployeePayrollDto>>($"api/Employees/getemployeepayroll/{employeeId}");
            return response?.Data;
        }
        catch { return null; }
    }

    public async Task<bool> SavePayrollAsync(EmployeePayrollDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/Employees/saveemployeepayroll", dto);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAllDataAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/Employees/deleteemployeeData/{id}");
        return res.IsSuccessStatusCode;
    }
}
