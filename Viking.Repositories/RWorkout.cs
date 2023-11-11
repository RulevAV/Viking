using System.Runtime.InteropServices.JavaScript;
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

    public Workout CreateWorkout(Workout workout, Guid idUser)
    {
        return new Workout
            {Id = Guid.NewGuid(), IdUser = idUser, WorkoutName = workout.WorkoutName, DateOfWeek = DateTime.Now};
    }

    public async Task<int> DelWorkout(Workout workout)
    {
        await _rExercise.DelExercises(workout.Id);
        ConVikingSports.Remove(workout);
        return await ConVikingSports.SaveChangesAsync();
    }

    public async Task<Workout> UpdateWorkout(Workout workout)
    {
        var oldWorkout = await GetWorkout(workout.Id);
        oldWorkout.WorkoutName = workout.WorkoutName;
        await ConVikingSports.SaveChangesAsync();
        return oldWorkout;
    }

    public async Task<Workout> GetWorkout(Guid IdWorkout)
    {
        return await ConVikingSports.Workouts.Include(w => w.Exercises).FirstAsync(t => t.Id == IdWorkout);
    }

    public async Task<List<Workout>> GetWorkouts(Guid IdUser)
    {
        return await ConVikingSports.Workouts.Where(t => t.IdUser == IdUser).Include(k => k.Exercises)
            .OrderBy(t => t.DateOfWeek).ToListAsync();
    }
}