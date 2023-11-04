using Viking.Models.Sports;

namespace Viking.Interfaces;

public interface IWorkout
{
    public Task<int> AddNewWorkout(Workout workout);
    public Task<int> DelWorkout(Workout workout );
    public Task<int> UpdateWorkout(Workout workout );
    public Task<Workout> GetWorkout(Guid workoutId );
    public List<Workout> GetWorkouts(Guid userId );
}