using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viking.Models.Sports;

namespace Viking.Interfaces
{
    public interface IExercise
    {
        public Task<int> AddNewExercise(Exercise exercise);
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
        public Task<int> UpdateExercise(Exercise exercise);
        public Task<Exercise> GetExercise(Guid exerciseId);
        public Task<List<Exercise>> GetExercisesByWorkoutId(Guid workoutId);
    }
}
