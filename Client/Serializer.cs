using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;


public enum SerializerFormat : byte
{
    XML,
    /// <summary>
    /// Бинарный
    /// </summary>
    Binary,
    /// <summary>
    /// Протокол SOAP (Simple Object Access Protocol) представляет простой протокол для обмена данными между различными платформами. При такой сериализации данные упакуются в конверт SOAP, данные в котором имеют вид xml-подобного документа
    /// </summary>
    //SOAP,
    JSON
}

public static class Serializer
{

    public static string BytesToString(this byte[] data, Encoding encoding)
    {
        if (encoding == null) { encoding = Encoding.Default; }
        return encoding.GetString(data);
    }

    static ConcurrentDictionary<string, object> fileList = new ConcurrentDictionary<string, object>();
    /// <summary>
    /// Добавляет в fileList адрес файла если он уникальный
    /// </summary>
    /// <param name="fileAddress">Адрес файла</param>
    static object addFile(string fileAddress)
    {
        FileInfo fileInf = new FileInfo(fileAddress);
        if (!fileList.ContainsKey(fileInf.FullName))
        {
            fileList[fileInf.FullName] = new object();
        }
        return fileList[fileInf.FullName];
    }

    /// <summary>
    /// Записать в фаил
    /// </summary>
    /// <param name="adress">Расположение файла</param>
    /// <param name="serializableСlass">Сериализуемый класс</param>
    /// <param name="serializerFormat">Формат сериализации</param>
    /// /// <param name="fileMode">Указывает, каким образом операционная система должна открыть файл.</param>
    /// <returns>true - запись прошла удачно</returns>
    public static void ClassToFile<T>(this T serializableСlass, string adress, SerializerFormat serializerFormat = SerializerFormat.XML, FileMode fileMode = FileMode.Create) where T : new()
    {
        lock (addFile(adress))
        {
            FileInfo fileInf = new FileInfo(adress);
            string vrem = fileInf.Directory + @"/_" + fileInf.Name;

            using (Stream fs = new FileStream(vrem, fileMode))//FileMode.OpenOrCreate - с ним какойто баг записывает в конце лишние символы
            {
                SerDeser(fs, serializerFormat, false, serializableСlass);
            }

            File.Copy(vrem, fileInf.Directory + @"/" + fileInf.Name, true);
            File.Delete(vrem);
        }
    }

    /// <summary>
    /// Считать фаил и преобразовать в Т класс
    /// </summary>
    /// <param name="fileAddress">Расположение файла</param>
    /// <param name="serializerFormat">Формат десериализации</param>
    /// <param name="fileMode">Указывает, каким образом операционная система должна открыть файл.</param>
    /// <returns>Возвращает десериализованный класс</returns>
    public static T FileToClass<T>(this T serializableСlass, string fileAddress, SerializerFormat serializerFormat = SerializerFormat.XML, FileMode fileMode = FileMode.Open) where T : new()
    {
        lock (addFile(fileAddress))
        {
            using (var fs = new FileStream(fileAddress, fileMode))
            {
                return SerDeser<T>(fs, serializerFormat);
            }
        }
    }

    /// <summary>
    /// Открывает двоичный файл, считывает содержимое файла в массив байтов и затем закрывает файл.
    /// </summary>
    /// <param name="fileAddress"></param>
    /// <returns></returns>
    public static byte[] FileToBytes(string fileAddress)
    {
        lock (addFile(fileAddress))
        {
            return File.ReadAllBytes(fileAddress);
        }
    }

    /// <summary>
    /// Получить с сайта
    /// </summary>
    /// <param name="webAddress"></param>
    /// <param name="serializerFormat"></param>
    /// <returns></returns>
    public static T WebToClass<T>(this T serializableСlass, string webAddress, SerializerFormat serializerFormat = SerializerFormat.XML)
    {
        byte[] response = null;
        using (WebClient client = new WebClient())
        {
            response = client.DownloadData(webAddress);
        }
        using (MemoryStream memory = new MemoryStream(response))
        {
            return SerDeser<T>(memory, serializerFormat);
        }
        /* Можно еще такой вариант делать
         var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + AdressSite + "/api/versions/latest/" + UniqueKey.Key());

                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "GET";//Можно POST
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = httpResponse.GetResponseStream())
                {
                    var Json = new DataContractJsonSerializer(typeof(Update));
                    update = (Update)Json.ReadObject(streamReader);
                }
         */
    }

    /// <summary>
    /// Преобразовать класс Т в байты
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serializableСlass"></param>
    /// <param name="serializerFormat"></param>
    /// <returns>Возвращает преобразованный клас T в виде массива байтов</returns>
    public static byte[] ClassToBytes<T>(this T serializableСlass, SerializerFormat serializerFormat = SerializerFormat.XML)
    {
        using (MemoryStream memory = new MemoryStream())
        {
            SerDeser(memory, serializerFormat, false, serializableСlass);
            return memory.ToArray();
        }
    }

    /// <summary>
    /// Преобразовать массив байтов в класс Т
    /// </summary>
    /// <typeparam name="T">лю</typeparam>
    /// <param name="data"></param>
    /// <param name="serializerFormat"></param>
    /// <returns></returns>
    public static T BytesToClass<T>(this byte[] data, SerializerFormat serializerFormat = SerializerFormat.XML)
    {
        using (MemoryStream memory = new MemoryStream(data))
        {
            return SerDeser<T>(memory, serializerFormat);
        }
    }

    /// <summary>
    /// Cериализация и десериализация
    /// </summary>
    /// <param name="stream">Куда будет производиться запись</param>
    /// <param name="serializerFormat">Формат сериализации и десериализации</param>
    /// <param name="deserialize">Cериализация = false, десериализация = true, по умолчанию true</param>
    /// <param name="serializableObject">Обьект для сериализации, по умолчанию default(T)</param>
    /// <returns></returns>
    static T SerDeser<T>(Stream stream, SerializerFormat serializerFormat, bool deserialize = true, T serializableObject = default(T))//default(T) возвращает значение null
    {
        T result = default(T);

        switch (serializerFormat)
        {
            case SerializerFormat.XML:
                var xml = new XmlSerializer(typeof(T));
                if (deserialize)
                {
                    result = (T)xml.Deserialize(stream);
                }
                else
                {
                    xml.Serialize(stream, serializableObject);
                }
                break;
            case SerializerFormat.Binary:
                var binary = new BinaryFormatter();
                if (deserialize)
                {
                    result = (T)binary.Deserialize(stream);
                }
                else
                {
                    binary.Serialize(stream, serializableObject);
                }
                break;
            /*case SerializerFormat.SOAP:
                var soap = new SoapFormatter();
                if (deserialize)
                {
                    result = (T)soap.Deserialize(stream);
                }
                else
                {
                    soap.Serialize(stream, serializableObject);
                }
                break;*/
            case SerializerFormat.JSON:
                var json = new DataContractJsonSerializer(typeof(T));
                if (deserialize)
                {
                    result = (T)json.ReadObject(stream);
                }
                else
                {
                    json.WriteObject(stream, serializableObject);
                }
                break;
        }

        return result;
    }
}