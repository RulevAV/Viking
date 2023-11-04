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
        public Task<int> UpdateSet(Set set);
        public Task<Set> GetSet(Guid setId);
        public Task<List<Set>> GetSetsByExerciseId(Guid exerciseId);
    }
}
