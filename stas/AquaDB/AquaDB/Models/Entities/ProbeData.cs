using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AquaDB.Database.Entities
{
    public class ProbeData
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Value { get; set; } = 0;

        [ForeignKey("Measurement")]
        public int MeasurementId { get; set; }

        [ForeignKey("Probe")]
        public int ProbeId { get; set; }

        public Measurement Measurement { get; set; }
        public Probe Probe { get; set; }
    }
}
