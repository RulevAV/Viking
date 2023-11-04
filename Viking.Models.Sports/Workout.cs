﻿using System;
using System.Collections.Generic;

namespace Viking.Models.Sports;

public partial class Workout
{
    public Guid Id { get; set; }

    public Guid IdUser { get; set; }

    public string WorkoutName { get; set; } = null!;

    public DateOnly DateOfWeek { get; set; }
}