using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace nKidReader
{
    class ServiceHandle
    {
        public string accessToken { get; set; }
        RestClient client;
        public ServiceHandle()
        {
            this.client = new RestClient("http://sandbox.tinizen.com");
            this.accessToken = getAccessToken();
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
                    client_secret = ConfigurationManager.AppSettings["client_secret"],
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
                    return (string)obj["access_token"];

                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public void uploadNFCCode(string action, string magneticCardID, string nfcID)
        {
            if (action == "OK")
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
                        MessageBox.Show("Updated successfully!");

                    }
                }
                catch (Exception)
                {

                    throw;
                }
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
                    result = "ID not found";
                }
                else
                {
                    JObject obj = JObject.Parse(response.Content);
                    if ((string)obj["nfc_code"] == null)
                    {
                        result = "NFC not found";
                    }
                    else
                        result = (string)obj["nfc_code"];
                }

            }
            return result;
        }

    }
}
