using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using NLog;
namespace nKidReader
{
    class ServiceHandle
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string encryptionKey = "YOURSECRETKEY";
        public string accessToken { get; set; }
        RestClient client;
        public ServiceHandle()
        {
            this.client = new RestClient(ConfigurationManager.AppSettings["restUrl"]);
            this.accessToken = getAccessToken();
        }

        private string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString.Replace(' ', '+'));

            if (useHashing)
            {
                // If hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encryptionKey));
                hashmd5.Clear();
            }
            else
            {
                // If hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(encryptionKey);
            }

            // Set the secret key for the tripleDES algorithm
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            // Return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        private string getAccessToken()
        {
            try
            {
                var request =
                    new RestRequest("api/oauth/token",
                        Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(new
                {
                    grant_type = ConfigurationManager.AppSettings["grant_type"],
                    client_id = ConfigurationManager.AppSettings["client_id"],
                    client_secret = Decrypt(ConfigurationManager.AppSettings["client_secret"], true),
                    name = ConfigurationManager.AppSettings["name"],
                    scope = ConfigurationManager.AppSettings["scope"]
                });
                request.AddHeader("Content-type", "application/json; charset=utf-8");
                RestResponse response = (RestResponse)client.Execute(request);

                if (response != null &&
                    ((response.StatusCode == HttpStatusCode.OK) &&
                    (response.ResponseStatus == ResponseStatus.Completed)))
                {
                    JObject obj = JObject.Parse(response.Content);
                    logger.Info("Domain: " + ConfigurationManager.AppSettings["restUrl"] + ". Get token done");
                    return (string)obj["access_token"];
                    

                }
                else
                {
                    logger.Info("Get token failed");
                    MessageBox.Show("Cannot get token");

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public string uploadNFCCode(string action, string magneticCardID, string nfcID)
        {
            if ("OK".Equals(action))
            {
                try
                {
                    var request = new RestRequest("api/cards/" + magneticCardID,
                                Method.PUT);
                    request.RequestFormat = DataFormat.Json;
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + accessToken);
                    request.AddJsonBody(new
                    {
                        nfc_code = nfcID
                    });
                    RestResponse response = (RestResponse)client.Execute(request);

                    if (response != null &&
                        ((response.StatusCode == HttpStatusCode.OK) &&
                        (response.ResponseStatus == ResponseStatus.Completed)))
                    {
                        logger.Info("Update card by mrs " + magneticCardID + " - nfc " + nfcID + ": Success");
                        return "Updated successfully!";

                    }
                    else
                    {
                        logger.Info("Update card by mrs " + magneticCardID + " - nfc " + nfcID + ": Failed, status: " + response.StatusCode);
                        return "Update failed. Status is: " + response.StatusCode;
                    }
                }   
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "You not press OK";
            }
        }



        public string makeRequest(string magneticCardID)
        {
            string result = "";
            var request =
                new RestRequest("api/cards/" + magneticCardID,
                    Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + this.accessToken);

            RestResponse response = (RestResponse)client.Execute(request);
            if (response != null)
            {

                if ((response.StatusCode == HttpStatusCode.NotFound) &&
                    (response.ResponseStatus == ResponseStatus.Completed))
                {
                    logger.Info("Get card by mrs " + magneticCardID + ": not found" );
                    result = "ID not found";
                }
                else if (response.StatusCode == 0)
	            {
                    logger.Info("Get card by mrs " + magneticCardID + ": error, statusCode: " + response.StatusCode);
                    result = "Không thể kết nối tới tiNiZen";
	            }
                else
                {
                    JObject obj = JObject.Parse(response.Content);
                    if ((string)obj["nfc_code"] == null)
                    {
                        logger.Info("Get card by mrs " + magneticCardID + ": has no NFC");
                        result = "NFC not found";
                    }
                    else
                    {
                        logger.Info("Get card by mrs " + magneticCardID + ": already has NFC");
                        result = (string)obj["nfc_code"];
                    }
                }

            }
            else
            {
                // ??? what the hell?
                return response.StatusCode.ToString();
            }
            return result;
        }

    }
}
