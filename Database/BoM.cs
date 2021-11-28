using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstProject.Database
{
    [Table("BoM")]
    public partial class BoM
    {
        [Key]
        [StringLength(50)]
        public string PartId { get; set; }

        [StringLength(50)]
        public string ParentPartId { get; set; }    
        
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
        
        public int Type { get; set; }       
        public int Quantity { get; set; }              
        public int UnitOfMeasure { get; set; }      
        public int Group { get; set; }


        // public DateTime? Date { get; set; }
    }
}
