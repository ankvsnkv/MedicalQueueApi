namespace MedicalQueueApi.Models
{
    public class Display
    {
        public int Id { get; set; }
        public int СolorSchemeId { get; set; }
        public ColorScheme? СolorScheme { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}