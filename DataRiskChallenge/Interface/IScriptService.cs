using DataRiskChallenge.Domain.Model;

namespace DataRiskChallenge.Interface;

public interface IScriptService
{
    Task SaveChangesByAsync(Script script);
    Task<string> GetContentByIdentifier(string identifier);
}