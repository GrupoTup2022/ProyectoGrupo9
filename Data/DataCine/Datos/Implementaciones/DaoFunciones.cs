﻿
using DataCine.Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataCine.Dominio;
using LibreriaTp;


namespace DataCine.Datos.Implementaciones
{
    public class DaoFunciones : IDaoFunciones
    {
        public int AltaFuncion(Funcion funcion)
        {
            List<Parametro> lista_parametros = new List<Parametro>();
            lista_parametros.Add(new Parametro("@id_pelicula", funcion.Pelicula.Id));
            lista_parametros.Add(new Parametro("@id_horario", funcion.Horario));
            lista_parametros.Add(new Parametro("@id_audio", funcion.Audio.Id));
            lista_parametros.Add(new Parametro("@id_sala", funcion.Sala.Id));
            lista_parametros.Add(new Parametro("@precio", funcion.Precio));
            lista_parametros.Add(new Parametro("@fecha", funcion.fecha));
            return HelperDAO.getinstancia().UtilizarProcedimiento("SP_DELETE_FUNCION", lista_parametros);
        }
        //ejecutar sp baja_funcion
        public int BajaLogicaFuncion(Funcion funcion)
        {
            List<Parametro> lista_parametro = new List<Parametro>();

            lista_parametro.Add(new Parametro("@id_funcion", funcion.Id));




          return  HelperDAO.getinstancia().UtilizarProcedimiento("SP_BAJA_FUNCION", lista_parametro);
             

        }

        public List<Audio> consultarAudios()
        {
            List<Audio> lista_audios = new List<Audio>();
            DataTable tabla = HelperDAO.getinstancia().ConsultarDB("SP_CONSULTAR_AUDIOS");
            foreach ( DataRow row in tabla.Rows)
            {
                Audio audio_nuevo = new Audio();
                audio_nuevo.Id = Convert.ToInt32(row["id_audio"]);
                audio_nuevo.Nombre = row["descripcion"].ToString();
                lista_audios.Add(audio_nuevo);
            }
            return lista_audios;
        }

        public List<Funcion> consultarFunciones()
        {
            List<Funcion> lista_funciones = new List<Funcion>();
            DataTable tabla = HelperDAO.getinstancia().ConsultarDB("SP_CONSULTAR_FUNCIONES");
            foreach (DataRow row in tabla.Rows)
            {
                Funcion funcion_nueva = new Funcion();
                funcion_nueva.Id = Convert.ToInt32(row["id_funcion"]);
                funcion_nueva.Pelicula.Titulo_local = row["nombre_pelicula"].ToString();
                funcion_nueva.Horario.Nombre = row["horario"].ToString();
                funcion_nueva.Audio.Nombre = row["audio"].ToString();
                funcion_nueva.Sala.Nombre = row["sala"].ToString();
                funcion_nueva.Precio = Convert.ToInt32(row["precio"]);
                funcion_nueva.fecha = Convert.ToDateTime(row["fecha"]);
               
               
              
                lista_funciones.Add(funcion_nueva);
            }
            return lista_funciones;
        }

        public List<Pelicula> consultarPeliculas()
        {
            List<Pelicula> lista_peliculas = new List<Pelicula>();
            DataTable tabla = HelperDAO.getinstancia().ConsultarDB("SP_CONSULTAR_PELICULAS");
            foreach (DataRow row in tabla.Rows)
            {
                Pelicula pelicula_nueva = new Pelicula();
                pelicula_nueva.Id = Convert.ToInt32(row["id_pelicula"]);
                pelicula_nueva.Titulo_local = row["titulo_local"].ToString();
                pelicula_nueva.Titulo_original = row["titulo_original"].ToString();
                pelicula_nueva.clasificacion.Id = Convert.ToInt32(row["id_calificacion"]);
                pelicula_nueva.pais.Id = Convert.ToInt32(row["id_pais"]);
                
                pelicula_nueva.Fecha_Estreno = Convert.ToDateTime(row["fecha_estreno"]);
                pelicula_nueva.duracion = Convert.ToInt32(row["duracion_min"]);
                pelicula_nueva.distribuidora.Id = Convert.ToInt32(row["id_distribuidora"]);
                pelicula_nueva.genero.Id = Convert.ToInt32(row["id_genero"]);
                pelicula_nueva.director.Id = Convert.ToInt32(row["id_director"]);
              
                pelicula_nueva.Baja = Convert.ToInt32(row["baja"]);
                lista_peliculas.Add(pelicula_nueva);


            }
            return lista_peliculas;
        }
     

        public List<Sala> consultarSalas()
        {
            List<Sala> lista_salas = new List<Sala>();
            DataTable tabla = HelperDAO.getinstancia().ConsultarDB("SP_CONSULTAR_SALAS");
            foreach(DataRow row in tabla.Rows)
            {
                Sala sala_nueva = new Sala();
                sala_nueva.Id = Convert.ToInt32(row["id_sala"]);
                sala_nueva.TipoSala.Id = Convert.ToInt32(row["id_tipo_sala"]);
                sala_nueva.Nombre = row["descripcion"].ToString();
                lista_salas.Add(sala_nueva);
            }
            return lista_salas;
        }

      

        public List<Horario> consutlarHorarios()
        {
            List<Horario> lista_horarios = new List<Horario>();
            DataTable tabla = HelperDAO.getinstancia().ConsultarDB("SP_CONSULTAR_HORARIOS");
            foreach(DataRow row in tabla.Rows)
            {
                Horario horario_nuevo = new Horario();
                horario_nuevo.Id = Convert.ToInt32(row["id_horario"]);
                horario_nuevo.Nombre = row["horario"].ToString();
                lista_horarios.Add(horario_nuevo);
            }
            return lista_horarios;
        }
    }
}
