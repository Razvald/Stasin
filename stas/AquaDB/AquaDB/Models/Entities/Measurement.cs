using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AquaDB.Database.Entities
{
    public class Measurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Location { get; set; } = string.Empty;
        public int Depth { get; set; } = 0;

        [ForeignKey("MeasurementType")]
        public int MeasurementTypeId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        
        public virtual Project Project { get; set; }
        public virtual MeasurementType MeasurementType { get; set; }
    }
}
