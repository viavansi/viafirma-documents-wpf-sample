using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViafirmaDocumentsWpfAppSample.Models
{
    public class MessageModel
    {
        public class Error
        {
            public int code { get; set; }
            public string type { get; set; }
            public string message { get; set; }
            public string trace { get; set; }
        }

        public class History
        {
            public string start { get; set; }
            public string ends { get; set; }
            public string taskName { get; set; }
            public int order { get; set; }
            public Error error { get; set; }
        }

        public class Workflow
        {
            public string code { get; set; }
            public string current { get; set; }
            public string next { get; set; }
            public List<History> history { get; set; }
            public string initiate { get; set; }
            public string lastUpdated { get; set; }
            public string expires { get; set; }
            public string type { get; set; }
        }

        public class SharedLink
        {
            public string scheme { get; set; }
            public string token { get; set; }
            public string link { get; set; }
            public string appCode { get; set; }
            public string email { get; set; }
            public string subject { get; set; }
        }

        public class Metadata
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class Device
        {
            public string appCode { get; set; }
            public string code { get; set; }
            public string description { get; set; }
            public string locale { get; set; }
            public string status { get; set; }
            public string token { get; set; }
            public string uniqueIdentifier { get; set; }
            public string type { get; set; }
            public string userEmail { get; set; }
            public string userCode { get; set; }
            public string userNationalId { get; set; }
        }

        public class Notification
        {
            public string code { get; set; }
            public string validateCode { get; set; }
            public string text { get; set; }
            public string detail { get; set; }
            public string sound { get; set; }
            public string status { get; set; }
            public string location { get; set; }
            public SharedLink sharedLink { get; set; }
            public List<Metadata> metadata { get; set; }
            public List<Device> devices { get; set; }
        }

        public class Item
        {
            public string key { get; set; }
            public string value { get; set; }
            public string type { get; set; }
            public string label { get; set; }
            public string placeHolder { get; set; }
            public string size { get; set; }
            public bool required { get; set; }
            public string validation { get; set; }
            public string validationRegex { get; set; }
            public string refValues { get; set; }
            public string list { get; set; }
            public string nestedList { get; set; }
            public string text { get; set; }
            public string href { get; set; }
            public string match { get; set; }
            public string update { get; set; }
            public bool disabled { get; set; }
            public bool hidden { get; set; }
            public bool random { get; set; }
            public List<string> values { get; set; }
            public string height { get; set; }
            public string format { get; set; }
            public string maxLength { get; set; }
            public string minLength { get; set; }
            public List<string> monthNames { get; set; }
            public List<string> dayNames { get; set; }
            public int increment { get; set; }
        }

        public class Font
        {
            public string name { get; set; }
            public int size { get; set; }
        }

        public class Document
        {
            public string templateCode { get; set; }
            public string templateReference { get; set; }
            public int templateVersion { get; set; }
            public string draftedCode { get; set; }
            public string draftedReference { get; set; }
            public string signedCode { get; set; }
            public string signedID { get; set; }
            public string signedReference { get; set; }
            public string templateType { get; set; }
            public bool formRequired { get; set; }
            public bool formDisabled { get; set; }
            public List<Item> items { get; set; }
            public bool pdfaCompliant { get; set; }
            public Font font { get; set; }
            public string policyCode { get; set; }
        }

        public class MetadataList
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class ParamList
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class Rectangle
        {
            public int x { get; set; }
            public int y { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Position
        {
            public Rectangle rectangle { get; set; }
            public int page { get; set; }
        }

        public class Geolocation
        {
            public int accuracy { get; set; }
            public int latitude { get; set; }
            public int longitude { get; set; }
            public string locationData { get; set; }
            public string locationInfo { get; set; }
        }

        public class Point
        {
            public int x { get; set; }
            public int y { get; set; }
            public int pressure { get; set; }
            public int milliseconds { get; set; }
        }

        public class Stroke
        {
            public List<Point> points { get; set; }
        }

        public class SignatureData
        {
            public string messageCode { get; set; }
            public string policyCode { get; set; }
            public string evidenceCode { get; set; }
            public string base64Image { get; set; }
            public Geolocation geolocation { get; set; }
            public Device device { get; set; }
            public List<Stroke> strokes { get; set; }
            public int signAreaHeight { get; set; }
            public int signAreaWidth { get; set; }
            public List<Position> positions { get; set; }
        }

        public class FingerPrintData
        {
            public string messageCode { get; set; }
            public string evidenceCode { get; set; }
            public string base64Image { get; set; }
            public string base64Template { get; set; }
            public string templateAlgorithmic { get; set; }
            public string fingerID { get; set; }
            public Geolocation geolocation { get; set; }
            public Device device { get; set; }
            public List<Position> positions { get; set; }
        }

        public class ImageData
        {
            public string messageCode { get; set; }
            public string evidenceCode { get; set; }
            public string base64Image { get; set; }
            public Geolocation geolocation { get; set; }
            public Device device { get; set; }
            public List<Position> positions { get; set; }
        }

        public class Evidence
        {
            public string type { get; set; }
            public string code { get; set; }
            public string status { get; set; }
            public string helpText { get; set; }
            public string temporalReference { get; set; }
            public List<Position> positions { get; set; }
            public string metadata { get; set; }
            public string deviceType { get; set; }
            public List<string> hashPdf { get; set; }
            public string hashImage { get; set; }
            public string algorithmic { get; set; }
            public string fingerID { get; set; }
            public string typeFormatSign { get; set; }
            public string certificateAlias { get; set; }
            public string certificatePassword { get; set; }
            public string metadataCipherPublicKey { get; set; }
            public string encryptionKeyAlias { get; set; }
            public bool optional { get; set; }
            public SignatureData signatureData { get; set; }
            public FingerPrintData fingerPrintData { get; set; }
            public ImageData imageData { get; set; }
            public string positionsKey { get; set; }
            public int stampsMin { get; set; }
            public string stampsPolicy { get; set; }
            public List<string> stylus { get; set; }
            public Geolocation geolocation { get; set; }
            public string wacomURL { get; set; }
        }

        public class Stamper
        {
            public string type { get; set; }
            public string rotation { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int xAxis { get; set; }
            public int yAxis { get; set; }
            public int page { get; set; }
            public string imageBase64 { get; set; }
        }

        public class Signature
        {
            public string type { get; set; }
            public string code { get; set; }
            public string status { get; set; }
            public string helpText { get; set; }
            public string certificateAlias { get; set; }
            public string certificatePassword { get; set; }
            public string typeFormatSign { get; set; }
            public string dataToSign { get; set; }
            public string idSign { get; set; }
            public bool custodyDisabled { get; set; }
            public List<Stamper> stampers { get; set; }
            public string certificationLevel { get; set; }
        }

        public class Checklist
        {
            public string status { get; set; }
            public string code { get; set; }
            public string helpText { get; set; }
            public Signature signature { get; set; }
            public string validateCode { get; set; }
            public string expires { get; set; }
            public string date { get; set; }
            public string commentReject { get; set; }
        }

        public class Policy
        {
            public string code { get; set; }
            public string userCode { get; set; }
            public string typeFormatSign { get; set; }
            public string typeSign { get; set; }
            public bool signByServer { get; set; }
            public string certificateAlias { get; set; }
            public string certificatePassword { get; set; }
            public string idTemporal { get; set; }
            public string idSign { get; set; }
            public List<ParamList> paramList { get; set; }
            public List<Evidence> evidences { get; set; }
            public List<Signature> signatures { get; set; }
            public Error error { get; set; }
            public List<Checklist> checklist { get; set; }
        }


        public string code { get; set; }
        public string userCode { get; set; }
        public string groupCode { get; set; }
        public string appCode { get; set; }
        public string version { get; set; }
        public Workflow workflow { get; set; }
        public Notification notification { get; set; }
        public Document document { get; set; }
        public List<MetadataList> metadataList { get; set; }
        public List<Policy> policies { get; set; }
        public string callbackURL { get; set; }
        public string callbackMails { get; set; }
        public List<string> callbackMailsFormKeys { get; set; }
        public Error error { get; set; }
        public string pid { get; set; }
        public string server { get; set; }
        public string processTimeExpired { get; set; }
        public string commentReject { get; set; }

    }
}
