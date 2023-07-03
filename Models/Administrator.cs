namespace MedicalQueueApi.Models {
    public class Administrator {

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime LastAuth { get; set; }
    }
}
