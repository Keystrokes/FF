using FriendsFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendsFirst.Web.Models
{
    public static class TestContracts
    {
        public static FriendsFirst.Contracts.StandardSavingsContract SavingsContract
        {
            get {
                return new Contracts.StandardSavingsContract()
                {
                    ContractNumber = "50416217",
                    CommencmentDate = new DateTime(2016, 10, 14),
                    Matures = new DateTime(2016, 10, 14).AddYears(25),

                    Premium = new Premiums.RegulerPaying(Premiums.PremiumFrequency.Monthly)
                    {
                        Amount = 1000.00,
                        IndexationRate = 0.15,
                        Status = Premiums.PremiumStatuses.InForce
                    },
                    Coverages = new Entities.Person[]
                    {
                        new Person(){
                            Name = "Joe Blogg",
                            Gender = Genders.Male,
                            DOB = new DateTime(1968, 07, 27)
                        },
                        new Person(){
                            Name = "Jane Blogg",
                            Gender = Genders.Female,
                            DOB = new DateTime(1969, 08, 28)
                        }
                    },
                    Product = new Products.StandardPensionProduct()
                    {
                        Description = "Conductor Savings Plan",
                        Code = "ZSR",

                        Charge = 3.5,
                        ChargeIncreasesRate = 0.015,
                        ChargePercent = 1.0,
                    },

                    Funds = new Funds.InvestedFunds()
                    {
                        Items = new Funds.Fund[]
                        {
                            new Funds.UnitLinkedFund()
                            {
                                Code = "606",
                                GrowthRate = 0.05,
                                Name = "With Profit",
                                ManagmentCharge = 0.0075,
                                InitalUnitDiscountFactor = 1,
                                InvestmentPercent = 1,
                                Units = new Funds.Unit(
                                    0,
                                    152.55336,
                                    1.701,
                                    1.701,
                                    0)
                            }
                        }
                    }
                };
            }

        }
        
    }
}