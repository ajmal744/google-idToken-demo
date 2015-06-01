using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Google.IdToken.Core
{
    public class ExtractTokenInfoByUrl : ITokenInfoExtractor
    {
        public string Extract(string idToken)
        {
            if (string.IsNullOrEmpty(idToken))
            {
                throw new ArgumentNullException("idToken", "Id Token can not be null or string.");
            }
            
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder(new Uri("https://www.googleapis.com/oauth2/v1/tokeninfo"))
                {
                    Query = "id_token=" + idToken
                };

                var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri)).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    JObject jsonObject = null;
                    jsonObject = GetJsonObject(response);
                    return jsonObject.ToString();
                }
            }

            return "Token Invalid";
        }

        private static dynamic GetJsonObject(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = (JObject)JsonConvert.DeserializeObject(content);
            return jsonObject;
        }
    }
}
