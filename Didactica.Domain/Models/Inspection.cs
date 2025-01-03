﻿using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

public class Inspection : BaseTrackingEntity
{
    public required InspectionMethod InspectionMethod { get; set; }
    public required Teacher Teacher { get; set; }
    public required Lesson Lesson { get; set; }
    public required bool IsRemote { get; set; }
    [MaxLength(100)]
    public string? LessonEnvironment { get; set; }
}