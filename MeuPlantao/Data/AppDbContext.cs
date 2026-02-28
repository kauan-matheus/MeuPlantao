using MeuPlantao.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<PlantaoModel> Plantoes { get; set; }
    public DbSet<ProfissionalModel> Profissionais { get; set; }
    public DbSet<SetorModel> Setores { get; set; }
    public DbSet<TrocaHistoricoModel> Historico { get; set; }
    public DbSet<TrocaPlantaoModel> TrocaPlantoes { get; set; }
    public DbSet<UserModel> Usuarios { get; set; }

    //OnModelCreating pra definir algumas coisas que são conveção pro EF
    //HasOne -> Profissional tem um user 1-1
    //WithOne -> User tem um profissional
    //HasForeignKey -> a FK ta em profissional
    //WithMany -> Um profissional pode participar de várias trocas. 1-N
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfissionalModel>()
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<ProfissionalModel>(p => p.UserId);
        
        //Builder pras FK de troca plantao
        modelBuilder.Entity<TrocaPlantaoModel>()
            .HasOne(t => t.Solicitante)
            .WithMany()
            .HasForeignKey(t => t.SolicitanteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TrocaPlantaoModel>()
            .HasOne(t => t.Destinatario)
            .WithMany()
            .HasForeignKey(t => t.DestinatarioId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<TrocaHistoricoModel>()
            .HasOne(h => h.TrocaPlantao)
            .WithMany()
            .HasForeignKey(h => h.TrocaPlantaoId);

        modelBuilder.Entity<TrocaHistoricoModel>()
            .HasOne(h => h.Usuario)
            .WithMany()
            .HasForeignKey(h => h.UsuarioId);
    }
}