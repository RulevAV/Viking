using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Viking.Interfaces;
using Viking.Models;
using Viking.Models.Contexts;
using Viking.Models.Sports;

namespace Viking.Repositories;

public class RWorkout : IWorkout
{
    private readonly conViking_Sports _conVikingSports;
    
    public RWorkout(conViking_Sports conVikingSports)
    {
        _conVikingSports = conVikingSports;
    }
    
    public async Task<int> AddNewWorkout(Workout workout)
    {
        await _conVikingSports.Workouts.AddAsync(workout);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> AddNewExercise(Exercise exercise)
    {
        await _conVikingSports.Exercises.AddAsync(new Exercise
        {
            Id = Guid.NewGuid(),
            ExercisesName = exercise.ExercisesName,
            WorkoutId = exercise.WorkoutId
        });
        
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> AddNewSet(Set set)
    {
        await _conVikingSports.Sets.AddAsync(new Set
        {
            Id = Guid.NewGuid(),
            Number = set.Number,
            ExerciseId = set.ExerciseId,
            LapsTime = set.LapsTime,
            RepetitionNuber = set.RepetitionNuber,
            SetWeight = set.SetWeight
        });
        
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> DelSet(Set set)
    {
         _conVikingSports.Remove(set);
         return await _conVikingSports.SaveChangesAsync();
    }

    private async Task<int> DelSets(Guid exerciseId)
    {
        var sets = await GetSet(exerciseId);
        _conVikingSports.Sets.RemoveRange(sets);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> DelSets(List<Set> sets)
    {
        _conVikingSports.RemoveRange(sets);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> DelExercise(Exercise exercise)
    {
        _conVikingSports.Exercises.Remove(exercise);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> DelExercises(List<Exercise> exercises)
    {
        _conVikingSports.Exercises.RemoveRange(exercises);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> DelExercises(Guid workoutId)
    {
        var exercises = await GetExercisesByWorkoutId(workoutId);
        _conVikingSports.RemoveRange(exercises);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> DelWorkout(Workout workout)
    {
        _conVikingSports.Remove(workout);
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> UpdateSet(Set set)
    {
        var oldSet = await GetSet(set.Id);
        oldSet = set;
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> UpdateExercise(Exercise exercise)
    {
        var oldExercise = await GetExercise(exercise.Id);
        oldExercise = exercise;
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<int> UpdateWorkout(Workout workout)
    {
        var oldWorkout = await GetWorkout(workout.Id);
        oldWorkout = workout;
        return await _conVikingSports.SaveChangesAsync();
    }

    public async Task<Workout> GetWorkout(Guid workoutId)
    {
        return await _conVikingSports.Workouts.FirstAsync(t => t.Id == workoutId);
    }

    public async Task<Set> GetSet(Guid setId)
    {
        return await _conVikingSports.Sets.FirstAsync(t => t.Id == setId);
    }

    public async Task<Exercise> GetExercise(Guid exerciseId)
    {
        return await _conVikingSports.Exercises.FirstAsync(t => t.Id == exerciseId);
    }

    public List<Workout> GetWorkouts(Guid userId)
    {
        return  _conVikingSports.Workouts.Where(t => t.UserId == userId).ToList();
    }

    public async Task<List<Exercise>> GetExercisesByWorkoutId(Guid workoutId)
    {
        return await _conVikingSports.Exercises.Where(t => t.WorkoutId == workoutId).ToListAsync();
    }

    public async Task<List<Set>> GetSets(Guid exerciseId)
    {
        return await _conVikingSports.Sets.Where(t => t.ExerciseId == exerciseId).ToListAsync();
    }

    public async Task<List<Exercise>> GetExercises(Guid workoutId)
    {
        return await _conVikingSports.Exercises.Where(t => t.WorkoutId == workoutId).ToListAsync();
    }
}