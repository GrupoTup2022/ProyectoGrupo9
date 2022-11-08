﻿using DataCine.Datos.Interfaces;
using DataCine.Dominio;
using LibreriaTp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCine.Datos.Implementaciones
{
    public class DaoPeliculas : IDaoPeliculas
    {
    

        public bool CargarPelicula(Pelicula oPelicula)
        {

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@titulo_local", oPelicula.Titulo_local));
            lParametros.Add(new Parametro("@titulo_original", oPelicula.Titulo_original));
            lParametros.Add(new Parametro("@descripcion", oPelicula.Descripcion));
            lParametros.Add(new Parametro("@id_pais", oPelicula.pais.Id));
            lParametros.Add(new Parametro("@id_clasificacion", oPelicula.clasificacion.Id));
            lParametros.Add(new Parametro("@fecha_estreno", oPelicula.Fecha_Estreno));
            lParametros.Add(new Parametro("@duracion_min", oPelicula.duracion));
            lParametros.Add(new Parametro("@id_distibuidora", oPelicula.distribuidora.Id));
            lParametros.Add(new Parametro("@id_genero", oPelicula.genero.Id));
            lParametros.Add(new Parametro("@id_directores", oPelicula.director.Id));

            if (HelperDAO.getinstancia().UtilizarProcedimiento("SP_CargarPeliculas", lParametros) > 0)
                return true;
            else
                return false;

        }

        public bool Acciones(int id, int baja)
        {
            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id", id));
            lParametros.Add(new Parametro("@baja", baja));

            if (HelperDAO.getinstancia().UtilizarProcedimiento("SP_BAJA_PELICULAS", lParametros) > 0)
                return true;
            else
                return false;
        }

        public string Sentencia(DateTime desde, DateTime hasta, string genero)
        {
            string sentencia = "  select id_pelicula as 'ID', titulo_local 'Titulo',duracion_min 'Duracion',fecha_estreno 'Fecha de Estreno',p.id_pais, pais,p.id_director, director,p.id_distribuidora, distribuidora,p.id_calificacion, calificacion 'Calificacion',p.id_genero,genero 'Genero\r\n', baja, fecha_baja  \r\n from Peliculas p join Calificaciones c on p.id_calificacion = c.id_calificacion  \r\n join generos g on p.id_genero = g.id_genero  \r\n join Paises pa on p.id_pais = pa.id_pais  \r\n join distribuidoras d on p.id_distribuidora = d.id_distribuidora  \r\n join directores di on p.id_director = di.id_director " +
                "Where fecha_estreno between " + desde + " and " + hasta;

            if(genero != "Todos")
            {
                sentencia += " and genero = " + genero;
            }
            return sentencia;
        }

        public List<Pelicula> ObtenerPeliculas(string Sp)
        {
            List<Pelicula> lPeliculas = new List<Pelicula>();
            DataTable dt = HelperDAO.getinstancia().ConsultarDB(Sp);

            foreach (DataRow row in dt.Rows)
            {
                Pelicula p = new Pelicula();
                p.Id = Convert.ToInt32(row[0].ToString());
                p.Titulo_local = row[1].ToString();
                p.duracion = Convert.ToInt32(row[2].ToString());
                p.Fecha_Estreno = Convert.ToDateTime(row[3].ToString());
                p.pais.Id = Convert.ToInt32(row[4].ToString());
                p.pais.Nombre = row[5].ToString();
                p.director.Id = Convert.ToInt32(row[6].ToString());
                p.director.Nombre = row[7].ToString();
                p.distribuidora.Id = Convert.ToInt32(row[8].ToString());
                p.distribuidora.Nombre = row[9].ToString();
                p.clasificacion.Id = Convert.ToInt32(row[10].ToString());
                p.clasificacion.Nombre = row[11].ToString();
                p.genero.Id = Convert.ToInt32(row[12].ToString());
                p.genero.Nombre = row[13].ToString();
                p.Baja = Convert.ToInt32(row[14].ToString());
                lPeliculas.Add(p);
            }
            return lPeliculas;
        }

        public List<Pelicula> ConsultarPeliculasConSentencia()
        {
            List<Pelicula> lPeliculas = new List<Pelicula>();
            DataTable dt = HelperDAO.getinstancia().EjecutarSentencia(SentenciaPeliculas());

            foreach (DataRow row in dt.Rows)
            {
                Pelicula p = new Pelicula();
                p.Id = Convert.ToInt32(row[0].ToString());
                p.Titulo_local = row[1].ToString();
                p.duracion = Convert.ToInt32(row[2].ToString());
                p.Fecha_Estreno = Convert.ToDateTime(row[3].ToString());
                p.pais.Id = Convert.ToInt32(row[4].ToString());
                p.pais.Nombre = row[5].ToString();
                p.director.Id = Convert.ToInt32(row[6].ToString());
                p.director.Nombre = row[7].ToString();
                p.distribuidora.Id = Convert.ToInt32(row[8].ToString());
                p.distribuidora.Nombre = row[9].ToString();
                p.clasificacion.Id = Convert.ToInt32(row[10].ToString());
                p.clasificacion.Nombre = row[11].ToString();
                p.genero.Id = Convert.ToInt32(row[12].ToString());
                p.genero.Nombre = row[13].ToString();
                p.Baja = Convert.ToInt32(row[14].ToString());
                lPeliculas.Add(p);
            }
            return lPeliculas;
        }

        public string SentenciaPeliculas()
        {
            string Sentencia = " select id_pelicula as 'ID', titulo_local 'Titulo',duracion_min 'Duracion',fecha_estreno 'Fecha de Estreno'," +
                " p.id_pais, pais,p.id_director, director,p.id_distribuidora, distribuidora,p.id_calificacion," +
                " calificacion ,p.id_genero, genero, baja, fecha_baja " +
                " from Peliculas p join Calificaciones c on p.id_calificacion = c.id_calificacion " +
                " join generos g on p.id_genero = g.id_genero  \r\n join Paises pa on p.id_pais = pa.id_pais " +
                " join distribuidoras d on p.id_distribuidora = d.id_distribuidora " +
                "join directores di on p.id_director = di.id_director";

            return Sentencia;
        }

        public List<Distribuidora> CargarDistribuidora()
        {
            List<Distribuidora> distribuidoras = new List<Distribuidora>();
            DataTable dt = HelperDAO.getinstancia().ConsultarDB("SP_Distribuidora");

            foreach (DataRow row in dt.Rows)
            {
                Distribuidora dis = new Distribuidora();
                dis.Id = Convert.ToInt32(row[0].ToString());
                dis.Nombre = row[1].ToString();
                distribuidoras.Add(dis);
            }
            return distribuidoras;
        }

        public List<Clasificacion> ObtenerClasificaciones()
        {
            List<Clasificacion> lCla = new List<Clasificacion>();
            DataTable dt = HelperDAO.getinstancia().ConsultarDB("SP_Clasificacion");

            foreach (DataRow row in dt.Rows)
            {
                Clasificacion cla = new Clasificacion();
                cla.Id = Convert.ToInt32(row[0].ToString());
                cla.Nombre = row[1].ToString();
                lCla.Add(cla);
            }
            return lCla;
        }

        public List<Director> ObtenerDirectores()
        {
            List<Director> lDire = new List<Director>();
            DataTable dt = HelperDAO.getinstancia().ConsultarDB("SP_Directores");

            foreach (DataRow row in dt.Rows)
            {
                Director dire = new Director();
                dire.Id = Convert.ToInt32(row[0].ToString());
                dire.Nombre = row[1].ToString();
                lDire.Add(dire);
            }
            return lDire;
        }

        public List<Genero> ObtenerGeneros()
        {
            List<Genero> lGene = new List<Genero>();
            DataTable dt = HelperDAO.getinstancia().ConsultarDB("SP_Genero");

            foreach (DataRow row in dt.Rows)
            {
                Genero gene = new Genero();
                gene.Id = Convert.ToInt32(row[0].ToString());
                gene.Nombre = row[1].ToString();
                lGene.Add(gene);
            }
            return lGene;
        }

        public List<Pais> ObtenerPaises()
        {
            List<Pais> lPais= new List<Pais>();
            DataTable dt = HelperDAO.getinstancia().ConsultarDB("SP_Pais");

            foreach (DataRow row in dt.Rows)
            {
                Pais pais = new Pais();
                pais.Id = Convert.ToInt32(row[0].ToString());
                pais.Nombre = row[1].ToString();
                lPais.Add(pais);
            }
            return lPais;
        }

        public bool ModificarPelicula(Pelicula oPelicula)
        {
            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@titulo_local", oPelicula.Titulo_local));
            lParametros.Add(new Parametro("@descripcion", oPelicula.Descripcion));
            lParametros.Add(new Parametro("@id_pais", oPelicula.pais.Id));
            lParametros.Add(new Parametro("@id_calificacion", oPelicula.clasificacion.Id));
            lParametros.Add(new Parametro("@fecha_est", oPelicula.Fecha_Estreno));
            lParametros.Add(new Parametro("@duracion", oPelicula.duracion));
            lParametros.Add(new Parametro("@id_dist", oPelicula.distribuidora.Id));
            lParametros.Add(new Parametro("@id_genero", oPelicula.genero.Id));
            lParametros.Add(new Parametro("@id_director", oPelicula.director.Id));
            lParametros.Add(new Parametro("@idPel", oPelicula.Id));

            if (HelperDAO.getinstancia().UtilizarProcedimiento("SP_update_peliculas", lParametros) > 0)
                return true;
            else
                return false;
        }
    }
}
