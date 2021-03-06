using System.Security.Principal;
using SignalR;
using SignalR.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RapleafDemo.App_Start.EventStreamStart), "Start")]
namespace RapleafDemo.App_Start {
    public static class EventStreamStart {
        public static void Start() {
			DependencyResolver.Register(typeof (IStreamAuthorize), () => new SignalRAuthorize());
        }
		
        /// <summary>
        /// Authorize method used by SignalR.EventStream
        /// </summary>
        public class SignalRAuthorize : IStreamAuthorize
        {
            /// <summary>
            /// This method is called whenever a client 
            /// attempts to connect to the service
            /// </summary>
            /// <param name="clientId">The guid of the client connection</param>
            /// <param name="user">HttpContext User for the request</param>
            /// <param name="group">What group they're attempting to connect to</param>
            /// <returns>Returns whether or not the user is authorized</returns>
            public bool Authorized(ref string clientId, IPrincipal user, string @group)
            {
                return true;
            }
        }
    }
}