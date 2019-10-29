using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeConcept.Models
{
    public interface IDataRepository
    {
        IEnumerable<Node> GetAllNodes();
        Node GetNode(int nodeID);
        Node GetNodeByName(string nodeName);
        Node AddNode(Node node);
        void DeleteNode(Node node);
        void SwitchNodes(Node node1, Node node2);
        void EditNode(int nodeID, string nodeName);
    }
}
