using atvCamila.Interfaces;
using atvCamila.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace atvCamila.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemsController : ControllerBase
    {
        private readonly ISupabaseClient _supabaseClient;

        public SystemsController(ISupabaseClient supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _supabaseClient.GetAsync("systems");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/groups
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Systems systems)
        {
            if (systems == null)
            {
                return BadRequest("Permissions data is required.");
            }

            try
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(systems);
                var result = await _supabaseClient.PostAsync("systems", jsonPayload);
                return CreatedAtAction(nameof(GetAll), "", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Systems systems)
        {
            if (systems == null)
            {
                return BadRequest("É necssário informar a permissão.");
            }

            try
            {
                // Atribuir o valor do parâmetro id ao campo id do modelo
                // group.id = id;

                // Serializar o grupo sem incluir o campo `id`
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(
                    systems,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
                    }
                );

                // Realizar a requisição de atualização
                var result = await _supabaseClient.PatchAsync($"systems", jsonPayload, id);

                if (result == null)
                {
                    return NotFound("Permissão não encontrada.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/groups/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _supabaseClient.DeleteAsync("systems", id);
                if (result == null)
                {
                    return NotFound("Permissão não encontrada.");
                }
                return NoContent(); // 204 status code for successful deletion with no content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
