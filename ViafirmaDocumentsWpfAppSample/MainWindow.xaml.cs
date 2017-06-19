using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using ViafirmaDocumentsWpfAppSample.Models;
using static ViafirmaDocumentsWpfAppSample.Models.MessageModel;

namespace ViafirmaDocumentsWpfAppSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Datos de configuración
        private string OAuthConsumerKey = "xxxxx";
        private string OAuthConsumerSecret = "xxxxx";
        private string userCode = "xxxxx";
        private string deviceCode = "xxxxx";
        private string appCodeDevice = "xxxxx";
        private string messageCode;
        private string urlApiBackend = "xxxxx";
        private string policyCode = "xxxxx";

        private MessageModel message;

        public MainWindow()
        {
            InitializeComponent();
        }

        //
        // Summary:
        //     Envía el documento pdf-sample.pdf de la carpeta Assets a viafirma documents.
        //
        private async void ButtonSendPdf_Click(object sender, RoutedEventArgs e)
        {
            ComposeJsonToSendPdf();
            string json = JsonConvert.SerializeObject(message);

            //Call to service
            var httpResponseMessage = await Post(json, "messages");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                messageCode = httpResponseMessage.Content.ReadAsStringAsync().Result;
                ButtonStatus.IsEnabled = true;
            }
        }

        //
        // Summary:
        //     Comprueba el estado del documento.
        //
        private async void ButtonStatus_Click(object sender, RoutedEventArgs e)
        {
            //Call to service
            var httpResponseMessage = await Get("messages/status/" + messageCode);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                DocumentStatus status = JsonConvert.DeserializeObject<DocumentStatus>(result);
                MessageBox.Show(result);

                if(status.status.Equals("RESPONSED"))
                    ButtonGetSignedPdf.IsEnabled = true;
            }
        }

        //
        // Summary:
        //     Recupera el documento firmado por el usuario. 
        //     Este botón únicamente será habilitado si cuando se compruebe el estado con el botón "Consultar estado" es RESPONSED.
        //
        private async void ButtonGetSignedPdf_Click(object sender, RoutedEventArgs e)
        {
            //Call to service
            var httpResponseMessage = await Get("documents/download/signed/" + messageCode);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                MessageBox.Show(result);
                DocumentDownload download = JsonConvert.DeserializeObject<DocumentDownload>(result);

                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFileAsync(new Uri(download.link), download.fileName);
                }

            }
        }

        //
        // Summary:
        //     Modelado del json utilizado en la llamada al servicio
        //
        private void ComposeJsonToSendPdf()
        {
            message = new MessageModel();

            message.notification = new Notification();
            message.notification.text = "Envío desde c#";
            message.notification.detail = "Ejemplo de envío de un documento pdf desde una aplicación cliente con tecnología .net";

            message.notification.devices = new List<Device>();
            Device device = new Device();
            device.appCode = appCodeDevice;
            device.code = deviceCode;
            device.userCode = userCode;
            message.notification.devices.Add(device);

            message.document = new Document();
            message.document.templateReference = ConvertPdfToBase64();
            message.document.templateType = "base64";
            message.document.policyCode = policyCode;
        }

        //
        // Summary:
        //     Conversión del documento pdf-sample.pdf a Base64
        //
        private string ConvertPdfToBase64()
        {
            string fileBase64 = null;
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Assets\\pdf-sample.pdf";

            //Read file and convert to base64
            Byte[] bytes = System.IO.File.ReadAllBytes(path);
            fileBase64 = Convert.ToBase64String(bytes);

            return fileBase64;
        }

        //
        // Summary:
        //     Método general de llamada a un servicio Post
        //
        // Parameters:
        //   json:
        //     Json a enviar.
        //
        //   service:
        //     Servicio al que se desea conectar.
        //
        private async Task<HttpResponseMessage> Post(string json, string service)
        {
            string urlApi = urlApiBackend + service;

            OAuthUtil oAuthUtil = new OAuthUtil();
            string nonce = oAuthUtil.GetNonce();
            string timeStamp = oAuthUtil.GetTimeStamp();

            string SigBaseStringParams = "oauth_consumer_key=" + OAuthConsumerKey;
            SigBaseStringParams += "&" + "oauth_nonce=" + nonce;
            SigBaseStringParams += "&" + "oauth_signature_method=HMAC-SHA1";
            SigBaseStringParams += "&" + "oauth_timestamp=" + timeStamp;
            //SigBaseStringParams += "&" + "oauth_token=" + oauth_token;
            SigBaseStringParams += "&" + "oauth_version=1.0";
            string SigBaseString = "POST&";
            SigBaseString += Uri.EscapeDataString(urlApi) + "&" + Uri.EscapeDataString(SigBaseStringParams);
            SigBaseString = SigBaseString.Replace(" ", "");

            String Signature = oAuthUtil.ComputeSignature(SigBaseString, OAuthConsumerSecret);

            //HttpContent httpContent = new HttpContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            string authorizationHeaderParams = "oauth_consumer_key=\"" + OAuthConsumerKey + "\", oauth_nonce=\"" + nonce
                + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + Uri.EscapeDataString(Signature) + "\", oauth_timestamp=\"" + timeStamp + "\",  oauth_version=\"1.0\"";

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authorizationHeaderParams);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpResponseMessage = await httpClient.PostAsync(urlApi, stringContent);

            return httpResponseMessage;
        }

        //
        // Summary:
        //     Método general de llamada a un servicio Get
        //
        // Parameters:
        //   json:
        //     Json a enviar.
        //
        public async Task<HttpResponseMessage> Get(string service)
        {
            HttpResponseMessage httpResponseMessage = null;

            OAuthUtil oAuthUtil = new OAuthUtil();

            string nonce = oAuthUtil.GetNonce();
            string timeStamp = oAuthUtil.GetTimeStamp();
            string urlApi = urlApiBackend + service;

            string SigBaseStringParams = "oauth_consumer_key=" + OAuthConsumerKey;
            SigBaseStringParams += "&" + "oauth_nonce=" + nonce;
            SigBaseStringParams += "&" + "oauth_signature_method=HMAC-SHA1";
            SigBaseStringParams += "&" + "oauth_timestamp=" + timeStamp;
            //SigBaseStringParams += "&" + "oauth_token=" + oauth_token;
            SigBaseStringParams += "&" + "oauth_version=1.0";
            string SigBaseString = "GET&";
            SigBaseString += Uri.EscapeDataString(urlApi) + "&" + Uri.EscapeDataString(SigBaseStringParams);
            SigBaseString = SigBaseString.Replace(" ", "");

            String Signature = oAuthUtil.ComputeSignature(SigBaseString, OAuthConsumerSecret);

            string authorizationHeaderParams = "oauth_consumer_key=\"" + OAuthConsumerKey + "\", oauth_nonce=\"" + nonce
                 + "\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + Uri.EscapeDataString(Signature) + "\", oauth_timestamp=\"" + timeStamp + "\",  oauth_version=\"1.0\"";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authorizationHeaderParams);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(urlApi));

            request.Headers.IfModifiedSince = new DateTimeOffset(DateTime.Now);

            httpResponseMessage = await client.SendAsync(request);


            return httpResponseMessage;
        }
    }

    public class DocumentDownload
    {
        public string link { get; set; }
        public string md5 { get; set; }
        public string fileName { get; set; }
        public long expires { get; set; }
    }

    public class DocumentStatus
    {
        public string status { get; set; }
        public string lastUpdate { get; set; }
    }
}
