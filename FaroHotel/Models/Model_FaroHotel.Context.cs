﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FaroHotel.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FaroHotelEntities : DbContext
    {
        public FaroHotelEntities()
            : base("name=FaroHotelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accion> Accion { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<EnlaceBusPasajero> EnlaceBusPasajero { get; set; }
        public virtual DbSet<EnlaceReservaHotelHabitacion> EnlaceReservaHotelHabitacion { get; set; }
        public virtual DbSet<EnlaceReservaHotelPasajero> EnlaceReservaHotelPasajero { get; set; }
        public virtual DbSet<Habitacion> Habitacion { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuAspNetRoles> MenuAspNetRoles { get; set; }
        public virtual DbSet<MenuAspNetRolesAccion> MenuAspNetRolesAccion { get; set; }
        public virtual DbSet<Paquete> Paquete { get; set; }
        public virtual DbSet<Pasajero> Pasajero { get; set; }
        public virtual DbSet<ReservaHotel> ReservaHotel { get; set; }
        public virtual DbSet<TipoCuota> TipoCuota { get; set; }
        public virtual DbSet<TipoDescripcionPaquete> TipoDescripcionPaquete { get; set; }
        public virtual DbSet<TipoHabitacion> TipoHabitacion { get; set; }
        public virtual DbSet<TipoHotel> TipoHotel { get; set; }
        public virtual DbSet<TipoNoche> TipoNoche { get; set; }
        public virtual DbSet<TipoSexo> TipoSexo { get; set; }
        public virtual DbSet<TipoTemporada> TipoTemporada { get; set; }
        public virtual DbSet<TipoTrayecto> TipoTrayecto { get; set; }
        public virtual DbSet<Ventanilla> Ventanilla { get; set; }
        public virtual DbSet<TipoBase> TipoBase { get; set; }
        public virtual DbSet<EnlaceReservaHotelExtra> EnlaceReservaHotelExtra { get; set; }
        public virtual DbSet<Extra> Extra { get; set; }
        public virtual DbSet<TipoExtra> TipoExtra { get; set; }
    
        [DbFunction("FaroHotelEntities", "func_Split")]
        public virtual IQueryable<func_Split_Result> func_Split(string delimitedString, string delimiter)
        {
            var delimitedStringParameter = delimitedString != null ?
                new ObjectParameter("DelimitedString", delimitedString) :
                new ObjectParameter("DelimitedString", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<func_Split_Result>("[FaroHotelEntities].[func_Split](@DelimitedString, @Delimiter)", delimitedStringParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<GetPermisosPorNombreDeUsuario_Result> GetPermisosPorNombreDeUsuario(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPermisosPorNombreDeUsuario_Result>("GetPermisosPorNombreDeUsuario", userNameParameter);
        }
    
        public virtual ObjectResult<InsertBooking_Result> InsertBooking(Nullable<System.DateTime> fecha, Nullable<int> hotelId, Nullable<int> noches, Nullable<int> titularId, string tipoHabitacionesIds, string nroHabitacionesIds, string pasajerosIds, string paquetesIds, string baseIds, string usuarioAlta, string busesIdaIds, string asientosIdaIds, string busesVueltaIds, string asientosVueltaIds, string extrasIds, string extrasCantidad)
        {
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            var hotelIdParameter = hotelId.HasValue ?
                new ObjectParameter("HotelId", hotelId) :
                new ObjectParameter("HotelId", typeof(int));
    
            var nochesParameter = noches.HasValue ?
                new ObjectParameter("Noches", noches) :
                new ObjectParameter("Noches", typeof(int));
    
            var titularIdParameter = titularId.HasValue ?
                new ObjectParameter("TitularId", titularId) :
                new ObjectParameter("TitularId", typeof(int));
    
            var tipoHabitacionesIdsParameter = tipoHabitacionesIds != null ?
                new ObjectParameter("TipoHabitacionesIds", tipoHabitacionesIds) :
                new ObjectParameter("TipoHabitacionesIds", typeof(string));
    
            var nroHabitacionesIdsParameter = nroHabitacionesIds != null ?
                new ObjectParameter("NroHabitacionesIds", nroHabitacionesIds) :
                new ObjectParameter("NroHabitacionesIds", typeof(string));
    
            var pasajerosIdsParameter = pasajerosIds != null ?
                new ObjectParameter("PasajerosIds", pasajerosIds) :
                new ObjectParameter("PasajerosIds", typeof(string));
    
            var paquetesIdsParameter = paquetesIds != null ?
                new ObjectParameter("PaquetesIds", paquetesIds) :
                new ObjectParameter("PaquetesIds", typeof(string));
    
            var baseIdsParameter = baseIds != null ?
                new ObjectParameter("BaseIds", baseIds) :
                new ObjectParameter("BaseIds", typeof(string));
    
            var usuarioAltaParameter = usuarioAlta != null ?
                new ObjectParameter("UsuarioAlta", usuarioAlta) :
                new ObjectParameter("UsuarioAlta", typeof(string));
    
            var busesIdaIdsParameter = busesIdaIds != null ?
                new ObjectParameter("BusesIdaIds", busesIdaIds) :
                new ObjectParameter("BusesIdaIds", typeof(string));
    
            var asientosIdaIdsParameter = asientosIdaIds != null ?
                new ObjectParameter("AsientosIdaIds", asientosIdaIds) :
                new ObjectParameter("AsientosIdaIds", typeof(string));
    
            var busesVueltaIdsParameter = busesVueltaIds != null ?
                new ObjectParameter("BusesVueltaIds", busesVueltaIds) :
                new ObjectParameter("BusesVueltaIds", typeof(string));
    
            var asientosVueltaIdsParameter = asientosVueltaIds != null ?
                new ObjectParameter("AsientosVueltaIds", asientosVueltaIds) :
                new ObjectParameter("AsientosVueltaIds", typeof(string));
    
            var extrasIdsParameter = extrasIds != null ?
                new ObjectParameter("ExtrasIds", extrasIds) :
                new ObjectParameter("ExtrasIds", typeof(string));
    
            var extrasCantidadParameter = extrasCantidad != null ?
                new ObjectParameter("ExtrasCantidad", extrasCantidad) :
                new ObjectParameter("ExtrasCantidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InsertBooking_Result>("InsertBooking", fechaParameter, hotelIdParameter, nochesParameter, titularIdParameter, tipoHabitacionesIdsParameter, nroHabitacionesIdsParameter, pasajerosIdsParameter, paquetesIdsParameter, baseIdsParameter, usuarioAltaParameter, busesIdaIdsParameter, asientosIdaIdsParameter, busesVueltaIdsParameter, asientosVueltaIdsParameter, extrasIdsParameter, extrasCantidadParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<GetHabitacionesDisponibles_Result> GetHabitacionesDisponibles(Nullable<System.DateTime> fecha, Nullable<int> hotelId, Nullable<int> tipoHabitacion)
        {
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            var hotelIdParameter = hotelId.HasValue ?
                new ObjectParameter("HotelId", hotelId) :
                new ObjectParameter("HotelId", typeof(int));
    
            var tipoHabitacionParameter = tipoHabitacion.HasValue ?
                new ObjectParameter("TipoHabitacion", tipoHabitacion) :
                new ObjectParameter("TipoHabitacion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetHabitacionesDisponibles_Result>("GetHabitacionesDisponibles", fechaParameter, hotelIdParameter, tipoHabitacionParameter);
        }
    
        public virtual ObjectResult<GetReservaHabitaciones_Result> GetReservaHabitaciones(Nullable<int> reservaId)
        {
            var reservaIdParameter = reservaId.HasValue ?
                new ObjectParameter("ReservaId", reservaId) :
                new ObjectParameter("ReservaId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReservaHabitaciones_Result>("GetReservaHabitaciones", reservaIdParameter);
        }
    
        public virtual ObjectResult<GetReservaPasajeros_Result> GetReservaPasajeros(Nullable<int> reservaId)
        {
            var reservaIdParameter = reservaId.HasValue ?
                new ObjectParameter("ReservaId", reservaId) :
                new ObjectParameter("ReservaId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReservaPasajeros_Result>("GetReservaPasajeros", reservaIdParameter);
        }
    
        public virtual ObjectResult<GetReserva_Result> GetReserva(Nullable<int> reservaId)
        {
            var reservaIdParameter = reservaId.HasValue ?
                new ObjectParameter("ReservaId", reservaId) :
                new ObjectParameter("ReservaId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReserva_Result>("GetReserva", reservaIdParameter);
        }
    
        public virtual ObjectResult<GetBuses_Result> GetBuses(Nullable<System.DateTime> fecha, Nullable<int> noches)
        {
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            var nochesParameter = noches.HasValue ?
                new ObjectParameter("Noches", noches) :
                new ObjectParameter("Noches", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetBuses_Result>("GetBuses", fechaParameter, nochesParameter);
        }
    
        public virtual ObjectResult<GetOcupacionBuses_Result> GetOcupacionBuses(Nullable<System.DateTime> rFecha)
        {
            var rFechaParameter = rFecha.HasValue ?
                new ObjectParameter("RFecha", rFecha) :
                new ObjectParameter("RFecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOcupacionBuses_Result>("GetOcupacionBuses", rFechaParameter);
        }
    }
}
