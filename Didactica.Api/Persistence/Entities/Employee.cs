﻿using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class Employee: BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }
    
    [MaxLength(255)]
    public required string Surname { get; set; }
    [MaxLength(255)]
    public string? Faculty { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
}