using System;
using System.Collections.Generic;

namespace Gymnasieskola.Models;

public partial class GradeScale
{
    public string GradeLetter { get; set; } = null!;

    public decimal Points { get; set; }

    public virtual ICollection<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();
}
