using System;
namespace BlazorDapperAPI.Data
{
    public class SqlConnectionConfiguration
    {
        public SqlConnectionConfiguration(string value)
        {
            this.Value = value;
        }
        public string Value { get;}
    }
}
