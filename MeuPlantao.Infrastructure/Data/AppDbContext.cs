using MeuPlantao.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Infrastructure.Data;

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
    public DbSet<PlantaoHistoricoModel> HistoricoPlantao { get; set; }
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

        //Builder pro FK do plantao
        modelBuilder.Entity<PlantaoModel>()
            .HasOne(p => p.ProfissionalResponsavel)
            .WithMany()
            .HasForeignKey(p => p.ProfissionalResponsavelId)
            .IsRequired(false);
        modelBuilder.Entity<PlantaoModel>()
            .HasOne(p => p.Setor)
            .WithMany()
            .HasForeignKey(p => p.SetorId)
            .IsRequired(true);

        //Builder pro FK do setor 
        modelBuilder.Entity<SetorModel>()
            .HasOne(p => p.Representante)
            .WithMany()
            .HasForeignKey(p => p.RepresentanteId)
            .IsRequired(true);
        
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

        //Builder pras FK do historico de plantao
        // Plantão → vários históricos
        modelBuilder.Entity<PlantaoHistoricoModel>()
            .HasOne(p => p.Plantao)
            .WithMany()
            .HasForeignKey(p => p.PlantaoId);

        // Usuário → vários históricos
        modelBuilder.Entity<PlantaoHistoricoModel>()
            .HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.UsuarioId);
    }
}