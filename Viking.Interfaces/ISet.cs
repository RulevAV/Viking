using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viking.Models.Sports;

namespace Viking.Interfaces
{
    public interface ISet
    {
        /// <summary>
        /// Добавление нового подхода
        /// Adding a new set
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public Task<int> AddNewSet(Set set);
        /// <summary>
        /// For del set from base
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public Task<int> DelSet(Set set);
        /// <summary>
        /// Удаление подходов при удалении упражнения
        /// Deleting sets when deleting an exercise
        /// </summary>
        /// <param name="idExercise"></param>
        /// <returns></returns>
        public Task<int> DelSetsByExerciseId(Guid idExercise);
        /// <summary>
        /// Удаление множества подходов по их сущностям
        /// Removing multiple sets by their entities
        /// </summary>
        /// <param name="sets"></param>
        /// <returns></returns>
        public Task<int> DelSets(List<Set> sets);
        /// <summary>
        /// Обновление полей подхода
        /// Updating set fields
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public Task<int> UpdateSet(Set set);
        /// <summary>
        /// Получение подхода по его id
        /// Getting a set by its id
        /// </summary>
        /// <param name="idSet"></param>
        /// <returns></returns>
        public Task<Set> GetSet(Guid idSet);
        /// <summary>
        /// Для удаления подходов при удалении одного упражнения
        /// To delete sets when deleting one exercise
        /// </summary>
        /// <param name="idExercise"></param>
        /// <returns></returns>
        public Task<List<Set>> GetSetsByExerciseId(Guid idExercise);
        /// <summary>
        /// Для удаления подходов при удалении множества упражнений
        /// To delete sets when deleting many exercises
        /// </summary>
        /// <param name="exercises"></param>
        /// <returns></returns>
        public Task<List<Set>> GetSetsByExercises(List<Exercise> exercises);

    }
}
