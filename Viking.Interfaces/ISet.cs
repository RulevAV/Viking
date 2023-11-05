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
        public Task<int> AddNewSet(Set set);
        /// <summary>
        /// For del set from base
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public Task<int> DelSet(Set set);
        public Task<int> DelSetsByExerciseId(Guid idExercise);
        public Task<int> DelSets(List<Set> sets);
        public Task<int> UpdateSet(Set set);
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
