using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TrelloApi
{
    public class TrelloClient
    {
        private string token;
        private string apiKey;

        public TrelloClient(string token, string apiKey)
        {
            this.token = token;
            this.apiKey = apiKey;
        }

        internal string GetResponseByWebRequest(string url, string method,
            IEnumerable<(string title, string value)> headers = null, IEnumerable<(string title, string value)> parameters = null)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);

            if (headers != null)
                foreach (var (title, value) in headers)
                    request.Headers.Add(title, value);
            
            request.Method = method.ToUpper();
            request.Headers.Add("Authorization", $"OAuth oauth_consumer_key =\"{apiKey}\", oauth_token=\"{token}\"");
            if (!(parameters is null))
            {
                using var requestStream = request.GetRequestStream();
                var paramsString = string.Join("&", parameters.Select(x => $"{x.title}={x.value}"));
                var bytes = Encoding.UTF8.GetBytes(paramsString);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }
            var response = request.GetResponse();
            using var responseStream = response.GetResponseStream();
            if (responseStream is null)
                throw new NullReferenceException("Response stream is null");
            using var reader = new StreamReader(responseStream);
            var input = reader.ReadToEnd();
            response.Close();
            return input;
        }

        internal T DeserializeJson<T>(string json)
        {
            var builderJson = new StringBuilder();
            builderJson.Append(json[0].ToString() + json[1].ToString());
            for (var i = 2; i < json.Length; i++)
            {
                var now = json[i];
                if (json[i - 1] == '"' && (json[i - 2] == ',' || json[i - 2] == '{'))
                    now = char.ToUpper(now);
                builderJson.Append(now);
            }

            var settings = new JsonSerializerSettings();
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            return JsonConvert.DeserializeObject<T>(builderJson.ToString(), settings);
        }

        internal void Copy<T>(T fromObject, T toObject)
        {
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                prop.SetValue(toObject, prop.GetValue(fromObject));
            }
        }
    }
}