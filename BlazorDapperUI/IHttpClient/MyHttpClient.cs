using System;
using System.Net.Http;

namespace BlazorDapperUI.IHttpClient
{
    public class MyHttpClient
    {
        public readonly HttpClient value;
        public MyHttpClient(string baseAddress)
        {
            value = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
    }
}
