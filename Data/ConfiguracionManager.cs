namespace WebApiTokenJWTRouting21122.Data
{
    public class ConfiguracionManager
    {
        //Instancia IConfiguration estatico
        public static IConfiguration AppSetting { get; }

        //Constructor estatico
        static ConfiguracionManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
