using MedicalQueueApi.Models;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using MedicalQueueApi.Data;
using MedicalQueueApi.Misc;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace MedicalQueueApi.Data
{
    public class DbInitializer {
        
        public static void Initialize(ApplicationContext context) {
            if ( context.Administrators.Any()) { return; }
            var scheme = AddColorScheme(context, 0, "CssFileProperty", "scheme", "Description", true);
            var type1 = AddTypePage(context, 0, "example_type", "Description");
            AddAdministartor(context,0, "Login", "Password", DateTime.MinValue);
            var display1 = AddDisplay(context, 0, scheme, "Display_1", "Description");
            AddPage(context, 0, "Title", DateTime.MinValue, 400, type1, 0, display1);
        }
        public static Administrator AddAdministartor(ApplicationContext db,int Id, string Login, string Password,DateTime LastAuth) {
            var entry = new Administrator()
            {
                Id = Id,
                Login = Login,
                Password = Password,
                LastAuth = LastAuth
            };
            db.Administrators.Add(entry);
            db.SaveChanges();
            return entry;
        }
        public static Monitoring AddMonitoring(ApplicationContext db, int Id, DateTime DateTimeEvent, bool TypeEvent, Display? Display)
        {
            var entry = new Monitoring()
            {
                Id = Id,
                DateTimeEvent = DateTimeEvent,
                TypeEvent = TypeEvent,
                Display = Display,
                DisplayId = Display?.Id ?? 0
            };
            db.MonitoringData.Add(entry);
            db.SaveChanges();
            return entry;
        }
        public static Display AddDisplay(ApplicationContext db, int Id, ColorScheme? ColorScheme, string Name, string Description) 
        {
            var entry = new Display()
            {
                Id = Id,
                ColorScheme = ColorScheme, 
                ColorSchemeId = ColorScheme?.Id ?? 0,
                Name=Name,
                Description=Description
            };
            db.Displays.Add(entry);
            db.SaveChanges();
            return entry;
        }
        public static Page AddPage(ApplicationContext db, int Id,string Title,DateTime DateCreate,int TimeDisplay, 
        TypePage? TypePage,int TypePageId, Display? Display) 
        {
            var entry = new Page()
            {
                Id= Id,
                Title = Title,
                DateCreate = DateCreate,
                TimeDisplay = TimeDisplay,
                TypePage = TypePage,
                TypePageId = TypePageId,
                DisplayId = Display?.Id,
            };
            db.Pages.Add(entry);
            db.SaveChanges();
            return entry;
        }
        public static ColorScheme AddColorScheme(ApplicationContext db, int Id, string CssFileProperty, string Name, string Description, bool IsActive)
        {
            var entry = new ColorScheme()
            {
                Id = Id,
                CssFileProperty = CssFileProperty,
                Name = Name,
                Description = Description,
                IsActive = IsActive
            };
            db.ColorSchemes.Add(entry);
            db.SaveChanges();
            return entry;
        }
        public static TypePage AddTypePage(ApplicationContext db, int Id, string Name, string Description)
        {
            var entry = new TypePage()
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };
            db.TypePages.Add(entry);
            db.SaveChanges();
            return entry;
        }
    }
}
