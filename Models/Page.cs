namespace MedicalQueueApi.Models {
    public class Page
    {
        public int Id { get; set; }                 // Id страницы
        public string Title { get; set; }           // Заголовок страницы
        public DateTime DateCreate { get; set; }    // Дата создания страницы
        public int TimeDisplay { get; set; }        // Время отображения страницы

        public TypePage? TypePage { get; set; }      // Тип страницы
        public int TypePageId { get; set; }
        public Display? Display { get; set; }        // Монитор
        public int? DisplayId { get; set; }
    }
}
