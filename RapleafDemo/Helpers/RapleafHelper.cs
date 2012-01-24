using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using RapleafDemo.Models;

namespace RapleafDemo.Helper
{
    class RapleafHelper
    {
        protected const string APIKey = "be9cec8790ddc423bbc1d3204acbb005"; //shhhh
        protected const string URLTemplate = "https://personalize.rapleaf.com/v4/dr?first={0}&last={1}&email={2}&api_key={3}&show_available=true";

        protected static string GetHttpResponse(string requestUrl)
        {
            // declare objects
            string responseData = String.Empty;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            StreamReader strmReader = null;

            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);


                resp = (HttpWebResponse)req.GetResponse();
                strmReader = new StreamReader(resp.GetResponseStream());
                responseData = strmReader.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (req != null)
                {
                    req = null;
                }

                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
            }

            return responseData;
        }

        protected static string GetValue(JObject o, string field)
        {
            JToken token = o.SelectToken(field);

            if (token == null)
                return string.Empty;
            else
                return (string)token;
        }

        protected static bool IsAvailable(JObject o, string field)
        {
            JToken token = o.SelectToken(field);

            if (token == null)
                return false;
            else
                return true;
        }

        public static RapleafBasicFields GetUserInfo(string firstName, string lastName, string emailAddress)
        {
            string url = String.Format(URLTemplate, firstName, lastName, emailAddress, APIKey);

            string response = GetHttpResponse(url);
            RapleafBasicFields userInfo = new RapleafBasicFields();
            if (!string.IsNullOrWhiteSpace(response))
            {
                JObject o = JObject.Parse(response);
                userInfo.HouseholdIncome = IsAvailable(o, "household_income");
                userInfo.LoanToValueRation = IsAvailable(o, "loan_to_value_ratio");
                userInfo.LikelySmartphoneUser = IsAvailable(o, "likely_smartphone_user");
                userInfo.InvestedAssets = IsAvailable(o, "invested_assets");
                userInfo.HomeOwnerStatus = IsAvailable(o, "home_owner_status");
                userInfo.HomeMarketValue = IsAvailable(o, "home_market_value");
                userInfo.LengthOfResidence = IsAvailable(o, "length_of_residence");
                userInfo.Children = IsAvailable(o, "children");
                userInfo.MaritalStatus = IsAvailable(o, "marital_status");
                userInfo.Education = IsAvailable(o, "education");

                userInfo.Age = GetValue(o, "age");
                userInfo.Gender = GetValue(o, "gender");
            }

            return userInfo;
        }

    }
}