using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Viking.Interfaces;
using Viking.Models.Sports;
using Viking.Repositories;

namespace Viking.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ExerciseController : Controller
{
    private readonly IExercise _rExercise;

    public ExerciseController(IExercise rExercise)
    {
        _rExercise = rExercise;
    }
    
    [HttpPost("CreateNewExercise")]
    public async Task<IActionResult> CreateNewExercise([FromBody] Exercise exercise)
    {
        try
        {
            var newExercise = _rExercise.CreateExercise(exercise);
            await _rExercise.AddNewExercise(newExercise);
            return Json(newExercise);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    [HttpDelete("DeleteExercise/{id}")]
    public async Task<IActionResult> DeleteExercise(Exercise exercise)
    {
        try
        {
            await _rExercise.DelExercise(exercise);
            return Json(exercise);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    [HttpDelete("DelExercises")]
    public async Task<IActionResult> DelExercises([FromBody] List<Exercise> exercises)
    {
        try
        {
            await _rExercise.DelExercises(exercises);
            return Json(exercises);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    [HttpPut("UpdateExercise")]
    public async Task<IActionResult> UpdateExercise([FromBody] Exercise exercise)
    {
        try
        {
            var updExercise = await _rExercise.UpdateExercise(exercise);
            return Json(updExercise);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
}