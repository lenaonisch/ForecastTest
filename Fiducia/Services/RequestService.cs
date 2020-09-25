using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Fiducia.Services
{
    public class RequestService
    {
        public string GetStringResponse(string URL)
        {
            WebRequest request = WebRequest.Create(URL);
            WebResponse response = request.GetResponse();
            string responseFromServer;

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            response.Close();

            return responseFromServer;
        }

        public T GetData<T>(string response)
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}