using System;
using System.Collections.Generic;

namespace Viking.Models.Sports;

public partial class Set
{
    public Guid Id { get; set; }

    public Guid ExerciseId { get; set; }

    public long Number { get; set; }

    public long SetWeight { get; set; }

    public short? RepetitionNuber { get; set; }

    public long? LapsTime { get; set; }
}
