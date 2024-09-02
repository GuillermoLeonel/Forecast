using Forecast_Master.Models.Forecast.Models;
using Forecast_Master.Models;
using Microsoft.EntityFrameworkCore;

public class ForecastContext : DbContext
{
    public ForecastContext(DbContextOptions<ForecastContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    // Define el DbSet para ReportConfiguration
    public DbSet<ReportConfiguration> ReportConfigurations { get; set; }

    // Nuevas entidades para el módulo de reportes
    public DbSet<Report> Reports { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Provider> Providers { get; set; }

    // Nuevas entidades para el módulo de parámetros
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<Folio> Folios { get; set; }
    public DbSet<Printer> Printers { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Permissions)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        // Configuración de Role
        modelBuilder.Entity<Role>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Role>()
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);

        // Configuración de Permission
        modelBuilder.Entity<Permission>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Permission>()
            .HasOne(p => p.User)
            .WithMany(u => u.Permissions)
            .HasForeignKey(p => p.UserId);

        // Configuración de UserRole
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => ur.Id);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        // Configuración de Report
        modelBuilder.Entity<Report>()
            .HasKey(r => r.Id);

        // Configuración de Sale
        modelBuilder.Entity<Sale>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Producto)
            .WithMany(p => p.Sales)
            .HasForeignKey(s => s.ProductoId);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Cliente)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.ClienteId);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Vendedor)
            .WithMany(v => v.Sales)
            .HasForeignKey(s => s.VendedorId);

        // Configuración de Purchase
        modelBuilder.Entity<Purchase>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Producto)
            .WithMany(pr => pr.Purchases)
            .HasForeignKey(p => p.ProductoId);

        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.Proveedor)
            .WithMany(pr => pr.Purchases)
            .HasForeignKey(p => p.ProveedorId);

        // Configuración de Product
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Sales)
            .WithOne(s => s.Producto)
            .HasForeignKey(s => s.ProductoId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Purchases)
            .WithOne(pr => pr.Producto)
            .HasForeignKey(pr => pr.ProductoId);

        // Configuración de Client
        modelBuilder.Entity<Client>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Sales)
            .WithOne(s => s.Cliente)
            .HasForeignKey(s => s.ClienteId);

        // Configuración de Vendor
        modelBuilder.Entity<Vendor>()
            .HasKey(v => v.Id);

        modelBuilder.Entity<Vendor>()
            .HasMany(v => v.Sales)
            .WithOne(s => s.Vendedor)
            .HasForeignKey(s => s.VendedorId);

        // Configuración de Provider
        modelBuilder.Entity<Provider>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Provider>()
            .HasMany(p => p.Purchases)
            .WithOne(pr => pr.Proveedor)
            .HasForeignKey(pr => pr.ProveedorId);

        // Configuración de Parameter
        modelBuilder.Entity<Parameter>()
            .HasKey(p => p.SucursalId);

        modelBuilder.Entity<Parameter>()
            .HasIndex(p => p.SucursalId);

        modelBuilder.Entity<Sucursal>()
            .HasKey(s => s.Id);

        // Configuración de Parameter
        modelBuilder.Entity<Parameter>()
            .HasKey(p => p.SucursalId); 

        // Configuración de Folio
        modelBuilder.Entity<Folio>()
            .HasKey(f => f.Id);

        // Configuración de Printer
        modelBuilder.Entity<Printer>()
            .HasKey(p => p.PrintersId);

        // Configuración de Point
        modelBuilder.Entity<Point>()
            .HasKey(p => p.PointsId);
    }
}
