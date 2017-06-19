using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace ViafirmaDocumentsWpfAppSample.Utils
{
    public class OAuthUtil
    {
        public string GetNonce()
        {
            Random rand = new Random();
            int nonce = rand.Next(1000000000);
            return nonce.ToString();
        }

        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public async Task<string> PostData(string url, string postData)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = int.MaxValue;
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            HttpRequestMessage requestMsg = new HttpRequestMessage();
            requestMsg.Content = new StringContent(postData);
            requestMsg.Method = new HttpMethod("POST");
            requestMsg.RequestUri = new Uri(url, UriKind.Absolute);
            requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await httpClient.SendAsync(requestMsg);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetData(string url, string getData)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = int.MaxValue;
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            HttpRequestMessage requestMsg = new HttpRequestMessage();
            requestMsg.Content = new StringContent(getData);
            requestMsg.Method = new HttpMethod("GET");
            requestMsg.RequestUri = new Uri(url, UriKind.Absolute);
            requestMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            try
            {
                var response = await httpClient.SendAsync(requestMsg);
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        public string ComputeSignature(string sigBaseString, string consumerSecretKey, string requestTokenSecretKey = null)
        {
            var signingKey = string.Format("{0}&{1}", consumerSecretKey, !string.IsNullOrEmpty(requestTokenSecretKey) ? requestTokenSecretKey : "");
            IBuffer keyMaterial = CryptographicBuffer.ConvertStringToBinary(signingKey, BinaryStringEncoding.Utf8);
            MacAlgorithmProvider hmacSha1Provider = MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");
            CryptographicKey macKey = hmacSha1Provider.CreateKey(keyMaterial);
            IBuffer dataToBeSigned = CryptographicBuffer.ConvertStringToBinary(sigBaseString, BinaryStringEncoding.Utf8);
            IBuffer signatureBuffer = CryptographicEngine.Sign(macKey, dataToBeSigned);
            String signature = CryptographicBuffer.EncodeToBase64String(signatureBuffer);
            return signature;
        }
    }
}