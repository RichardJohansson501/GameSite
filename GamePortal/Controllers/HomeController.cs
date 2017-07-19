using GamePortal.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamePortal.Controllers
{
    public class HomeController : Controller
    {
        static public CourseTreeViewModel viewModel;
        static public List<string> descriptions;

        public HomeController()
        {
            if (viewModel == null)
            {
                viewModel = new CourseTreeViewModel();
                var node = new Node();
                node.nodeType = NodeType.Course;
                node.opened = false;
                node.name = "Course 1";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Module;
                node.opened = false;
                node.name = "Module 11";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Activity;
                node.opened = false;
                node.name = "Activity 21";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Activity;
                node.opened = false;
                node.name = "Activity 22";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Module;
                node.opened = false;
                node.name = "Module 12";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Activity;
                node.opened = false;
                node.name = "Activity 23";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Course;
                node.opened = false;
                node.name = "Course 2";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Module;
                node.opened = false;
                node.name = "Module 13";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Module;
                node.opened = false;
                node.name = "Module 14";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Activity;
                node.opened = false;
                node.name = "Activity 24";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Activity;
                node.opened = false;
                node.name = "Activity 25";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Module;
                node.opened = false;
                node.name = "Module 15";
                viewModel.nodes.Add(node);

                node = new Node();
                node.nodeType = NodeType.Activity;
                node.opened = false;
                node.name = "Activity 26";
                viewModel.nodes.Add(node);
            }

            if (descriptions == null)
            {
                descriptions = new List<string>();
                descriptions.Add("Course 1 description");
                descriptions.Add("Module 11 description");
                descriptions.Add("Activity 21 description");
                descriptions.Add("Activity 22 description");
                descriptions.Add("Module 12 description");
                descriptions.Add("Activity 23 description");
                descriptions.Add("Course 2 description");
                descriptions.Add("Module 13 description");
                descriptions.Add("Module 14 description");
                descriptions.Add("Activity 24 description");
                descriptions.Add("Activity 25 description");
                descriptions.Add("Module 15 description");
                descriptions.Add("Activity 26 description");
            }
        }

        public ActionResult Index(string toggleOpened, string nodeIndex)
        {
            if (nodeIndex == null)
            {
                viewModel.nodeDescr = "";
            }
            else
            {
                var nodeOffset = int.Parse(nodeIndex);
                if (toggleOpened == "toggle")
                {
                    var node = viewModel.nodes.ElementAt(nodeOffset);
                    node.opened = !node.opened;
                    viewModel.nodes.RemoveAt(nodeOffset);
                    viewModel.nodes.Insert(nodeOffset, node);

                    if ((node.nodeType == NodeType.Course) && (node.opened == false))
                    {
                        nodeOffset++;
                        node = viewModel.nodes.ElementAt(nodeOffset);
                        while (node.nodeType == NodeType.Module)
                        {
                            node.opened = false;
                            viewModel.nodes.RemoveAt(nodeOffset);
                            viewModel.nodes.Insert(nodeOffset, node);

                            nodeOffset++;
                            node = viewModel.nodes.ElementAt(nodeOffset);
                        }
                    }
                    viewModel.nodeDescr = "";
                }
                else
                {
                    viewModel.nodeDescr = descriptions.ElementAt(nodeOffset);
                }
            }
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}