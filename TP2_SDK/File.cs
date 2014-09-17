using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// External Library
using RestSharp;
using Newtonsoft.Json;

// TP2_SDK
using TeamPlatform.TP2_SDK.TpResponse;

namespace TeamPlatform.TP2_SDK
{
    public class File : TP2
    {
        private RestClient client;

        public File()
        {
            client = new RestClient(Tp2Host);
        }

        public File(string strApiToken) : this()
        {
            
            ApiToken = strApiToken;
        }

        public List<FileResponse> all()
        {
            //RestRequest request = new RestRequest(ApiPath + "/files", Method.GET);
            RestRequest request = new RestRequest("file.json", Method.GET);
            request.AddHeader("Authentication", String.Format("OAuth {0}", ApiToken));

            try
            {
                IRestResponse httpResponse = client.Execute(request);
                List<FileResponse> jsonResponse = JsonConvert.DeserializeObject<List<FileResponse>>(httpResponse.Content);

                return jsonResponse;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        public FileResponse find_by_id(int FileId)
        {
            RestRequest request = new RestRequest(ApiPath + "/files", Method.GET);
            request.AddHeader("Authentication", String.Format("OAuth {0}", ApiToken));

            try
            {
                IRestResponse httpResponse = client.Execute(request);
                FileResponse jsonResponse = JsonConvert.DeserializeObject<FileResponse>(httpResponse.Content);

                return jsonResponse;
            }
            catch (Exception ee)
            {
                return null;
            }
        }

        public bool upload()
        {
            return true;
        }

        public bool download()
        {
            return true;
        }
               
    }    
}
