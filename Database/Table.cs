namespace MyFirstProject.Database
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Table")]
    public partial class DbTable
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CadEngineer { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string PDF { get; set; }
    }
}
