﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesAPI.Models;

public partial class Camiones
{
    [Key]
    public int ID_Camion { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Matricula { get; set; }

    [Required]
    [StringLength(25)]
    [Unicode(false)]
    public string Tipo_Camion { get; set; }

    [Required]
    [StringLength(25)]
    [Unicode(false)]
    public string Marca { get; set; }

    [Required]
    [StringLength(25)]
    [Unicode(false)]
    public string Modelo { get; set; }

    public int Capacidad { get; set; }

    public double Kilometraje { get; set; }

    [Required]
    [StringLength(250)]
    [Unicode(false)]
    public string UrlFoto { get; set; }

    public bool Disponibilidad { get; set; }
}