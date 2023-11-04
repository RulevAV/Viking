using System;
using System.Collections.Generic;

namespace Viking.Models;

public partial class Workout
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string WorkoutName { get; set; } = null!;

    public DateOnly DateOfWeek { get; set; }
}
