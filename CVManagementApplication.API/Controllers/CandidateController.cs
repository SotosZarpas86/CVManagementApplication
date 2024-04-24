using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CVManagementApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController(ICandidateService candidateService) : Controller
    {
        private readonly ICandidateService _candidateService = candidateService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateModel>>> GetAll()
        {
            var result = await _candidateService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CandidateModel>> Create([FromBody] CandidateCreateModel model)
        {
            var result = await _candidateService.Create(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<CandidateModel>> Update(int Id, [FromBody] CandidateModel model)
        {
            if (Id != model.Id)
                return BadRequest();

            var result = await _candidateService.Edit(Id, model);

            if (result == null)
                return NotFound("Candidate not found");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int candidateId)
        {
            var response = await _candidateService.Delete(candidateId);
            if (!response)
                return NotFound("Candidate not found");

            return Ok();
        }
    }
}
