using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viking.Interfaces;
using Viking.Models.Sports;
using Viking.Repositories;

namespace Viking.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class SetController : Controller
    {
        private readonly ISet _rSet;

        public SetController(ISet rSet)
        {
            _rSet = rSet;
        }

        [HttpPost("CreateNewSet")]
        public async Task<IActionResult> CreateNewSet([FromBody] Set Set)
        {
            try
            {
                var newSet = _rSet.CreateNewSet(Set);
                await _rSet.AddNewSet(newSet);
                return Json(newSet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("DeleteSet")]
        public async Task<IActionResult> DeleteSet([FromBody] Set Set)
        {
            try
            {
                await _rSet.DelSet(Set);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("DelSets")]
        public async Task<IActionResult> DelSets([FromBody] List<Set> Sets)
        {
            try
            {
                await _rSet.DelSets(Sets);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("UpdateSet")]
        public async Task<IActionResult> UpdateSet([FromBody] Set Set)
        {
            try
            {
                var updSet = await _rSet.UpdateSet(Set);
                return Json(updSet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("GetAllSet/{id}")]
        public async Task<IActionResult> GetAllSet(Guid id)
        {
            return Json(await _rSet.GetSetsByExerciseId(id));
        }
    }
}
