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
    public class RSet : Base, ISet
    {
        public RSet(conViking_Sports conVikingSports) : base(conVikingSports) { }
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
        private async Task<int> DelSets(Guid IdExercise)
        {
            var sets = await GetSet(IdExercise);
            _conVikingSports.Sets.RemoveRange(sets);
            return await _conVikingSports.SaveChangesAsync();
        }
        public async Task<int> DelSets(List<Set> sets)
        {
            _conVikingSports.RemoveRange(sets);
            return await _conVikingSports.SaveChangesAsync();
        }
        public async Task<int> UpdateSet(Set set)
        {
            var oldSet = await GetSet(set.Id);
            oldSet = set;
            return await _conVikingSports.SaveChangesAsync();
        }
        public async Task<Set> GetSet(Guid IdSet)
        {
            return await _conVikingSports.Sets.FirstAsync(t => t.Id == IdSet);
        }
        public async Task<List<Set>> GetSets(Guid IdExercise)
        {
            return await _conVikingSports.Sets.Where(t => t.ExerciseId == IdExercise).ToListAsync();
        }
        public async Task<List<Set>> GetSetsByExerciseId(Guid IdExercise)
        {
            return await _conVikingSports.Sets.Where(u => u.ExerciseId == IdExercise).ToListAsync();
        }
    }
}
