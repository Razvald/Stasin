namespace AquaDB.Models
{
    public class MeasurementM
    {
        public int MeasurementId { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int MeasurementTypeId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Depth { get; set; }
        public string LocationDescription { get; set; }
        public string ProjectTitle { get; set; }
        public string MeasurementTypeName { get; set; }
    }
}
