
using AlexandriaWrapper.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace AlexandriaWrapper.Helpers
{
    public class ApiHelper : IApiHelper
    {
        HttpClient client;

        private const string URL_Validator = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?";
       
        public void SetHttpClient(HttpClient _client)
        {
            if (_client != null)
                client = _client;
            else
                client = new HttpClient();
        }

      //Veracode vulnerability CWE-918 mitigated as per the design in the portal. Ref:https://community.veracode.com/s/question/0D52T00004s161DSAQ/how-to-fix-cwe918-serverside-request-forgery-ssrf
        public HttpResponseMessage POSTSpecial(WrapperModel WrapperData, string apiKey)
        {
            SetHttpClient(client);
            var url = WrapperData.url;
            var URL_regex = new System.Text.RegularExpressions.Regex(URL_Validator);
            if (!URL_regex.Match(url).Success)
                throw new ArgumentException("Invalid URL");

            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(WrapperData.content, UTF8Encoding.UTF8, "application/json");
            var messge = client.PostAsync(url, content).Result;
            client = null;
            return messge;
        }

        //Veracode vulnerability CWE-918 mitigated as per the design in the portal. Ref:https://community.veracode.com/s/question/0D52T00004s161DSAQ/how-to-fix-cwe918-serverside-request-forgery-ssrf
        public HttpResponseMessage GETSpecial(string url,string apiKey)
        {
            SetHttpClient(client);
            var URL_regex = new System.Text.RegularExpressions.Regex(URL_Validator);
            if (!URL_regex.Match(url).Success)
                throw new ArgumentException("Invalid URL");

            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage messge = client.GetAsync(url).Result;
            client = null;
            return messge;
         }
    }
}
