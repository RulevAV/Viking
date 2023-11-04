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
            await ConVikingSports.Sets.AddAsync(new Set
            {
                Id = Guid.NewGuid(),
                Number = set.Number,
                IdExercise= set.IdExercise,
                LapsTime = set.LapsTime,
                RepetitionNuber = set.RepetitionNuber,
                SetWeight = set.SetWeight
            });

            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<int> DelSet(Set set)
        {
            ConVikingSports.Remove(set);
            return await ConVikingSports.SaveChangesAsync();
        }
        private async Task<int> DelSets(Guid idExercise)
        {
            var sets = await GetSet(idExercise);
            ConVikingSports.Sets.RemoveRange(sets);
            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<int> DelSets(List<Set> sets)
        {
            ConVikingSports.RemoveRange(sets);
            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<int> UpdateSet(Set set)
        {
            var oldSet = await GetSet(set.Id);
            oldSet = set;
            return await ConVikingSports.SaveChangesAsync();
        }
        public async Task<Set> GetSet(Guid idSet)
        {
            return await ConVikingSports.Sets.FirstAsync(t => t.Id == idSet);
        }
        public async Task<List<Set>> GetSets(Guid idExercise)
        {
            return await ConVikingSports.Sets.Where(t => t.IdExercise == idExercise).ToListAsync();
        }
        public async Task<List<Set>> GetSetsByExerciseId(Guid idExercise)
        {
            return await ConVikingSports.Sets.Where(u => u.IdExercise == idExercise).ToListAsync();
        }
    }
}
