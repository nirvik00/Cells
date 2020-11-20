using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Cells
{
    public class CellsComponent : GH_Component
    {
        public CellsComponent()
          : base("About", "about",
              "About",
              "Cells", "cells")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("about", "about", "about", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string s = "Created at CASE, RPI";
            DA.SetData(0, s);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("5451362c-b2ac-4ed5-ae63-66bb96211fc7"); } // updated
        }
    }
}
