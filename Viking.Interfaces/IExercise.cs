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
        /// <summary>
        /// Добавление нового упражнения
        /// Adding a new exercise
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        public Task<int> AddNewExercise(Exercise exercise);
        /// <summary>
        /// Создание тренировки
        /// Create exercise
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        public Exercise CreateExercise(Exercise exercise);
        /// <summary>
        /// Удаление упражнения
        /// for del exercise
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        public Task<int> DelExercise(Exercise exercise);
        /// <summary>
        /// Для удаления множества упражнений
        /// To delete multiple exercises
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        public Task<int> DelExercises(List<Exercise> exercises);
        /// <summary>
        /// Обновление полей упражнения
        /// Updating Exercise Fields
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        public Task<Exercise> UpdateExercise(Exercise exercise);
        /// <summary>
        /// Получение упражнения по Id тренировки
        /// Receiving an exercise by workout ID
        /// </summary>
        /// <param name="exerciseId"></param>
        /// <returns></returns>
        public Task<Exercise> GetExercise(Guid exerciseId);
        /// <summary>
        /// Получение упражнений по тренировке
        /// Getting Workout Exercises
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        public Task<List<Exercise>> GetExercisesByWorkoutId(Guid workoutId);
    }
}
