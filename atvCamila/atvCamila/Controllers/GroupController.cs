using atvCamila.Interfaces;
using atvCamila.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace atvCamila.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ISupabaseClient _supabaseClient;

        public GroupsController(ISupabaseClient supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var result = await _supabaseClient.GetAsync("groups");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/groups
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] Groups group)
        {
            if (group == null)
            {
                return BadRequest("Group data is required.");
            }

            try
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(group);
                var result = await _supabaseClient.PostAsync("groups", jsonPayload);
                return CreatedAtAction(nameof(GetAllGroups),"", result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] Groups group)
        {
            if (group == null)
            {
                return BadRequest("Group data is required.");
            }

            try
            {
                // Atribuir o valor do parâmetro id ao campo id do modelo
               // group.id = id;

                // Serializar o grupo sem incluir o campo `id`
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(
                    group,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
                    }
                );

                // Realizar a requisição de atualização
                var result = await _supabaseClient.PatchAsync($"groups", jsonPayload,id);

                if (result == null)
                {
                    return NotFound("Group not found.");
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
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                var result = await _supabaseClient.DeleteAsync("groups",id);
                if (result == null)
                {
                    return NotFound("Group not found.");
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
