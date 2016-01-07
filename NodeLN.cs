﻿/*
 *  Copyright (C) 2013 Pavel Charvat
 * 
 *  This file is part of IEDExplorer.
 *
 *  IEDExplorer is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  IEDExplorer is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with IEDExplorer.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEDExplorer
{
    class NodeLN: NodeBase
    {
        public NodeLN(string Name)
            : base(Name)
        {
        }

        public override void SaveModel(List<String> lines, bool fromSCL)
        {
            // Syntax: LN(<logical node name>){…}
            lines.Add("LN(" + Name + ") {");
            //List<NodeBase> DAs = new List<NodeBase>();

            foreach (NodeFC fc in _childNodes)
            {
                // Now we are at FC level
                //fc.GetAllLeaves(DAs);
            }
            lines.Add("}");
        }
    }
}
