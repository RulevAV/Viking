using System;
using System.Collections.Generic;

namespace Viking.Models.Sports;

public partial class Set
{
    public Guid Id { get; set; }

    public Guid IdExercise { get; set; }

    public long Number { get; set; }

    public long SetWeight { get; set; }

    public short? RepetitionNumber { get; set; }

    public long? LapsTime { get; set; }

    public virtual Exercise IdExerciseNavigation { get; set; } = null!;
}
