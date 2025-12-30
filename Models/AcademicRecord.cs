using System;
using System.Collections.Generic;

namespace Gymnasieskola.Models;

public partial class AcademicRecord
{
    public int RecordId { get; set; }

    public string Grade { get; set; } = null!;

    public DateOnly GradingDate { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherId { get; set; }

    public bool IsOngoing { get; set; }

    public virtual GradeScale GradeNavigation { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Staff Teacher { get; set; } = null!;
}
