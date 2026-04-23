using IstreamBlazor.Models;

namespace IstreamBlazor.Services;

public class AppState
{
    // ── Auth ──────────────────────────────────────────────────────────────────
    public bool IsAuthenticated { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string UserEmail { get; private set; } = string.Empty;

    public void Login(string name, string email)
    {
        IsAuthenticated = true;
        UserName = name;
        UserEmail = email;
        NotifyStateChanged();
    }

    public void Logout()
    {
        IsAuthenticated = false;
        UserName = string.Empty;
        UserEmail = string.Empty;
        NotifyStateChanged();
    }

    // ── Sidebar ───────────────────────────────────────────────────────────────
    public bool SidebarOpen { get; private set; } = true;

    public void ToggleSidebar()
    {
        SidebarOpen = !SidebarOpen;
        NotifyStateChanged();
    }

    public void SetSidebar(bool open)
    {
        SidebarOpen = open;
        NotifyStateChanged();
    }

    // ── Employees ─────────────────────────────────────────────────────────────
    public List<Employee> Employees { get; private set; } = new();
    public bool Loading { get; private set; }
    public string? Error { get; private set; }

    public void SetEmployees(List<Employee> employees)
    {
        Employees = employees;
        NotifyStateChanged();
    }

    public void SetLoading(bool loading)
    {
        Loading = loading;
        NotifyStateChanged();
    }

    public void SetError(string? error)
    {
        Error = error;
        NotifyStateChanged();
    }

    public void DismissError()
    {
        Error = null;
        NotifyStateChanged();
    }

    public void RemoveEmployee(int id)
    {
        Employees = Employees.Where(e => e.Id != id).ToList();
        NotifyStateChanged();
    }

    // ── Change notification ───────────────────────────────────────────────────
    public event Action? OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();
}
