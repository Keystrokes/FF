using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FriendsFirst.Web.Controllers
{
    public class ProjectionsController : Controller
    {
        // GET: Projections
        public ActionResult Index()
        {
            var model = new FriendsFirst.Web.Models.Projections.Projection()
            {
                Contract = FriendsFirst.Web.Models.TestContracts.SavingsContract,
                Projections = new List<FriendsFirst.Projections.ProjectedContract<FriendsFirst.Contracts.StandardSavingsContract>>()
            };
            return View(model);
        }

        public ActionResult Project(FormCollection form)
        {
            var Alterations = new FriendsFirst.Web.Models.Projections.ProjectionAlteration[] { };
            //Get the Contract
            var contract = FriendsFirst.Web.Models.TestContracts.SavingsContract;

            //Apply any alterations that have been provided
            

            //Create a projection
            var projector = new FriendsFirst.Projections.Projection<FriendsFirst.Contracts.StandardSavingsContract>(
                contract);

            //Project to maturity date
            var projection = projector[contract.Matures];

            //Create a new view model from the projection
            var model = new Web.Models.Projections.Projection()
            {
                Contract = contract,
                Projections = projector.Projections
            };

            //Return the projection.
            return View("index", model);
        }

        List<object> Alterations(ref FriendsFirst.Contracts.StandardSavingsContract contract, FormCollection form)
        {

            //Check all of the form values and if diffrent from the contract 
            //then update the contract and record the old and new values.


            return new List<object>();
        }
    }
}
