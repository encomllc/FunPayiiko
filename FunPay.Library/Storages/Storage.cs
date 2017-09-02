namespace FunPay.Library.Storages
{
    public class Storage
    {

        public Files Files { get; set; } = new Files();
        /// <summary>
        /// Проверка наличия файла с настройками
        /// </summary>
        /// <returns></returns>
        public bool ExistSettings()
        {
            return Files.ExistFile("FunPay/settings.json");
        }
        /// <summary>
        /// Добавление файла с настройками
        /// </summary>
        /// <param name="settings"></param>
        public void AddSettings(string settings)
        {
            Files.SaveFile("settings.json","FunPay",settings);
        }
        /// <summary>
        /// Получить натсройки из файла
        /// </summary>
        /// <returns></returns>
        public string GetSettings()
        {
            return Files.ReadFile("settings.json", "FunPay");
        }
        
    }
}
