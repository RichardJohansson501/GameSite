using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamePortal.Core.ViewModels
{
    public enum NodeType
    {
        Course,
        Module,
        Activity,
    }

    public struct Node
    {
        public NodeType nodeType;
        public bool opened;
        public string name;
    }
    
    public class CourseTreeViewModel
    {
        public List<Node> nodes;
        public string nodeDescr;

        public CourseTreeViewModel()
        {
            nodes = new List<Node>();
        }
    }
}