using Poetizando.Database.Framework.Interface;
using Poetizando.Entidade;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Poetizando.Database
{
    public class PoetizandoContext : DbContext, IUnitOfWork
    {
        public DbSet<Frase> Frases { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Texto> Textos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Estatistica> Estatisticas { get; set; }

        public void Save()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Frase>().
              HasMany(c => c.Tags).
              WithMany(p => p.Frases).
              Map(
               m =>
               {
                   m.MapLeftKey("Frase_Id");
                   m.MapRightKey("Tag_Id");
                   m.ToTable("tagfrase");
               });
        }
    }
}
