namespace MedicalQueueApi.Models
{
    public class ColorScheme
    {
        public int Id { get; set; }
        public string CssFileProperty { get; set; } // путь к файлу со стилями
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } // признак доступности схемы 
    }
}
