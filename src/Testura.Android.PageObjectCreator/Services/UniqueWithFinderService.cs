using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;
using Testura.Android.Util;

namespace Testura.Android.PageObjectCreator.Services
{
    public class UniqueWithFinderService : IUniqueWithFinderService
    {
        private readonly IList<AttributeTags> _attributesPriority;

        public UniqueWithFinderService()
        {
            _attributesPriority = new List<AttributeTags>
            {
                AttributeTags.ResourceId,
                AttributeTags.ContentDesc,
                AttributeTags.Text,
                AttributeTags.Package,
                AttributeTags.Index
            };
        }

        public AutoSelectedWith GetUniqueWiths(Node selectedNode, IList<Node> allNode)
        {
            var combinations = GetAllCombinations(_attributesPriority);

            // We can not use only text or index.
            combinations.Remove(combinations.FirstOrDefault(c => c.Count == 1 && c.First() == AttributeTags.Text));
            combinations.Remove(combinations.FirstOrDefault(c => c.Count == 1 && c.First() == AttributeTags.Index));

            foreach (var combination in combinations)
            {
                var properties = new List<PropertyInfo>();

                foreach (var attributeinfo in combination)
                {
                    properties.Add(selectedNode.GetType().GetProperty(Enum.GetName(typeof(AttributeTags), attributeinfo)));
                }

                if (CheckAttribute(selectedNode, properties, allNode))
                {
                    return new AutoSelectedWith { Withs = new List<AttributeTags>(combination), Node = selectedNode };
                }
            }

            if (selectedNode.Parent != null)
            {
                var uniqueParent = GetUniqueWiths(selectedNode.Parent, allNode);
                var currentNodeUniqueWiths = GetUniqueWiths(selectedNode, selectedNode.Parent.Children);
                currentNodeUniqueWiths.Parent = uniqueParent;
                return currentNodeUniqueWiths;
            }

            throw new Exception("Failed to find any unique withs");
        }

        private bool CheckAttribute(Node node, IList<PropertyInfo> properties, IList<Node> allNodes)
        {
            if (properties.Any(p => string.IsNullOrEmpty(p.GetValue(node)?.ToString())))
            {
                return false;
            }

            if (allNodes.Count == 1)
            {
                return true;
            }

            if (allNodes.Any(n =>
            {
                if (n == node)
                {
                    return false;
                }

                return properties.All(p =>
                {
                    var value = p.GetValue(n)?.ToString();
                    if (string.IsNullOrEmpty(value))
                    {
                        return false;
                    }

                    return value.Equals(p.GetValue(node));
                });
            }))
            {
                return false;
            }

            return true;
        }

        private IList<IList<T>> GetAllCombinations<T>(IList<T> inputList, int minimumItems = 1)
        {
            var nonEmptyCombinations = (int)Math.Pow(2, inputList.Count) - 1;
            var listOfLists = new List<IList<T>>(nonEmptyCombinations + 1);

            for (int i = 1; i <= nonEmptyCombinations; i++)
            {
                var thisCombination = new List<T>();
                for (int j = 0; j < inputList.Count; j++)
                {
                    if ((i >> j & 1) == 1)
                    {
                        thisCombination.Add(inputList[j]);
                    }
                }

                if (thisCombination.Count >= minimumItems)
                {
                    listOfLists.Add(thisCombination);
                }
            }

            return listOfLists;
        }
    }
}
