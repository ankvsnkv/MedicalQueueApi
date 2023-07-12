namespace MedicalQueueApi.Models
{
    public class Monitoring
    {
        public int Id { get; set; }                     // Идентификатор
        public DateTime dateTimeEvent { get; set; }     // Дата времени регистрации события
        public bool typeEvent { get; set; }             // Тип события (успешно/не успешно)

        public Device? Device { get; set; }             // Ссылка на монитор
        public int DeviceId { get; set; }
    }
}
