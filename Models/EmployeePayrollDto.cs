namespace IstreamBlazor.Models;

public class EmployeePayrollDto
{
    public int EmployeeId { get; set; }
    public bool PayrollGenerateAllowed { get; set; }
    public bool PayslipPrintingAllowed { get; set; }
    public bool AccrualsGenerateAllowed { get; set; }
    public bool ManageAttendance { get; set; }
    public bool CanCheckAnytime { get; set; }
    public bool IgnoreBreakDuration { get; set; }
    public bool IgnoreLatAttendanceDeduction { get; set; }
    public string? WorkShift { get; set; }
    public string? ModeOfPayment { get; set; }
    public string? BankACNo { get; set; }
    public string? Bank { get; set; }
    public string? SalaryCurrency { get; set; }
    public decimal? ExchangeRate { get; set; }
    public bool WeekendsAllowed { get; set; }
    public bool HolidaysAllowed { get; set; }
    public string? DestinationCountry { get; set; }
    public string? Airport { get; set; }
    public decimal? EstimatedAirfare { get; set; }
    public int? AirfareFrequencyMonths { get; set; }
    public string? DefaultOperationName { get; set; }
    public string? DefaultCostHead { get; set; }
    public string? FileRef { get; set; }
    public string? LabourCamp { get; set; }
    public bool IsAccommodated { get; set; }
    public string? ManpowerCategory { get; set; }
    public string? PayrollCategory { get; set; }
    public string? AccrualsCategory { get; set; }
    public string? OtherCategory { get; set; }
}
