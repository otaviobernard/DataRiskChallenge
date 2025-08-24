using System.Text.Json;
using DataRiskChallenge.Data;
using DataRiskChallenge.Domain.Model;
using Jint.Native;
using Jint.Native.Json;
using Newtonsoft.Json;

namespace DataRiskChallenge.Services;
using Jint;

public class ScriptExecutorService(Engine engine, ExecutionService executionService)
{
    public async Task ExecuteScript(string script, JsonElement content, Execution newExecution)
    {
        engine.Execute(script);
        var result = engine.Invoke("process", ParseJson(content));
        await executionService.UpdateExecutionStatusAsync(newExecution, 1);
        newExecution.Result = JsonConvert.SerializeObject(result.ToObject());
        ProcessResult(newExecution);
    }

    private JsValue ParseJson(JsonElement json)
    {
        var parser = new JsonParser(engine);
        return parser.Parse(json.GetRawText());
    }
    
    private async Task ProcessResult(Execution newExecution)
    {
        if(newExecution.Result is "null" || string.IsNullOrEmpty(newExecution.Result))
        {
            newExecution.Status = 2;
            newExecution.Result = "undefined";
        }
        else
        {
            newExecution.Status = 3;
        }
        newExecution.CompletedAt = DateTime.UtcNow;
        await executionService.UpdateExecutionResultAsync(newExecution);
    }
}