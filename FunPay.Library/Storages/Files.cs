using System;
using System.IO;
using System.Text;

namespace FunPay.Library.Storages
{
    public class Files
    {
        public Files()
        {
            //Присваеваем путь
            AppData=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        public string AppData { get; set; }

        #region Работа с папками

        /// <summary>
        /// Проверка наличия нужной папки от пути AppData
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ExistFolder(string path)
        {
            string folder = Path.Combine(AppData, path);
            return Directory.Exists(folder);
        }
        /// <summary>
        /// Добавление нужной папки
        /// </summary>
        /// <param name="path"></param>
        public void AddFolder(string path)
        {
            string folder = Path.Combine(AppData, path);
            Directory.CreateDirectory(folder);
        }

        #endregion

        #region Работа с файлами

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="path">Путь от AppData</param>
        /// <param name="text">Текст</param>
        public void SaveFile(string name, string path, string text)
        {
            using (FileStream fstream = new FileStream(Path.Combine(AppData, path, name), FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }
        /// <summary>
        /// Чтение из файла
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="path">Путь от AppData</param>
        /// <returns></returns>
        public string ReadFile(string name, string path)
        {
            // чтение из файла
            using (FileStream fstream = File.OpenRead(Path.Combine(AppData, path, name)))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(array);
                return textFromFile;
            }
        }
        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ExistFile(string path)
        {
            var file = Path.Combine(AppData, path);
            return File.Exists(file);
        }

        #endregion
    }
}
