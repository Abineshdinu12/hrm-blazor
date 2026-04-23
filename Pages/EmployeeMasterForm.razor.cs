using IstreamBlazor.Models;
using IstreamBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace IstreamBlazor.Pages;

public partial class EmployeeMasterForm : ComponentBase
{
    [Inject] IEmployeeApiService Api { get; set; } = default!;
    [Inject] AppState AppState { get; set; } = default!;
    [Inject] NavigationManager Nav { get; set; } = default!;

    [SupplyParameterFromQuery] public string Mode { get; set; } = "create";
    [SupplyParameterFromQuery] public int? Id { get; set; }

    // ── UI State ──────────────────────────────────────────────────────────────
    private int _activeTab = 0;
    private bool _loading;
    private string _errorMsg = "";
    private string _successMsg = "";
    private int? _resolvedId;
    private bool _isView => Mode == "view";
    private bool _isModify => Mode == "modify";

    private string ModeLabel => Mode switch { "view" => "View Employee", "modify" => "Modify Employee", _ => "Create Employee" };
    private string ModeColor => Mode switch { "view" => "#6C63FF", "modify" => "#f59e0b", _ => "#22c55e" };

    // ── Tab 1: Personal ───────────────────────────────────────────────────────
    private string _empName = "", _aliasName = "", _designation = "", _gender = "", _dob = "", _religion = "", _maritalStatus = "";
    private List<ContactDto> _contacts = new() { new() };
    private List<QualificationDto> _qualifications = new() { new() };

    // ── Tab 2: Employee Details ───────────────────────────────────────────────
    private string _dateOfJoin = "", _contractStart = "", _dateOfLeaving = "", _visaCategory = "";
    private string _noticePeriod = "", _company = "", _branch = "", _division = "", _department = "", _section = "", _workCategory = "";
    private string _cprNo = "", _cprExpiry = "", _designationAsPerId = "";
    private string _nationality = "", _passportNo = "", _passportName = "", _passportExpiry = "", _issuedPlace = "";
    private string _rpSponsor = "", _rpNo = "", _rpExpiry = "";
    private string _gosiNo = "", _gosiSalary = "", _lastVacationDate = "", _vacationRejoinedDate = "";

    // ── Tab 3: Salary ─────────────────────────────────────────────────────────
    private string _grade = "", _gradeLevel = "", _salaryType = "", _basicSalary = "", _supplier = "", _commission = "";
    private bool _isOtEligible, _useFixedOt;
    private string _otRate = "";
    private List<AllowanceDto> _allowances = new() { new() { AllowanceName = "Housing" }, new() { AllowanceName = "Transport" } };
    private decimal GrossSalary => (decimal.TryParse(_basicSalary, out var b) ? b : 0) + _allowances.Sum(a => a.Value);

    // ── Tab 4: Payroll ────────────────────────────────────────────────────────
    private bool _payrollAllowed, _payslipAllowed, _accrualsAllowed, _manageAttendance, _canCheckAnytime;
    private bool _ignoreBreak, _ignoreLate, _weekends, _holidays, _isAccommodated;
    private string _workShift = "", _modeOfPayment = "", _bankACNo = "", _bank = "", _salaryCurrency = "";
    private string _exchangeRate = "", _destCountry = "", _airport = "", _estAirfare = "", _airfareFreq = "";
    private string _defaultOp = "", _defaultCost = "", _fileRef = "", _labourCamp = "";
    private string _manpowerCat = "", _payrollCat = "", _accrualsCat = "", _otherCat = "";
    private bool ShowBankFields => _modeOfPayment is "Bank Transfer" or "WPS";

    // ─────────────────────────────────────────────────────────────────────────
    protected override async Task OnInitializedAsync()
    {
        if (Id.HasValue && (Mode == "modify" || Mode == "view"))
        {
            _resolvedId = Id.Value;
            await LoadEmployeeById(Id.Value);
        }
    }

