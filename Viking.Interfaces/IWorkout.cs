using Viking.Models.Sports;

namespace Viking.Interfaces;

public interface IWorkout
{
    /// <summary>
    /// Добавление тренировки в базу
    /// Adding a Workout to base
    /// </summary>
    /// <param name="workout"></param>
    /// <returns></returns>
    public Task<int> AddNewWorkout(Workout workout);
    /// <summary>
    /// Создание тренировки
    /// Create workout
    /// </summary>
    /// <param name="workout"></param>
    /// <param name="idUser"></param>
    /// <returns></returns>
    public  Workout CreateWorkout(Workout workout, Guid idUser);
    /// <summary>
    /// Удаление тренировки по ее сущности
    /// Deleting a workout by its essence
    /// </summary>
    /// <param name="workout"></param>
    /// <returns></returns>
    public Task<int> DelWorkout(Workout workout );
    /// <summary>
    /// Обновление полей тренировки по ее сущности
    /// Updating workout fields based on its entity
    /// </summary>
    /// <param name="workout"></param>
    /// <returns></returns>
    public Task<Workout> UpdateWorkout(Workout workout );
    /// <summary>
    /// Получение тренировки по ее Id
    /// Receiving training by her ID
    /// </summary>
    /// <param name="workoutId"></param>
    /// <returns></returns>
    public Task<Workout> GetWorkout(Guid workoutId );
    /// <summary>
    /// Получение тренировок по пользователю
    /// Getting training by user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<Workout>> GetWorkouts(Guid userId );
}