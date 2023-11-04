using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viking.Interfaces;
using Viking.Models.Sports;

namespace Viking.Repositories
{
    public class RExercise : Base, IExercise
    {
        public RExercise(conViking_Sports conVikingSports): base(conVikingSports) {}
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
        public async Task<int> DelExercises(Guid IdWorkout)
        {
            var exercises = await GetExercisesByWorkoutId(IdWorkout);
            _conVikingSports.RemoveRange(exercises);
            return await _conVikingSports.SaveChangesAsync();
        }
        public async Task<int> UpdateExercise(Exercise exercise)
        {
            var oldExercise = await GetExercise(exercise.Id);
            oldExercise = exercise;
            return await _conVikingSports.SaveChangesAsync();
        }
        public async Task<Exercise> GetExercise(Guid IdExercise)
        {
            return await _conVikingSports.Exercises.FirstAsync(t => t.Id == exerciseId);
        }
        public async Task<List<Exercise>> GetExercisesByWorkoutId(Guid IdExercise)
        {
            return await _conVikingSports.Exercises.Where(t => t.WorkoutId == IdExercise).ToListAsync();
        }
        public async Task<List<Exercise>> GetExercises(Guid IdExercise)
        {
            return await _conVikingSports.Exercises.Where(t => t.WorkoutId == IdExercise).ToListAsync();
        }
    }
}
