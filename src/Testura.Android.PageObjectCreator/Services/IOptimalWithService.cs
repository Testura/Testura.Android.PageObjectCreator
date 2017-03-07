﻿using System.Collections.Generic;
using Testura.Android.Device.Ui.Nodes.Data;
using Testura.Android.PageObjectCreator.Models;

namespace Testura.Android.PageObjectCreator.Services
{
    public interface IOptimalWithService
    {
        OptimalWith GetOptimalWith(Node selectedNode, IList<Node> allNode);
    }
}
