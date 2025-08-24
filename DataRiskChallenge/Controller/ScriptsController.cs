using DataRiskChallenge.Data;
using DataRiskChallenge.Domain.Model;
using DataRiskChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataRiskChallenge.Controller;

[ApiController]
[Route("api/scripts")]
public class ScriptsController(AppDbContext context, ScriptService scriptService) : ControllerBase
{
    [HttpPost]
    [Route("new-script")]
    public async Task<IActionResult> CreateNewScript([FromHeader] string identifier, [FromHeader] string content)
    {
        Script script = new Script(identifier, content);
        try
        {
            var existingScript = context.Scripts.FirstOrDefault(s => s.Identifier == script.Identifier);
            if (existingScript != null)
            {
                return Conflict($"A script with identifier '{script.Identifier}' already exists.");
            }

            if(!scriptService.ValidateScript(script.Content))
            {
                return BadRequest("Invalid script content. The script must define a 'process' function.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while checking for existing scripts: {ex.Message}");
        }
        await scriptService.SaveChangesByAsync(script);
        return Ok(script);
    }

    [HttpGet]
    [Route("content/{identifier}")]
    public async Task<string> GetContentByIdentifier(string identifier)
    {
        var content = await scriptService.GetContentByIdentifier(identifier);
        return content;
    }
    
    [HttpGet]
    [Route("script/{identifier}")]
    public async Task<IActionResult> GetScriptByIdentifier(string identifier)
    {
        var script = await context.Scripts.FirstOrDefaultAsync(s => s.Identifier == identifier);
        if (script == null)
        {
            return NotFound($"Script with identifier '{identifier}' not found.");
        }
        return Ok(script);
    }

    [HttpGet]
    [Route("all-scripts")]
    public async Task<IActionResult> GetAllScripts()
    {
        var scripts = await context.Scripts.ToListAsync();
        return Ok(scripts);
    }
}