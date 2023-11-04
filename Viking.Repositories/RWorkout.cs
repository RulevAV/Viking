using Microsoft.EntityFrameworkCore;
using Viking.Interfaces;
using Viking.Models.Sports;

namespace Viking.Repositories;

public class RWorkout : Base, IWorkout
{
    public RWorkout(conViking_Sports conVikingSports) : base(conVikingSports) { }
    public async Task<int> AddNewWorkout(Workout workout)
    {
        await _conVikingSports.Workouts.AddAsync(workout);
        return await _conVikingSports.SaveChangesAsync();
    }
    public async Task<int> DelWorkout(Workout workout)
    {
        _conVikingSports.Remove(workout);
        return await _conVikingSports.SaveChangesAsync();
    }
    public async Task<int> UpdateWorkout(Workout workout)
    {
        var oldWorkout = await GetWorkout(workout.Id);
        oldWorkout = workout;
        return await _conVikingSports.SaveChangesAsync();
    }
    public async Task<Workout> GetWorkout(Guid IdWorkout)
    {
        return await _conVikingSports.Workouts.FirstAsync(t => t.Id == IdWorkout);
    }
    public List<Workout> GetWorkouts(Guid IdUser)
    {
        return  _conVikingSports.Workouts.Where(t => t.UserId == IdUser).ToList();
    }
}