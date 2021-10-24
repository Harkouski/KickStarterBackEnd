using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class google
    {
        public  GoogleConfig _googleConfig;

        public google(IOptionsMonitor<GoogleConfig> optionsMonitor)
        {
            _googleConfig = optionsMonitor.CurrentValue;
        }
    
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string tokenId)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleConfig.client_id }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
    public class GoogleConfig
    {
        public string client_id { get; set; }
    }
}
