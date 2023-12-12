using Microsoft.BingAds;
using Microsoft.BingAds.V13.CampaignManagement;
using System.Configuration;
using System.Net.Security;

namespace BingAdsTrainingProjects
{
    internal class Program
    {

        static AuthorizationData authorizationData;
        static ServiceClient<ICampaignManagementService> CampaignService;
        static string ClientState = "ClientStateGoesHere";
        static void Main(string[] args)
        {
            try
            {
                Authentication authentication = AuthenticateWithOAuth();

                // Most Bing Ads API service operations require account and customer ID. 
                // This utiltiy operation sets the global authorization data instance 
                // to the first account that the current authenticated user can access. 
                SetAuthorizationDataAsync(authentication).Wait();

                // You can extend the console app with the examples library at:
                // https://github.com/BingAds/BingAds-dotNet-SDK/tree/main/examples/BingAdsExamples
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("OAuthTokenRequestException Message:\n{0}", ex.Message));
                if (ex.Details != null)
                {
                    OutputStatusMessage(string.Format("OAuthTokenRequestException Details:\nError: {0}\nDescription: {1}",
                    ex.Details.Error, ex.Details.Description));
                }
            }
        }

        private static void OutputStatusMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static Authentication AuthenticateWithOAuth()
        {
            var apiEnvironment =
                            ConfigurationManager.AppSettings["BingAdsEnvironment"] == ApiEnvironment.Sandbox.ToString() ?
                            ApiEnvironment.Sandbox : ApiEnvironment.Production;
            var oAuthDesktopMobileAuthCodeGrant = new OAuthDesktopMobileAuthCodeGrant(
                ConfigurationManager.AppSettings.Default["ClientId"].ToString(),
                apiEnvironment
            );
        }
    }
}
