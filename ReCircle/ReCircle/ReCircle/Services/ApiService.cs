using System;
using System.Threading.Tasks;
using Plugin.Connectivity;
using ReCircle.Classes;

namespace ReCircle.Services
{
    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Parece ser que no tienes conexión a internet",
                };
            }

            //var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
            //"google.com");
            //if (!isReachable)
            //{
            // return new Response
            // {
            //   IsSuccess = false,
            // Message = "Revise su conexión a internet",
            //};
            //}

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }
    }
}
