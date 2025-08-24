namespace DataRiskChallenge.Domain.Model;

public static class Constants
{
    public static string IsNull = "null";

    public enum ExecutionStatus
    {
        Pending = 0,
        Running = 1,
        Completed = 2,
        Failed = 3
    }
}