using AlexandriaWrapper.Model;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Net.Http;

namespace AlexandriaWrapper.Helpers
{
    public interface IApiHelper
    {
        HttpResponseMessage POSTSpecial( WrapperModel wrapperData, string apiKey);
        HttpResponseMessage GETSpecial(string url, string apiKey);
     
    }
}
