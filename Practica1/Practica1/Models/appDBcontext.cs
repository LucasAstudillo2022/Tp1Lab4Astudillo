using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Practica1.Models
{
    public class appDBcontext : DbContext
    {
        public appDBcontext(DbContextOptions<appDBcontext> options)
         : base(options)
        {
        }


        public DbSet<Libro> libros { get; set; }
        public DbSet<Genero> generos { get; set; }
        public DbSet<Autor> autores { get; set; }

    }

}