    private async Task LoadEmployeeById(int id)
    {
        _loading = true;
        var emp = await Api.GetByIdAsync(id);
        _loading = false;
        if (emp == null) return;

        _empName = emp.EmpName; _aliasName = emp.AliasName; _designation = emp.Designation;
        _gender = emp.Gender; _religion = emp.Religion; _maritalStatus = emp.MaritalStatus;
        _dob = emp.DateOfBirth != default ? emp.DateOfBirth.ToString("yyyy-MM-dd") : "";
        if (emp.Contacts?.Any() == true) _contacts = emp.Contacts.ToList();
        if (emp.Qualifications?.Any() == true) _qualifications = emp.Qualifications.ToList();

        // Employee details
        var d = emp.EmployeeDetail;
        if (d != null)
        {
            _dateOfJoin = d.DateOfJoin?.ToString("yyyy-MM-dd") ?? "";
            _contractStart = d.ContractStartDate?.ToString("yyyy-MM-dd") ?? "";
            _dateOfLeaving = d.DateOfLeaving?.ToString("yyyy-MM-dd") ?? "";
            _visaCategory = d.VisaCategory ?? ""; _noticePeriod = d.NoticePeriodDays?.ToString() ?? "";
            _company = d.Company ?? ""; _branch = d.Branch ?? ""; _division = d.Division ?? "";
            _department = d.Department ?? ""; _section = d.Section ?? ""; _workCategory = d.WorkCategory ?? "";
            _cprNo = d.CprNo ?? ""; _cprExpiry = d.CprExpiry?.ToString("yyyy-MM-dd") ?? "";
            _designationAsPerId = d.DesignationAsPerId ?? ""; _nationality = d.Nationality ?? "";
            _passportNo = d.PassportNo ?? ""; _passportName = d.PassportName ?? "";
            _passportExpiry = d.PassportExpiry?.ToString("yyyy-MM-dd") ?? ""; _issuedPlace = d.IssuedPlace ?? "";
            _rpSponsor = d.RpSponsor ?? ""; _rpNo = d.RpNo ?? ""; _rpExpiry = d.RpExpiry?.ToString("yyyy-MM-dd") ?? "";
            _gosiNo = d.GosiNo ?? ""; _gosiSalary = d.GosiSalary?.ToString() ?? "";
            _lastVacationDate = d.LastVacationDate?.ToString("yyyy-MM-dd") ?? "";
            _vacationRejoinedDate = d.VacationRejoinedDate?.ToString("yyyy-MM-dd") ?? "";
        }
    }

    private async Task LoadSalaryData()
    {
        if (!_resolvedId.HasValue) return;
        var s = await Api.GetSalaryAsync(_resolvedId.Value);
        if (s == null) return;
        _grade = s.Grade ?? ""; _gradeLevel = s.GradeLevel ?? ""; _salaryType = s.SalaryType ?? "";
        _basicSalary = s.BasicSalary?.ToString() ?? ""; _isOtEligible = s.IsOvertimeEligible;
        _useFixedOt = s.UseFixedOvertimeRate; _otRate = s.OvertimeRate?.ToString() ?? "";
        _supplier = s.Supplier ?? ""; _commission = s.Commission?.ToString() ?? "";
        if (s.Allowances?.Any() == true) _allowances = s.Allowances.ToList();
    }

    private async Task LoadPayrollData()
    {
        if (!_resolvedId.HasValue) return;
        var p = await Api.GetPayrollAsync(_resolvedId.Value);
        if (p == null) return;
        _payrollAllowed = p.PayrollGenerateAllowed; _payslipAllowed = p.PayslipPrintingAllowed;
        _accrualsAllowed = p.AccrualsGenerateAllowed; _manageAttendance = p.ManageAttendance;
        _canCheckAnytime = p.CanCheckAnytime; _ignoreBreak = p.IgnoreBreakDuration;
        _ignoreLate = p.IgnoreLatAttendanceDeduction; _workShift = p.WorkShift ?? "";
        _modeOfPayment = p.ModeOfPayment ?? ""; _bankACNo = p.BankACNo ?? ""; _bank = p.Bank ?? "";
        _salaryCurrency = p.SalaryCurrency ?? ""; _exchangeRate = p.ExchangeRate?.ToString() ?? "";
        _weekends = p.WeekendsAllowed; _holidays = p.HolidaysAllowed;
        _destCountry = p.DestinationCountry ?? ""; _airport = p.Airport ?? "";
        _estAirfare = p.EstimatedAirfare?.ToString() ?? ""; _airfareFreq = p.AirfareFrequencyMonths?.ToString() ?? "";
        _defaultOp = p.DefaultOperationName ?? ""; _defaultCost = p.DefaultCostHead ?? "";
        _fileRef = p.FileRef ?? ""; _labourCamp = p.LabourCamp ?? ""; _isAccommodated = p.IsAccommodated;
        _manpowerCat = p.ManpowerCategory ?? ""; _payrollCat = p.PayrollCategory ?? "";
        _accrualsCat = p.AccrualsCategory ?? ""; _otherCat = p.OtherCategory ?? "";
    }

