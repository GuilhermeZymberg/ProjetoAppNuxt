namespace API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
    public class EFDBContext : DbContext
    {
        public DbSet<Artista> Artista { get; set; }

        public EFDBContext(DbContextOptions<EFDBContext> options)
        : base(options)
        {

        }
                
        
    }

