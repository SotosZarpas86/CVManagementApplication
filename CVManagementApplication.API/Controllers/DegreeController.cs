using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CVManagementApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DegreeController(IDegreeService degreeService) : Controller
    {
        private readonly IDegreeService _degreeService = degreeService;

        [HttpGet]
        public async Task<ActionResult<IList<DegreeModel>>> GetAll()
        {
            return Ok(await _degreeService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<DegreeModel>> Create([FromBody] DegreeCreateModel model)
        {
            var result = await _degreeService.Create(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<DegreeModel>> Update(int Id, [FromBody] DegreeModel model)
        {
            if (Id != model.Id)
                return BadRequest();

            var result = await _degreeService.Update(Id, model);

            if (result == null)
                return NotFound("Degree not found");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int degreeId)
        {
            var response = await _degreeService.Delete(degreeId);
            if (!response)
                return NotFound("Degree not found");

            return Ok();
        }
    }
}
