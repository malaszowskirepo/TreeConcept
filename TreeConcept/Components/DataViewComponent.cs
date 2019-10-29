using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeConcept.Models;

namespace TreeConcept.Components
{
    public class DataViewComponent : ViewComponent
    {
        private readonly IDataRepository _dataRepository;
        private IUrlHelperFactory _factory;

        public DataViewComponent(IDataRepository dataRepository, IUrlHelperFactory factory)
        {
            _dataRepository = dataRepository;
            _factory = factory;
        }

        

        public IViewComponentResult Invoke(int? errorCode)
        {
           

            bool check = SeededNodes.IsAscending;
            System.Diagnostics.Debug.WriteLine("check: " + check);
            IOrderedEnumerable<Node> result;
            IEnumerable<Node> nodes = _dataRepository.GetAllNodes();
            if (SeededNodes.IsAscending == true)
            {
                result = nodes.OrderBy(c => c.Name);
            }
            else
            {
                result = nodes.OrderByDescending(c => c.Name);
            }

            SeededNodes model = new SeededNodes { Seed = null, Nodes = result };   

            switch (errorCode)
            {
                case 1:
                    ModelState.AddModelError("AddNodeError", "Name should be greater than 3 characters");
                    break;
                case 2:
                    ModelState.AddModelError("EditNodeError", "Name in Edit Box should be greater than 3 characters");
                    break;
            }   
            return View("DataIndex", model);
        }
    }
}
