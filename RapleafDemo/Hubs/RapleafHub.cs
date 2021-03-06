﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using RapleafDemo.Helper;
using RapleafDemo.Models;
using SignalR;
using System.Collections;

namespace RapleafDemo.SignalR
{
    public class Aggregates
    {
        public int totalCount;
        public int numMale;
        public int numFemale;
        public int age1;
        public int age2;
        public int age3;
        public int age4;
        public int age5;
        public int age6;
        public int age7;

        public int HouseholdIncome;
        public int LoanToValueRation;
        public int LikelySmartphoneUser;
        public int InvestedAssets;
        public int HomeOwnerStatus;
        public int HomeMarketValue;
        public int LengthOfResidence;
        public int Children;
        public int MaritalStatus;
        public int Education;
    }

    [HubName("rapleafHub")]
    public class RapleafHub : Hub
    {
        protected static ArrayList emailAddresses = new ArrayList();
        protected static Aggregates aggregate = new Aggregates();

        public void GetUserInfo(string firstName, string lastName, string email)
        {
            RapleafBasicFields fields = RapleafHelper.GetUserInfo(firstName, lastName, email);

            Clients.getResults(fields);
            aggregate.totalCount++;

            if (!emailAddresses.Contains(email))
            {
                if (fields.Gender == "Male")
                    aggregate.numMale++;
                else if (fields.Gender == "Female")
                    aggregate.numFemale++;

                if (fields.Age == "18-20")
                    aggregate.age1++;
                else if (fields.Age == "21-24")
                    aggregate.age2++;
                else if (fields.Age == "25-34")
                    aggregate.age3++;
                else if (fields.Age == "35-44")
                    aggregate.age4++;
                else if (fields.Age == "45-54")
                    aggregate.age5++;
                else if (fields.Age == "55-64")
                    aggregate.age6++;
                else if (fields.Age == "65+")
                    aggregate.age7++;

                if (fields.HouseholdIncome)
                    aggregate.HouseholdIncome++;
                
                if (fields.LoanToValueRation)
                    aggregate.LoanToValueRation++;
                
                if (fields.LikelySmartphoneUser)
                    aggregate.LikelySmartphoneUser++;
                
                if (fields.InvestedAssets)
                    aggregate.InvestedAssets++;
                if (fields.HomeOwnerStatus)
                    aggregate.HomeOwnerStatus++;
                if (fields.HomeMarketValue)
                    aggregate.HomeMarketValue++;
                
                if (fields.LengthOfResidence)
                    aggregate.LengthOfResidence++;
                
                if (fields.Children)
                    aggregate.Children++;
                
                if (fields.MaritalStatus)
                    aggregate.MaritalStatus++;
                
                if (fields.Education)
                    aggregate.Education++;

                emailAddresses.Add(email);
            }

            Clients.updateAggregate(aggregate);
        }



    }
}