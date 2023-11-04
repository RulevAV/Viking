using Microsoft.AspNetCore.Mvc;
using Viking.Models;
using Viking.Models.Sports;

namespace Viking.Interfaces;

public interface IWorkout
{
    public Task<int> AddNewWorkout(Workout workout);
    public Task<int> AddNewExercise(Exercise exercise);
    public Task<int> AddNewSet(Set set);
    /// <summary>
    /// For del set from base
    /// </summary>
    /// <param name="set"></param>
    /// <returns></returns>
    public Task<int> DelSet(Set set);
    /// <summary>
    /// for del sets after del exercise
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <returns></returns>
    private Task<int> DelSets(Guid exerciseId)
    {
        throw new NotImplementedException();
    }
    public Task<int> DelSets(List<Set> sets);
    /// <summary>
    /// for del exercise
    /// </summary>
    /// <param name="exercise"></param>
    /// <returns></returns>
    public Task<int> DelExercise(Exercise exercise);

    /// <summary>
    /// for del exercise after del workout
    /// </summary>
    /// <param name="workoutId"></param>
    /// <returns></returns>
    public Task<int> DelExercises(List<Exercise> exercises);
    /// <summary>
    /// for del exercises after del workout
    /// </summary>
    /// <param name="workoutId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private Task<int> DelExercises(Guid workoutId)
    {
        throw new NotImplementedException();
    }
    public Task<int> DelWorkout(Workout workout );
    public Task<int> UpdateSet(Set set);
    public Task<int> UpdateExercise(Exercise exercise);
    public Task<int> UpdateWorkout(Workout workout );
    public Task<Workout> GetWorkout(Guid workoutId );
    public Task<Set> GetSet(Guid setId);
    public Task<Exercise> GetExercise(Guid exerciseId);
    public List<Workout> GetWorkouts(Guid userId );
    public Task<List<Exercise>> GetExercisesByWorkoutId(Guid workoutId );
    public Task<List<Set>> GetSets(Guid exerciseId);
    public Task<List<Exercise>> GetExercises(Guid workoutId);
    
}