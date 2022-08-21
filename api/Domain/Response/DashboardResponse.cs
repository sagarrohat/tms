namespace Domain;

public class DashboardResponse
{
    public int LowCompletedCount { get; set; }
    public int LowCancelledCount { get; set; }
    public int LowOverDueCount { get; set; }
    public int NormalCompletedCount { get; set; }
    public int NormalCancelledCount { get; set; }
    public int NormalOverDueCount { get; set; }
    public int HighCompletedCount { get; set; }
    public int HighCancelledCount { get; set; }
    public int HighOverDueCount { get; set; }
    public int CompletedCount { get; set; }
    public int CancelledCount { get; set; }
    public int OverDueCount { get; set; }
}

