using Microsoft.EntityFrameworkCore;
using Viking.Interfaces;
using Viking.Models.Sports;

namespace Viking.Repositories;

public class RWorkout : Base, IWorkout
{
    private readonly RExercise _rExercise;
    public RWorkout(conViking_Sports conVikingSports) : base(conVikingSports)
    {
        _rExercise = new RExercise(conVikingSports);
    }
    public async Task<int> AddNewWorkout(Workout workout)
    {
        await ConVikingSports.Workouts.AddAsync(workout);
        return await ConVikingSports.SaveChangesAsync();
    }
    public async Task<int> DelWorkout(Workout workout)
    {
        await _rExercise.DelExercises(workout.Id);
        ConVikingSports.Remove(workout);
        return await ConVikingSports.SaveChangesAsync();
    }
    public async Task<int> UpdateWorkout(Workout workout)
    {
        var oldWorkout = await GetWorkout(workout.Id);
        oldWorkout = workout;
        return await ConVikingSports.SaveChangesAsync();
    }
    public async Task<Workout> GetWorkout(Guid IdWorkout)
    {
        return await ConVikingSports.Workouts.FirstAsync(t => t.Id == IdWorkout);
    }
    public List<Workout> GetWorkouts(Guid IdUser)
    {
        return  ConVikingSports.Workouts.Where(t => t.IdUser == IdUser).ToList();
    }
}