using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TreeConcept.Models
{
    public class SqlNodeRepository : IDataRepository
    {

        public SqlNodeRepository(AppDbContext context)
        {
            Context = context;
        }
        public AppDbContext Context { get; }


        public Node AddNode(Node node)
        {
            node.ID = Context.Nodes.Max(c => c.ID) + 1;
            Context.Add(node);
            Context.SaveChanges();
            return node;
        }


        public void DeleteNode(Node node)
        {
            Context.Remove(node);
            Context.SaveChanges();
            int node_id = node.ID;
            if(Context.Nodes.Any(c => c.Parent_ID == node.ID))
            {
                IEnumerable<Node> childrens = Context.Nodes.Where(c => c.Parent_ID == node_id).ToList();
                foreach (Node child in childrens)
                {
                    Context.Remove(child);
                    DeleteNode(child);
                }
            }
        }

        public void EditNode(int nodeID, string nodeName)
        {
            Node node = GetNode(nodeID);
            node.Name = nodeName;
            Context.SaveChanges();
        }

        public IEnumerable<Node> GetAllNodes()
        {
            return Context.Nodes;
        }

        public Node GetNode(int nodeID)
        {
            return Context.Nodes.FirstOrDefault(c => c.ID == nodeID);
        }

        public Node GetNodeByName(string nodeName)
        {
            return Context.Nodes.FirstOrDefault(c => c.Name == nodeName);
        }

        public IEnumerable<Node> GetNodeChildrens(Node node)
        {
            return GetAllNodes().Where(c => c.Parent_ID == node.ID);
        }

        public bool HasChildrens(Node node)
        {
            if(GetNodeChildrens(node).Count() > 0)
            {
                return true;
            }
            return false;
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


        public List<int> GetAllChildrens(Node node, List<int> tempList)
        {
            foreach (Node child in GetNodeChildrens(node))
            {
                tempList.Add(child.ID);
                GetAllChildrens(child, tempList);
            }
            return tempList;
        }

        public void SwitchNodes(Node node1, Node node2)
        {
            if(IsRelated(node1, node2) || IsRelated(node1, node2))
            {
                int tempParentID = node1.Parent_ID.Value;
                int tempID = node1.ID;
                node1.ID = node2.ID;
                node1.Parent_ID = node2.Parent_ID;
                node2.ID = tempID;
                node2.Parent_ID = tempParentID;
                Context.SaveChanges();
            }
            else
            {    
                int tempParentID = node1.Parent_ID.Value;
                node1.Parent_ID = node2.Parent_ID;
                node2.Parent_ID = tempParentID;
                Context.SaveChanges();
            }
        }
    }
}
