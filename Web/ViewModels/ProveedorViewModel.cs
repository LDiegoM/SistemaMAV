using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SistemaMAV.Entities.Models;

namespace SistemaMAV.Web.ViewModels;
public class ProveedorViewModel {

    [Display(Name = "Código")]
    public int ProveedorId { get; set; }

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

    public ICollection<Stock>? Stock { get; set; }

    public ProveedorViewModel() {
        Nombre = "";
    }

    public ProveedorViewModel(Proveedor proveedor) {
        ProveedorId = proveedor.ProveedorId;
        Nombre = proveedor.Nombre;
        AnioApertura = proveedor.AnioApertura;
        Direccion = proveedor.Direccion;
        LocalidadId = proveedor.LocalidadId;
        Localidad = proveedor.Localidad;
        Telefono = proveedor.Telefono;
        Email = proveedor.Email;
        Activo = proveedor.Activo;
        VotosPositivos = proveedor.VotosPositivos;
        VotosNegativos = proveedor.VotosNegativos;
        Puntaje = VotosPositivos - VotosNegativos;
    }

    public Proveedor ToProveedor() {
        return new Proveedor() {
            ProveedorId = ProveedorId,
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
