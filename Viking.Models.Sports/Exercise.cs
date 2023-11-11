using System;
using System.Collections.Generic;

namespace Viking.Models.Sports;

public partial class Exercise
{
    public Guid Id { get; set; }

    public Guid IdWorkout { get; set; }

    public string ExerciseName { get; set; } = null!;

    public virtual Workout IdWorkoutNavigation { get; set; } = null!;
}
