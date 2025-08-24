using System.Text.Json;
using DataRiskChallenge.Data;
using DataRiskChallenge.Domain.Model;
using DataRiskChallenge.Services;
using Jint.Native;
using Microsoft.AspNetCore.Mvc;

namespace DataRiskChallenge.Controller;

[ApiController]
[Route("api/executions")]
public class ExecutionsController(ScriptExecutorService execution, ScriptService scriptsService, ExecutionService executionService)
    : ControllerBase
{
    [HttpPost]
    [Route("new-execution")]
    public async Task<IActionResult> CreateNewExecution([FromHeader] string identifier, [FromBody] JsonElement payload)
    {
        var script = await scriptsService.GetScriptByIdentifier(identifier);
        if (script.Content == Constants.IsNull || script.Content == null)
        {
            return BadRequest("Script não encontrado");
        }
        var newExecution = new Execution(script.Identifier, script.Id);
        await executionService.SaveChangesByAsync(newExecution);
        execution.ExecuteScript(script.Content, payload, newExecution);
        return Ok("O script entrou pra fila de execucao");
    }
}