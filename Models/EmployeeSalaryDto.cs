namespace IstreamBlazor.Models;

public class EmployeeSalaryDto
{
    public int EmployeeId { get; set; }
    public string? Grade { get; set; }
    public string? GradeLevel { get; set; }
    public string? SalaryType { get; set; }
    public decimal? BasicSalary { get; set; }
    public bool IsOvertimeEligible { get; set; }
    public bool UseFixedOvertimeRate { get; set; }
    public decimal? OvertimeRate { get; set; }
    public string? Supplier { get; set; }
    public decimal? Commission { get; set; }
    public List<AllowanceDto> Allowances { get; set; } = new();
    public List<object> Benefits { get; set; } = new();
}
