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
        private readonly RSet _rSet;
        public RExercise(conViking_Sports conVikingSports): base(conVikingSports)
        {
            _rSet = new RSet(conVikingSports);
        }
        public async Task<int> AddNewExercise(Exercise exercise)
        {
            await ConVikingSports.Exercises.AddAsync(exercise);

            return await ConVikingSports.SaveChangesAsync();
        }

        public Exercise CreateExercise(Exercise exercise)
        {
            return new Exercise { Id = Guid.NewGuid(),ExerciseName = exercise.ExerciseName, IdWorkout = exercise.IdWorkout};
        }

        public async Task<int> DelExercise(Exercise exercise)
        {
            await _rSet.DelSetsByExerciseId(exercise.Id);
            ConVikingSports.Exercises.Remove(exercise);
            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<int> DelExercises(List<Exercise> exercises)
        {
            await _rSet.DelSets(await _rSet.GetSetsByExercises(exercises));
            ConVikingSports.Exercises.RemoveRange(exercises);
            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<int> DelExercises(Guid idWorkout)
        {
            var exercises = await GetExercisesByWorkoutId(idWorkout);
            await _rSet.DelSets(await _rSet.GetSetsByExercises(exercises));
            ConVikingSports.RemoveRange(exercises);
            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<Exercise> UpdateExercise(Exercise exercise)
        {
            var oldExercise = await GetExercise(exercise.Id);
            oldExercise.ExerciseName = exercise.ExerciseName;
            await ConVikingSports.SaveChangesAsync();
            return oldExercise;
        }
        public async Task<Exercise> GetExercise(Guid idExercise)
        {
            return await ConVikingSports.Exercises.Include(u => u.Sets).FirstAsync(t => t.Id == idExercise);
        }
        public async Task<List<Exercise>> GetExercisesByWorkoutId(Guid idExercise)
        {
            return await ConVikingSports.Exercises.Where(t => t.IdWorkout== idExercise).Include(u => u.Sets).ToListAsync();
        }
        public async Task<List<Exercise>> GetExercises(Guid idExercise)
        {
            return await ConVikingSports.Exercises.Where(t => t.IdWorkout == idExercise).Include(u=>u.Sets).ToListAsync();
        }
    }
}
