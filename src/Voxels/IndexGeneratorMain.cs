using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Cells.src.Voxels
{
    public class IndexGeneratorMain : GH_Component
    {
        public IndexGeneratorMain()
          : base("Generate Indices", "gen-index",
              "generates list of indices to select voxels",
              "Cells", "Voxels")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // 0. min X 
            pManager.AddIntegerParameter("minimum x-index value", "min-x-index", "minimum value of the x-index", GH_ParamAccess.item, 0);
            // 1. max X 
            pManager.AddIntegerParameter("maximum x-index value", "max-x-index", "maximum value of the x-index", GH_ParamAccess.item, 10);
            // 2. min y
            pManager.AddIntegerParameter("minimum y-index value", "min-y-index", "minimum value of the y-index", GH_ParamAccess.item, 0);
            // 3. max y
            pManager.AddIntegerParameter("maximum y-index value", "max-y-index", "maximum value of the y-index", GH_ParamAccess.item, 10);
            // 4. min z
            pManager.AddIntegerParameter("minimum z-index value", "min-z-index", "minimum value of the z-index", GH_ParamAccess.item, 0);
            // 5. max z 
            pManager.AddIntegerParameter("maximum z-index value", "max-z-index", "maximum value of the z-index", GH_ParamAccess.item, 10);
            // 6. num values
            pManager.AddIntegerParameter("number of indices", "num vals", "number of values to generate => number of voxels to select", GH_ParamAccess.item, 20);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // 0. x indices
            pManager.AddIntegerParameter("x-indices-list", "x-indices", "list of x-indices", GH_ParamAccess.list);
            // 1. y indices
            pManager.AddIntegerParameter("y-indices-list", "y-indices", "list of y-indices", GH_ParamAccess.list);
            // 2. z indices
            pManager.AddIntegerParameter("z-indices-list", "z-indices", "list of z-indices", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Random rnd = new Random();
            int minX = 0;
            int maxX = 2;
            int minY = 0;
            int maxY = 2;
            int minZ = 0;
            int maxZ = 2;
            int num = 2;
            if (!DA.GetData(0, ref minX)) return;
            if (!DA.GetData(1, ref maxX)) return;
            if (!DA.GetData(2, ref minY)) return;
            if (!DA.GetData(3, ref maxY)) return;
            if (!DA.GetData(4, ref minZ)) return;
            if (!DA.GetData(5, ref maxZ)) return;
            if (!DA.GetData(6, ref num)) return;

            List<int> xInd = new List<int>();
            List<int> yInd = new List<int>();
            List<int> zInd = new List<int>();
            List<int[]> indices = new List<int[]>();
            int numGot = 0;
            int numItrs = 100;
            while(numGot<num && numItrs<num*100)
            {
                int a = rnd.Next(maxX - minX) + minX;
                int b = rnd.Next(maxY - minY) + minY;
                int c = rnd.Next(maxZ - minZ) + minZ;
                int[] idx = { a, b, c };
                bool t=matchExistingIndex(indices, a, b, c);
                if (t == false)
                {
                    indices.Add(idx);
                    xInd.Add(a);
                    yInd.Add(b);
                    zInd.Add(c);
                    numGot++;
                }
                if (numGot == num) break;
            }

            DA.SetDataList(0, xInd);
            DA.SetDataList(1, yInd);
            DA.SetDataList(2, zInd);
        }

        public bool matchExistingIndex(List<int[]> indLi, int a, int b, int c )
        {
            bool t = false;
            for(int i=0; i<indLi.Count; i++)
            {
                int p = indLi[i][0];
                int q = indLi[i][1];
                int r = indLi[i][2];
                if(p==a && q==b && r == c)
                {
                    t = true;
                    break;
                }
            }
            return t;
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.VoxelIndexGen;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2280df89-2ecd-4419-ac50-c86d98922cd5"); } // updated
        }
    }
}