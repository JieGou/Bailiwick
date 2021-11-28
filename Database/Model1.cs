namespace MyFirstProject.Database
{
    using System.Data.Entity;
    using System.Reflection;
    using System.IO;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base(Conn())
        {
        }

        private static string Conn()
        {
            string result = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string ConnStr = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=" +
                  result +
                  "\\CadDatabase.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            
            return ConnStr;
        }

        public virtual DbSet<MyFirstProject.Database.DbTable> Tables { get; set; }

        public virtual DbSet<MyFirstProject.Database.BoM> BoM { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyFirstProject.Database.DbTable>()
                .Property(e => e.CadEngineer)
                .IsUnicode(false);

            modelBuilder.Entity<MyFirstProject.Database.DbTable>()
                .Property(e => e.PDF)
                .IsUnicode(false);
        }



    }
}