    // ── Tab activation ────────────────────────────────────────────────────────
    private async Task SetTab(int tab)
    {
        if (tab == 2 && _resolvedId.HasValue) await LoadSalaryData();
        if (tab == 3 && _resolvedId.HasValue) await LoadPayrollData();
        _activeTab = tab;
    }

    // ── Save handlers ─────────────────────────────────────────────────────────
    private async Task<bool> SaveTab1()
    {
        if (string.IsNullOrWhiteSpace(_empName)) { _errorMsg = "Employee Name is required."; return false; }
        _loading = true; _errorMsg = "";
        try
        {
            if (_isModify && _resolvedId.HasValue)
            {
                var dto = new UpdateEmployeeDto { Id = _resolvedId.Value, EmpName = _empName, AliasName = _aliasName, Designation = _designation, Gender = _gender, DateOfBirth = ParseDate(_dob), Religion = _religion, MaritalStatus = _maritalStatus, Contacts = _contacts, Qualifications = _qualifications };
                var r = await Api.UpdateAsync(dto);
                if (r == null) { _errorMsg = "Update failed."; return false; }
                _resolvedId = r.Id;
            }
            else
            {
                var dto = new CreateEmployeeDto { EmpName = _empName, AliasName = _aliasName, Designation = _designation, Gender = _gender, DateOfBirth = ParseDate(_dob), Religion = _religion, MaritalStatus = _maritalStatus, Contacts = _contacts, Qualifications = _qualifications };
                var r = await Api.CreateAsync(dto);
                if (r == null) { _errorMsg = "Create failed."; return false; }
                _resolvedId = r.Id;
            }
            _successMsg = "Personal details saved!";
            return true;
        }
        catch (Exception ex) { _errorMsg = ex.Message; return false; }
        finally { _loading = false; }
    }

    private async Task<bool> SaveTab2()
    {
        if (!_resolvedId.HasValue) { _errorMsg = "Save personal details first."; return false; }
        _loading = true;
        try
        {
            var dto = new EmployeeDetailDto { EmployeeId = _resolvedId.Value, DateOfJoin = ParseDateNull(_dateOfJoin), ContractStartDate = ParseDateNull(_contractStart), DateOfLeaving = ParseDateNull(_dateOfLeaving), VisaCategory = _visaCategory, NoticePeriodDays = int.TryParse(_noticePeriod, out var np) ? np : null, Company = _company, Branch = _branch, Division = _division, Department = _department, Section = _section, WorkCategory = _workCategory, CprNo = _cprNo, CprExpiry = ParseDateNull(_cprExpiry), DesignationAsPerId = _designationAsPerId, Nationality = _nationality, PassportNo = _passportNo, PassportName = _passportName, PassportExpiry = ParseDateNull(_passportExpiry), IssuedPlace = _issuedPlace, RpSponsor = _rpSponsor, RpNo = _rpNo, RpExpiry = ParseDateNull(_rpExpiry), GosiNo = _gosiNo, GosiSalary = decimal.TryParse(_gosiSalary, out var gs) ? gs : null, LastVacationDate = ParseDateNull(_lastVacationDate), VacationRejoinedDate = ParseDateNull(_vacationRejoinedDate) };
            var r = await Api.SaveEmployeeDetailsAsync(dto);
            if (r == null) { _errorMsg = "Save failed."; return false; }
            _successMsg = "Employee details saved!"; return true;
        }
        catch (Exception ex) { _errorMsg = ex.Message; return false; }
        finally { _loading = false; }
    }

    private async Task<bool> SaveTab3()
    {
        if (!_resolvedId.HasValue) { _errorMsg = "Save personal details first."; return false; }
        _loading = true;
        try
        {
            var dto = new EmployeeSalaryDto { EmployeeId = _resolvedId.Value, Grade = _grade, GradeLevel = _gradeLevel, SalaryType = _salaryType, BasicSalary = decimal.TryParse(_basicSalary, out var bs) ? bs : null, IsOvertimeEligible = _isOtEligible, UseFixedOvertimeRate = _useFixedOt, OvertimeRate = decimal.TryParse(_otRate, out var ot) ? ot : null, Supplier = _supplier, Commission = decimal.TryParse(_commission, out var cm) ? cm : null, Allowances = _allowances.Where(a => !string.IsNullOrEmpty(a.AllowanceName)).ToList() };
            var ok = await Api.SaveSalaryAsync(dto);
            if (!ok) { _errorMsg = "Save failed."; return false; }
            _successMsg = "Salary details saved!"; return true;
        }
        catch (Exception ex) { _errorMsg = ex.Message; return false; }
        finally { _loading = false; }
    }

