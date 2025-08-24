using DataRiskChallenge.Data;
using DataRiskChallenge.Domain.Model;
using DataRiskChallenge.Interface;

namespace DataRiskChallenge.Services;

public class ExecutionService(AppDbContext context) : IExecutionService
{
    public async Task SaveChangesByAsync(Execution execution)
    {
        context.Executions.Add(execution);
        await context.SaveChangesAsync();
    }

    public async Task UpdateExecutionStatusAsync(Execution execution, int status)
    {
        execution.Status = status;
        context.Executions.Update(execution);
    }
    
    public async Task UpdateExecutionResultAsync(Execution execution)
    {
        context.Executions.Update(execution);
        await context.SaveChangesAsync();
    }
}