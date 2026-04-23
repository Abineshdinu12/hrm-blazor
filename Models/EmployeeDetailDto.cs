namespace IstreamBlazor.Models;

public class EmployeeDetailDto
{
    public int EmployeeId { get; set; }
    public DateTime? DateOfJoin { get; set; }
    public DateTime? ContractStartDate { get; set; }
    public DateTime? DateOfLeaving { get; set; }
    public string? VisaCategory { get; set; }
    public int? NoticePeriodDays { get; set; }
    // Organisation
    public string? Company { get; set; }
    public string? Branch { get; set; }
    public string? Division { get; set; }
    public string? Department { get; set; }
    public string? Section { get; set; }
    public string? WorkCategory { get; set; }
    // CPR
    public string? CprNo { get; set; }
    public DateTime? CprExpiry { get; set; }
    public string? DesignationAsPerId { get; set; }
    // Passport
    public string? Nationality { get; set; }
    public string? PassportNo { get; set; }
    public string? PassportName { get; set; }
    public DateTime? PassportExpiry { get; set; }
    public string? IssuedPlace { get; set; }
    // RP
    public string? RpSponsor { get; set; }
    public string? RpNo { get; set; }
    public DateTime? RpExpiry { get; set; }
    // GOSI
    public string? GosiNo { get; set; }
    public decimal? GosiSalary { get; set; }
    // Vacation
    public DateTime? LastVacationDate { get; set; }
    public DateTime? VacationRejoinedDate { get; set; }
}
