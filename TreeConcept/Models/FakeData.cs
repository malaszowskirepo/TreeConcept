using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeConcept.Models
{
    public class FakeData : IDataRepository
    {
        private List<Node> nodes;

        public bool IsAscending { get; set; } = true;


        public FakeData()
        {
            nodes = new List<Node>()
            {
                new Node { ID = 1, Parent_ID = null, Name = "Root" },
                new Node { ID = 2, Parent_ID = 1, Name = "el1" },
                new Node { ID = 3, Parent_ID = 1, Name = "el2" },
                new Node { ID = 4, Parent_ID = 2, Name = "el11" }

            };
        }

        public Node AddNode(Node node)
        {
            node.ID = nodes.Max(c => c.ID) + 1;
            nodes.Add(node);
            return node;
        }

     
        public static bool isLeaf(Node node, IEnumerable<Node> _nodes)
        {
            if(_nodes.Any(c => c.Parent_ID == node.ID))
            {
                return true;
            }
            return false;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            int node_id = node.ID;
            if(isLeaf(node, nodes))
            {
                IEnumerable<Node> childrens = nodes.Where(c => c.Parent_ID == node_id).ToList();
                foreach(Node child in childrens)
                {
                    nodes.Remove(child);
                    DeleteNode(child);
                }
            }
        }

        public IEnumerable<Node> GetAllNodes()
        {
            return nodes;
        }
     
        public Node GetNode(int nodeID)
        {
            return nodes.FirstOrDefault(c => c.ID == nodeID);
        }

        public Node GetNodeByName(string nodeName)
        {
            return nodes.FirstOrDefault(c => c.Name == nodeName);
        }

        public IEnumerable<Node> GetNodeChildrens(Node node)
        {
            return GetAllNodes().Where(c => c.Parent_ID == node.ID);
        }

        public bool IsRelated(Node node1, Node node2)
        {
            if (node1.ID == node2.ID)
            {
                return true;
            }
            else
            {
                foreach (Node child in GetNodeChildrens(node1))
                {
                    if (IsRelated(child, node2)) return true;
                }
            }
            return false;
        }

        public void SwitchNodes(Node node1, Node node2)
        {
            if (IsRelated(node1, node2) || IsRelated(node1, node2))
            {
                int tempParentID = node1.Parent_ID.Value;
                int tempID = node1.ID;
                node1.ID = node2.ID;
                node1.Parent_ID = node2.Parent_ID;
                node2.ID = tempID;
                node2.Parent_ID = tempParentID;
            }
            else
            {
                int tempParentID = node1.Parent_ID.Value;
                node1.Parent_ID = node2.Parent_ID;
                node2.Parent_ID = tempParentID;
            }
        }

        public void EditNode(int nodeID, string nodeName)
        {
            Node node = GetNode(nodeID);
            node.Name = nodeName;
        }
    }
}
