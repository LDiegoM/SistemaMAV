using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SistemaMAV.Entities.Models;
using SistemaMAV.Web.Data;

namespace SistemaMAV.Web.Helpers;

public static class Utils {
    public static IEnumerable<ModelStateEntry> GetErrorsList(ModelStateDictionary modelState) {
        var errors =
            from value in modelState.Values
            where value.ValidationState == ModelValidationState.Invalid
            select value;
        return errors;
    }

    public static int? GetProximoMantenimiento(this Vehiculo unVehiculo, ApplicationDbContext _context) {
        int? proximoMantenimiento = null;
        if (_context.Mantenimiento == null || _context.Planilla == null || _context.PlanillaItem == null)
            return null;

        // Busca el mínimo ciclo de mantenimiento de los ítems de mantenimiento del vehículo.
        int? kilometrosMantenimiento = (from p in _context.Planilla
                                        where p.ModeloId == unVehiculo.ModeloId
                                            && p.AnioFabricacion == unVehiculo.AnioFabricacion
                                            && p.Activo == true
                                        select p.Kilometros).FirstOrDefault();
        if (kilometrosMantenimiento == null || kilometrosMantenimiento == 0)
            return null;

        proximoMantenimiento = (unVehiculo.Kilometros / kilometrosMantenimiento + 1) * kilometrosMantenimiento;

        Mantenimiento? ultimoMantenimiento = (from m in _context.Mantenimiento
                                              where m.VehiculoId == unVehiculo.VehiculoId
                                              orderby m.Kilometros descending
                                              select new Mantenimiento
                                              {
                                                  MantenimientoId = m.MantenimientoId,
                                                  Fecha = m.Fecha,
                                                  PlanillaId = m.PlanillaId,
                                                  VehiculoId = m.VehiculoId,
                                                  TallerId = m.TallerId,
                                                  Kilometros = m.Kilometros,
                                                  Precio = m.Precio
                                              }).FirstOrDefault();
        if (ultimoMantenimiento != null && ultimoMantenimiento.Kilometros + kilometrosMantenimiento > proximoMantenimiento) {
            proximoMantenimiento = ultimoMantenimiento.Kilometros + kilometrosMantenimiento;
        }

        return proximoMantenimiento;
    }
}
