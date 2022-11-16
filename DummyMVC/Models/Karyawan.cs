using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DummyMVC.Models;

[Table("Karyawan")]
[Index("NoKaryawan", Name = "UQ__Karyawan__ED4D858352D5C2E3", IsUnique = true)]
public partial class Karyawan
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    [Required]
    public string? NoKaryawan { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    [Required]
    public string NamaKaryawan { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime? TanggalLahir { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Alamat { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    public int? Umur { get; set; }
}
