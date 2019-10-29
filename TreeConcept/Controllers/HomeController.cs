using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TreeConcept.Models;
using TreeConcept.Models.ViewModels;

namespace TreeConcept.Controllers
{
    public class HomeController : Controller
    {
       private readonly IDataRepository _dataRepository;
        public bool AddNoteError { get; set; }

        public HomeController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Home()
        {
            return View();
        }
     

        [HttpPost]
        public IActionResult AddNode(int parent, Node node)
        {
          
            if (!ModelState.IsValid)
            {
                ViewBag.Error = 1;        
                return View("home");
            }

            node.Parent_ID = parent;
            _dataRepository.AddNode(node);
            ViewBag.Kategoria = node;
            return RedirectToAction("home");
        }

        [HttpPost]
        public IActionResult DeleteNode(int parent)
        {
            Node node = _dataRepository.GetNode(parent);
            _dataRepository.DeleteNode(node);
            return RedirectToAction("home");
        }

        [HttpPost]
        public IActionResult SwitchNode(SwitchNodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("home", model);
            }

            Node node1 = _dataRepository.GetNode(model.ID1);
            Node node2 = _dataRepository.GetNode(model.ID2);
            _dataRepository.SwitchNodes(node1, node2);

            return View("home");
        }

        [HttpPost]
        public IActionResult EditNode(int id, Node node)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = 2;
                return View("home");
            }
            _dataRepository.EditNode(id, node.Name);
            return RedirectToAction("home");
        }

        [HttpPost]
        public IActionResult SwitchSortingType(string order)
        {
            
            if(order != null)
            {
                if (order.Equals("ascending"))
                {
                    SeededNodes.IsAscending = true;
                }
                else if (order.Equals("descending"))
                {
                    SeededNodes.IsAscending = false;
                }
            }
            else
            {
                ModelState.AddModelError("SwitchSortingTypeError", "Switch sorting CheckBox cannot be empty.");
            }
            return View("home");
        }
    }
}

