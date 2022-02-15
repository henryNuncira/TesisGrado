using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiCCCactualizado.Models
{
    public partial class CCCVEntasContext : DbContext
    {
        public CCCVEntasContext()
        {
        }

        public CCCVEntasContext(DbContextOptions<CCCVEntasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetalleIngreso> DetalleIngresos { get; set; }
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }
        public virtual DbSet<Ingreso> Ingresos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Vendedor> Vendedors { get; set; }
        public virtual DbSet<Ventum> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CCCVEntas;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(50)
                    .HasColumnName("correoElectronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("nit");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(50)
                    .HasColumnName("nombreCompleto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<DetalleIngreso>(entity =>
            {
                entity.HasKey(e => e.IdDetalleIngreso);

                entity.ToTable("DetalleIngreso");

                entity.Property(e => e.IdDetalleIngreso).HasColumnName("idDetalleIngreso");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdIngreso).HasColumnName("idIngreso");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(11, 2)")
                    .HasColumnName("precio");

                modelBuilder.Entity<DetalleIngreso>()
                 .HasOne<Ingreso>(d => d.IdIngresoNavigation)
                 .WithMany(p => p.DetalleIngresos)
                 .HasForeignKey(d => d.IdIngreso)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<DetalleIngreso>()
                 .HasOne<Producto>(d => d.IdProductoNavigation)
                 .WithMany(p => p.DetalleIngresos)
                 .HasForeignKey(d => d.IdProducto)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta);

                entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(11, 2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(11, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleVe__idPro__49C3F6B7");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__idVen__4AB81AF0");
            });

            modelBuilder.Entity<Ingreso>(entity =>
            {
                entity.HasKey(e => e.IdIngreso);

                entity.ToTable("Ingreso");

                entity.Property(e => e.IdIngreso).HasColumnName("idIngreso");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.Impuesto)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("impuesto");

                entity.Property(e => e.NumeroComprobante)
                    .HasMaxLength(10)
                    .HasColumnName("numeroComprobante")
                    .IsFixedLength(true);

                entity.Property(e => e.TipoComprobante)
                    .HasMaxLength(50)
                    .HasColumnName("tipoComprobante");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(11, 2)")
                    .HasColumnName("total");

                modelBuilder.Entity<Ingreso>()
                 .HasOne<Proveedor>(d => d.IdProveedorNavigation)
                 .WithMany(p => p.Ingresos)
                 .HasForeignKey(d => d.IdProveedor)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Descricion)
                    .HasMaxLength(50)
                    .HasColumnName("descricion");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.NitCodigo)
                    .HasMaxLength(50)
                    .HasColumnName("nitCodigo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.PrecioVenta).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Stock).HasColumnName("stock");

                modelBuilder.Entity<Producto>()
                 .HasOne<Categorium>(d => d.IdCategoriaNavigation)
                 .WithMany(p => p.Productos)
                 .HasForeignKey(d => d.IdCategoria)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.ToTable("Proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(50)
                    .HasColumnName("correoElectronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.NitProveedor)
                    .HasMaxLength(50)
                    .HasColumnName("nitProveedor");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(50)
                    .HasColumnName("nombreCompleto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .HasColumnName("nombreRol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .HasColumnName("nombreUsuario");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Sal)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("sal");

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("token");

                modelBuilder.Entity<Usuario>()
                    .HasOne<Rol>(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.IdVendedor);

                entity.ToTable("Vendedor");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(50)
                    .HasColumnName("correoElectronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .HasColumnName("nit");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(50)
                    .HasColumnName("nombreCompleto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta);

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");

                entity.Property(e => e.Impuesto)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("impuesto");

                entity.Property(e => e.NumeroComprobante)
                    .HasMaxLength(50)
                    .HasColumnName("numeroComprobante");

                entity.Property(e => e.TipoComprobante)
                    .HasMaxLength(50)
                    .HasColumnName("tipoComprobante");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(11, 2)")
                    .HasColumnName("total");

                modelBuilder.Entity<Ventum>()
                    .HasOne<Cliente>(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Ventum>()
                     .HasOne<Vendedor>(d => d.IdVendedorNavigation)
                     .WithMany(p => p.Venta)
                     .HasForeignKey(d => d.IdVendedor);
            });

           // OnModelCreatingPartial(modelBuilder);
        }

      //  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
