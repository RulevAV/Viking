using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Viking.Interfaces;
using Viking.Models.Sports;

namespace Viking.Controllers;

[Authorize]
[Route("api/[controller]")]
public class WorkoutController : Controller
{
    private readonly IWorkout _rWorkout;
    private readonly IExercise _rExercise;
    private readonly ISet _rSet;

    public WorkoutController(IWorkout rWorkout, IExercise rExercise, ISet rSet)
    {
        _rWorkout = rWorkout;
        _rExercise = rExercise;
        _rSet = rSet;
    }

    [HttpPost("CreateNewWorkout")]
    public async Task<IActionResult> CreateNewWorkout([FromBody] Workout workout)
    {
        try
        {
            var newWorkout = _rWorkout.CreateWorkout(workout,
                Guid.Parse(this.User.Claims.First(t => t.Type == "idUser").Value.ToString()));
            await _rWorkout.AddNewWorkout(newWorkout);
            return Json(newWorkout);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("GetAllWorkout")]
    public async Task<IActionResult> GetAllWorkout()
    {
        return Json(
            await _rWorkout.GetWorkouts(Guid.Parse(this.User.Claims.First(t => t.Type == "idUser").Value.ToString())));
    }

    [HttpPost("CreateNewSet")]
    public async Task<IActionResult> CreateNewSet(Set set)
    {
        try
        {
            var newSet = _rSet.CreateNewSet(set);
            await _rSet.AddNewSet(newSet);
            return Json(newSet);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpDelete("DeleteWorkout/{id}")]
    public async Task<IActionResult> DeleteWorkout(Workout workout)
    {
        try
        {
            await _rWorkout.DelWorkout(workout);
            return Json(workout);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpDelete("DelSet")]
    public async Task<IActionResult> DelSet(Set set)
    {
        try
        {
            await _rSet.DelSet(set);
            return Ok();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpDelete("DelSets")]
    public async Task<IActionResult> DelSets(List<Set> sets)
    {
        try
        {
            await _rSet.DelSets(sets);
            return Ok();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPut("UpdateWorkout")]
    public async Task<IActionResult> UpdateWorkout([FromBody] Workout workout)
    {
        try
        {
            var updWorkout = await _rWorkout.UpdateWorkout(workout);
            return Json(updWorkout);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    [HttpPost("UpdateSet")]
    public async Task<IActionResult> UpdateSet(Set set)
    {
        try
        {
            var updSet = await _rSet.UpdateSet(set);
            return Json(updSet);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}