using DataRiskChallenge.Data;
using DataRiskChallenge.Domain.Model;
using DataRiskChallenge.Interface;
using Jint;
using Microsoft.EntityFrameworkCore;

namespace DataRiskChallenge.Services;

public class ScriptService(AppDbContext context, Engine engine) : IScriptService
{
    public async Task SaveChangesByAsync(Script script)
    {
        context.Scripts.Add(script);
        await context.SaveChangesAsync();
    }

    public async Task<string> GetContentByIdentifier(string identifier)
    {
        var script = await context.Scripts.FirstOrDefaultAsync(s => s.Identifier == identifier);
        return script?.Content;
    }

    public async Task<Script?> GetScriptByIdentifier(string identifier)
    {
        var script = await context.Scripts.FirstOrDefaultAsync(s => s.Identifier == identifier);
        return script;
    }
    
    public bool ValidateScript(string script)
    {
        engine.Execute(script);
        return !engine.GetValue("process").IsUndefined();
    }
}
