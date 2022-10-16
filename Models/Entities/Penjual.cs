﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAS.Bootcamp.Mvc.Net.Models.Entities;

public class Penjual
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int IdUser { get; set; }
    public string NamaToko { get; set; }
    public string Alamat { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Barang> Barangs { get; set; }
}