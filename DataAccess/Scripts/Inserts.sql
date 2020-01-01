DELETE FROM TipoUnidad
SET IDENTITY_INSERT TipoUnidad ON
INSERT INTO TipoUnidad (TipoUnidadId,Detalle,Activo) VALUES (1,'Automóvil',1)
INSERT INTO TipoUnidad (TipoUnidadId,Detalle,Activo) VALUES (2,'Motocicleta',1)
INSERT INTO TipoUnidad (TipoUnidadId,Detalle,Activo) VALUES (3,'Vehículo pesado',1)
SET IDENTITY_INSERT TipoUnidad OFF
GO
DELETE FROM Marca
SET IDENTITY_INSERT Marca ON
INSERT INTO Marca (MarcaId,Detalle,Activo) VALUES (1,'Ford',1)
INSERT INTO Marca (MarcaId,Detalle,Activo) VALUES (2,'Chevrolet',1)
SET IDENTITY_INSERT Marca OFF
GO
DELETE FROM ItemMantenimiento
SET IDENTITY_INSERT ItemMantenimiento ON
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (1,'Cambio de aceite',10000,12)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (3,'Correa de accesorios: sustituir',50000,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (4,'Control de fugas de fluidos',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (5,'Prueba en ruta',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (6,'Sustituir bujías',30000,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (7,'Correa de distribución: inspeccionar',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (8,'Correa de distribución: sustituir',50000,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (9,'Aceite de transmisión',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (10,'Líquido de frenos',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (11,'Fluido de la dirección asistida',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (12,'Pedal de embrague',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (13,'Filtro de aire: inspeccionar',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (14,'Filtro de aire: sustituir',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (15,'Filtro de combustible: sustituir',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (17,'Prefiltro de bomba de combustible',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (18,'Sistema de aire acondicionado',NULL,NULL)
INSERT INTO ItemMantenimiento (ItemMantenimientoId,Detalle,KilometrosPredeterminado,TiempoPredeterminado) VALUES (19,'Sistema de refrigeración de motor',NULL,NULL)
SET IDENTITY_INSERT ItemMantenimiento OFF
GO
DELETE FROM Modelo
SET IDENTITY_INSERT Modelo ON
INSERT INTO Modelo (ModeloId,Detalle,MarcaId,TipoUnidadId,FechaAlta) VALUES (1,'Chevrolet Agile',2,1,'2005-01-01')
INSERT INTO Modelo (ModeloId,Detalle,MarcaId,TipoUnidadId,FechaAlta) VALUES (2,'Ford Fiesta Kinetic Design',1,1,'2005-01-01')
SET IDENTITY_INSERT Modelo OFF
GO
DELETE FROM Planilla
SET IDENTITY_INSERT Planilla ON
INSERT INTO Planilla (PlanillaId,ModeloId,Detalle,AnioFabricacion,Version,Activo) VALUES (1,1,'Mantenimiento Chevrolet Agile',2015,1,1)
SET IDENTITY_INSERT Planilla OFF
GO
DELETE FROM PlanillaItem
SET IDENTITY_INSERT PlanillaItem ON
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (1,1,1,10000,12,'Utilizar aceites Elaion F50 d1 (Dexos 1 API-SN ILSAC GF-5, grado SAE 5W30)','Cambiar y verificar nivel con el motor a temperatura de operación normal','Cambie el aceite del motor y el filtro de aceite conforme a los intervalos de tiempo o kilómetros recorridos, ya que los mismos pierden sus propiedades de lubricación no solo debido al funcionamiento del motor, sino también a su envejecimiento. Verificar el nivel de aceite semanalmente o antes de iniciar un viaje de más de 50 kilómetros. Tener en cuenta que el gasto promedio de aceite es de 0,8 litros cada 1000 km')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (3,1,4,10000,12,NULL,NULL,'Inspeccionar fugas de aceite, líquido refrigerante, de dirección, de freno, grasa de la caja de cambios y líquido lava-parabrisas.')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (4,1,5,30000,NULL,NULL,NULL,'Inspeccionar si el vehículo presenta anomalías ocasionales. Realizar una prueba en ruta después de la inspección')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (5,1,6,30000,NULL,NULL,NULL,NULL)
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (6,1,7,NULL,NULL,NULL,'Primer control a los 20.000 Km, luego cada 50.000 Km','Inspeccionar el estado de la correa y del tensor automático')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (7,1,8,50000,NULL,NULL,NULL,NULL)
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (8,1,3,50000,NULL,NULL,NULL,'Verificar el estado de los tensores')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (9,1,9,10000,12,'Aceite mineral para cajas de cambios, SAE 75W85, engranajes helicoidales, color rojo.','Caja de velocidades','Verificar el nivel y sustituir si fuera necesario')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (10,1,10,10000,24,'Líquido de frenos DOT 4 de ACDelco',NULL,'Verificar el nivel y completar al nivel si hay fuga. Se debe corregir inmediatamente si hay fuga.')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (11,1,11,10000,NULL,'Aceite Dexron II de ACDelco',NULL,'Verificar el nivel. No requiere cambio, excepto baja del nivel.')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (12,1,12,30000,NULL,NULL,NULL,'Comprobar el recorrido')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (13,1,13,NULL,NULL,NULL,'Primer control a los 20.000 Km, luego cada 30.000 Km','Limpiar el filtro si fuera necesario')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (14,1,14,30000,NULL,NULL,NULL,NULL)
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (16,1,17,80000,NULL,NULL,NULL,'Colador de la bomba de combustible')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (17,1,18,10000,NULL,'Gas R134a',NULL,'Controlar en cada inspección. No requiere sustitución, excepto que haya fuga.')
INSERT INTO PlanillaItem (PlanillaItemId,PlanillaId,ItemMantenimientoId,Kilometros,Meses,Recomendaciones,Observaciones,InfoExtra) VALUES (18,1,19,NULL,NULL,'Inspeccionar el nivel de líquido refrigerante mensualmente',NULL,'Cambiar el líquido refrigerante y reparar posibles fugas. Antes de cambiar se recomienda limpiar el sistema de refrigeración')
SET IDENTITY_INSERT PlanillaItem OFF