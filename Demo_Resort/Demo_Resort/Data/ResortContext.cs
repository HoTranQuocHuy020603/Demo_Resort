using Demo_Resort.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;


namespace Demo_Resort.Data
{
    public class ResortContext : DbContext
    {
        // setup constructor with binding configuration (such as: Connection string)
        public ResortContext(DbContextOptions<ResortContext> options)
            : base(options)
        {
        }

        // Model
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public object Connection { get; internal set; }

        // mapping configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            // Account
            modelBuilder.Entity<Account>(builder =>
            {
                builder.ToTable("Accounts"); //name table

                builder.HasKey(x => x.id); //have primary key

                builder.Property(x => x.id)
                .ValueGeneratedOnAdd() //auto increment
                .IsRequired(true); // not null

                builder.Property(x => x.username)
                .HasMaxLength(255) //varchar(255)
                .IsRequired(true);

                builder.Property(x => x.password)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.isAdmin)
                .IsRequired(true);

                builder.Property(x => x.isEmployee)
                .IsRequired(true);
            });

            // employee
            modelBuilder.Entity<Employee>(builder =>
            {
                builder.ToTable("Employees");

                builder.HasKey(x => x.id);

                builder.Property(x => x.id)
                .ValueGeneratedOnAdd()
                .IsRequired(true);

                builder.Property(x => x.firstname)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.lastname)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.email)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.phonenumber)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.gender)
                .IsRequired(true);

                builder.Property(x => x.position)
                .IsRequired(true);
            });

            // customer
            modelBuilder.Entity<Customer>(builder =>
            {
                builder.ToTable("Customers");

                builder.HasKey(x => x.id);

                builder.Property(x => x.id)
                .ValueGeneratedOnAdd()
                .IsRequired(true);

                builder.Property(x => x.firstname)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.lastname)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.email)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.phonenumber)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.gender)
                .IsRequired(true);

            });

            // contract
            modelBuilder.Entity<Contract>(builder =>
            {
                builder.ToTable("Contracts");

                builder.HasKey(x => x.cid);

                builder.Property(x => x.cid)
                .ValueGeneratedOnAdd()
                .IsRequired(true);

                // foreign key with id of employee
                builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(x => x.id);

                builder.Property(x => x.falname)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.email)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.phonenumber)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.gender)
                .IsRequired(true);

                builder.Property(x => x.arrivaldate)
                .IsRequired(true);

                builder.Property(x => x.departuredate)
                .IsRequired(true);

                builder.Property(x => x.roomtype)
                .IsRequired(true);

                builder.Property(x => x.totalprice)
                .IsRequired(true);

                builder.Property(x => x.caterogy);
            });
        }
    }

    // setup design context factory
    public class ResortContextFactory : IDesignTimeDbContextFactory<ResortContext>
    {
        public ResortContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ResortContext>();
            optionBuilder.UseMySQL("Server=localhost;Database=DemoResort;Uid=root;Pwd= ;");

            return new ResortContext(optionBuilder.Options);
        }
    }
    public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkMySQL();
            new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                .TryAddCoreServices();
        }
    }
}