    private async Task<bool> SaveTab4()
    {
        if (!_resolvedId.HasValue) { _errorMsg = "Save personal details first."; return false; }
        _loading = true;
        try
        {
            var dto = new EmployeePayrollDto { EmployeeId = _resolvedId.Value, PayrollGenerateAllowed = _payrollAllowed, PayslipPrintingAllowed = _payslipAllowed, AccrualsGenerateAllowed = _accrualsAllowed, ManageAttendance = _manageAttendance, CanCheckAnytime = _canCheckAnytime, IgnoreBreakDuration = _ignoreBreak, IgnoreLatAttendanceDeduction = _ignoreLate, WorkShift = _workShift, ModeOfPayment = _modeOfPayment, BankACNo = _bankACNo, Bank = _bank, SalaryCurrency = _salaryCurrency, ExchangeRate = decimal.TryParse(_exchangeRate, out var er) ? er : null, WeekendsAllowed = _weekends, HolidaysAllowed = _holidays, DestinationCountry = _destCountry, Airport = _airport, EstimatedAirfare = decimal.TryParse(_estAirfare, out var ea) ? ea : null, AirfareFrequencyMonths = int.TryParse(_airfareFreq, out var af) ? af : null, DefaultOperationName = _defaultOp, DefaultCostHead = _defaultCost, FileRef = _fileRef, LabourCamp = _labourCamp, IsAccommodated = _isAccommodated, ManpowerCategory = _manpowerCat, PayrollCategory = _payrollCat, AccrualsCategory = _accrualsCat, OtherCategory = _otherCat };
            var ok = await Api.SavePayrollAsync(dto);
            if (!ok) { _errorMsg = "Save failed."; return false; }
            _successMsg = "Payroll saved!"; return true;
        }
        catch (Exception ex) { _errorMsg = ex.Message; return false; }
        finally { _loading = false; }
    }

    private async Task HandleSaveAndNext()
    {
        if (_isView) { _activeTab++; return; }
        bool ok = _activeTab switch { 0 => await SaveTab1(), 1 => await SaveTab2(), 2 => await SaveTab3(), _ => false };
        if (ok) await SetTab(_activeTab + 1);
    }

    private async Task HandleFinalSave()
    {
        if (_isView) { Nav.NavigateTo("/employee-master"); return; }
        var ok = await SaveTab4();
        if (ok) { await Task.Delay(1200); Nav.NavigateTo("/employee-master"); }
    }

    private void AddContact() => _contacts.Add(new ContactDto());
    private void RemoveContact(int i) { if (_contacts.Count > 1) _contacts.RemoveAt(i); }
    private void AddQualification() => _qualifications.Add(new QualificationDto());
    private void RemoveQualification(int i) { if (_qualifications.Count > 1) _qualifications.RemoveAt(i); }
    private void AddAllowance() => _allowances.Add(new AllowanceDto());
    private void RemoveAllowance(int i) { if (_allowances.Count > 1) _allowances.RemoveAt(i); }

    private void GoBack() => Nav.NavigateTo("/employee-master");

    private static DateTime ParseDate(string s) => DateTime.TryParse(s, out var d) ? d : DateTime.Today;
    private static DateTime? ParseDateNull(string s) => DateTime.TryParse(s, out var d) ? d : null;

    private IEnumerable<(string Label, Func<bool> Getter, Action<bool> Setter)> PayrollFlags() =>
        new (string, Func<bool>, Action<bool>)[]
        {
            ("Payroll Generate Allowed",    () => _payrollAllowed,    v => _payrollAllowed    = v),
            ("Payslip Printing Allowed",    () => _payslipAllowed,    v => _payslipAllowed    = v),
            ("Accruals Generate Allowed",   () => _accrualsAllowed,   v => _accrualsAllowed   = v),
            ("Manage Attendance",           () => _manageAttendance,  v => _manageAttendance  = v),
            ("Can Check Anytime",           () => _canCheckAnytime,   v => _canCheckAnytime   = v),
            ("Ignore Break Duration",       () => _ignoreBreak,       v => _ignoreBreak       = v),
            ("Ignore Late Deduction",       () => _ignoreLate,        v => _ignoreLate        = v),
            ("Weekends Allowed",            () => _weekends,          v => _weekends          = v),
            ("Holidays Allowed",            () => _holidays,          v => _holidays          = v),
        };
}
