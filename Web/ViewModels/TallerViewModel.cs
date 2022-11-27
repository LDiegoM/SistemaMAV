using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class TallerViewModel {

    [Display(Name = "Código")]
    public int TallerId { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "Debe ingresar el Nombre")]
    public string Nombre { get; set; }

    [Display(Name = "Año de apertura")]
    public int AnioApertura { get; set; }

    [Display(Name = "Dirección de venta")]
    public string? Direccion { get; set; }

    [Display(Name = "Localidad")]
    [Required(ErrorMessage = "Debe ingresar la Localidad")]
    public int LocalidadId { get; set; }
    public Localidad? Localidad { get; set; }

    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Activo?")]
    public bool Activo { get; set; }

    [Display(Name = "Votos positivos")]
    public int VotosPositivos { get; set; }

    [Display(Name = "Votos negativos")]
    public int VotosNegativos { get; set; }

    [Display(Name = "Puntaje")]
    public int Puntaje { get; set; }

    public ICollection<Mantenimiento>? Mantenimientos { get; set; }

    public TallerViewModel() {
        Nombre = "";
    }

    public TallerViewModel(Taller taller) {
        TallerId = taller.TallerId;
        Nombre = taller.Nombre;
        AnioApertura = taller.AnioApertura;
        Direccion = taller.Direccion;
        LocalidadId = taller.LocalidadId;
        Localidad = taller.Localidad;
        Telefono = taller.Telefono;
        Email = taller.Email;
        Activo = taller.Activo;
        VotosPositivos = taller.VotosPositivos;
        VotosNegativos = taller.VotosNegativos;
        Puntaje = VotosPositivos - VotosNegativos;
    }

    public Taller ToTaller() {
        return new Taller() {
            TallerId = TallerId,
            Nombre = Nombre,
            AnioApertura = AnioApertura,
            Direccion = Direccion??"",
            LocalidadId = LocalidadId,
            Telefono = Telefono??"",
            Email = Email??"",
            Activo = Activo,
            VotosPositivos = VotosPositivos,
            VotosNegativos = VotosNegativos
        };
    }
}
