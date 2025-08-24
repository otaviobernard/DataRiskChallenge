using DataRiskChallenge.Domain.Model;

namespace DataRiskChallenge.Interface;

public interface IExecutionService
{
    Task SaveChangesByAsync(Execution execution);
    Task UpdateExecutionStatusAsync(Execution execution, int status);
    Task UpdateExecutionResultAsync(Execution execution);
}