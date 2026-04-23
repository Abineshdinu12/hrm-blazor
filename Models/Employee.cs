namespace IstreamBlazor.Models;

public class Employee
{
    public int Id { get; set; }
    public string EmpName { get; set; } = string.Empty;
    public string AliasName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public List<ContactDto> Contacts { get; set; } = new();
    public List<QualificationDto> Qualifications { get; set; } = new();
    public EmployeeDetailDto? EmployeeDetail { get; set; }
    public EmployeeSalaryDto? EmployeeSalary { get; set; }
    public EmployeePayrollDto? EmployeePayrollDetail { get; set; }
}

public class CreateEmployeeDto
{
    public string EmpName { get; set; } = string.Empty;
    public string AliasName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public List<ContactDto> Contacts { get; set; } = new();
    public List<QualificationDto> Qualifications { get; set; } = new();
}

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string EmpName { get; set; } = string.Empty;
    public string AliasName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public List<ContactDto> Contacts { get; set; } = new();
    public List<QualificationDto> Qualifications { get; set; } = new();
}

public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string EmpName { get; set; } = string.Empty;
    public string AliasName { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Religion { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public List<ContactDto> Contacts { get; set; } = new();
    public List<QualificationDto> Qualifications { get; set; } = new();
}
