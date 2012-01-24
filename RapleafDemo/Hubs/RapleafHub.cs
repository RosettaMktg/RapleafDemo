using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using RapleafDemo.Helper;
using RapleafDemo.Models;
using SignalR;

namespace RapleafDemo.SignalR
{
    public class Aggregates
    {
        int totalCount;
        int numMale;
        int numFemale;

    }

    [HubName("rapleafHub")]
    public class RapleafHub : Hub
    {
        protected static int totalResults = 0;

        public void GetUserInfo(string firstName, string lastName, string email)
        {
            RapleafBasicFields fields = RapleafHelper.GetUserInfo(firstName, lastName, email);

            Clients.getResults(fields);
            //Clients.updateAggregate(fields);
        }



    }
}