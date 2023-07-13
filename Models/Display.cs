namespace MedicalQueueApi.Models
{
    public class Display
    {
        public int Id { get; set; }
        public int ColorSchemeId { get; set; }
        public ColorScheme? ColorScheme { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}