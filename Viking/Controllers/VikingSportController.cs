using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Viking.Interfaces;

namespace Viking.Controllers;

[Authorize]
[Route("api/[controller]")]
public class VikingSportController : Controller
{
   private readonly IWorkout _rWorkout;
   private readonly IExercise _rExercise;
   private readonly ISet _rSet;

   public VikingSportController(IWorkout rWorkout, IExercise rExercise, ISet rSet)
   {
      _rWorkout = rWorkout;
      _rExercise = rExercise;
      _rSet = rSet;
   }
   
   
   
}