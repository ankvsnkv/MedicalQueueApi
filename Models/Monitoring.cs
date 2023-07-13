namespace MedicalQueueApi.Models
{
    public class Monitoring
    {
        public int Id { get; set; }                     // Идентификатор
        public DateTime DateTimeEvent { get; set; }     // Дата времени регистрации события
        public bool TypeEvent { get; set; }             // Тип события (успешно/не успешно)

        public Display? Display { get; set; }             // Ссылка на монитор
        public int DisplayId { get; set; }
    }
}
