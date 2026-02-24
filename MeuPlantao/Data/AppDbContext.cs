using MeuPlantao.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Data;

public class AppDbContext : DbContext
{
    public DbSet<PlantaoModel> Plantoes { get; set; }
    public DbSet<ProfissionalModel> Profissionais { get; set; }
    public DbSet<SetorModel> Setores { get; set; }
    public DbSet<TrocaHistoricoModel> Historico { get; set; }
    public DbSet<TrocaPlantaoModel> TrocaPlantoes { get; set; }
    public DbSet<UserModel> Usuarios { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    //OnModelCreating pra definir algumas coisas que são conveção pro EF
    //HasOne -> Profissional tem um user
    //WithOne -> User tem um profissional
    //HasForeignKey -> a FK ta em profissional
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfissionalModel>()
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<ProfissionalModel>(p => p.UserId);
    }
}