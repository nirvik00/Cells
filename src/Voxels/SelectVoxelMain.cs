using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Cells.src.Voxels
{
    public struct Idx
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public Idx(int i, int j, int k)
        {
            this.x = i;
            this.y = j;
            this.z = k;
        }
    }

    public class SelectVoxelMain : GH_Component
    {
        public SelectVoxelMain()
          : base("SelectVoxel", "select-xyz-indices",
              "Select Voxels by x,y,z-indices USE the accompanying component to Generate Indices ",
              "Cells", "Voxels")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            List<int> tmpIdXLi = new List<int> { 0 };
            List<int> tmpIdYLi = new List<int> { 0 };
            List<int> tmpIdZLi = new List<int> { 0 };
            // 0. list of voxels
            pManager.AddGenericParameter("List of Voxel Object with Data Structure", "voxel objects", "List of voxel objects from generator with internal data structure", GH_ParamAccess.list);
            // 2. X-number
            pManager.AddIntegerParameter("X-Index", "x-index", "Indices of Cells in X-dir", GH_ParamAccess.list, tmpIdXLi);
            // 3. Y-number
            pManager.AddIntegerParameter("Y-Index", "y-index", "Indices of Cells in Y-dir", GH_ParamAccess.list, tmpIdYLi);
            // 4. Z-number
            pManager.AddIntegerParameter("Z-Index", "z-index", "Indices of Cells in Z-dir", GH_ParamAccess.list, tmpIdZLi);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // 0
            pManager.AddPointParameter("Point-List", "pts", "list of output points- center of 3d grid", GH_ParamAccess.list);
            // 1.
            pManager.AddBrepParameter("Voxel-List", "voxels", "list of output box (as brep)", GH_ParamAccess.list);
            // 2.
            pManager.AddGenericParameter("List of Voxel Data Structure", "voxel-ds", "List of output voxels from generator with internal data structure", GH_ParamAccess.list);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Idx> IndexLi = new List<Idx>();
            List<Voxel> inpVoxelList = new List<Voxel>();
            List<int> idXLi = new List<int>();
            List<int> idYLi = new List<int>();
            List<int> idZLi = new List<int>();

            if (!DA.GetDataList(0, inpVoxelList)) return;
            if (!DA.GetDataList(1, idXLi)) return;
            if (!DA.GetDataList(2, idYLi)) return;
            if (!DA.GetDataList(3, idZLi)) return;

            // construct the index list
            for (int i=0; i<idXLi.Count; i++)
            {
                int a = idXLi[i];
                int b = idYLi[i];
                int c = idZLi[i];
                Idx index = new Idx(a, b, c);
                IndexLi.Add(index);
            }

            // iterate over input and select voxels
            List<Point3d> cpList = new List<Point3d>();
            List<Brep> brepLi = new List<Brep>();
            List<Voxel> outputVoxelList = new List<Voxel>();
            for (int i=0; i<inpVoxelList.Count; i++)
            {
                Voxel v = inpVoxelList[i];
                bool T = idxMatch(IndexLi, v.x, v.y, v.z);
                if (T)
                {
                    Point3d c = v.CenterPoint;
                    Brep brep = v.Mass;
                    if (v.selected == false)
                    {
                        cpList.Add(c);
                        brepLi.Add(brep);
                        outputVoxelList.Add(v);
                        v.selected = true;
                    }
                }
            }
            DA.SetDataList(0, cpList);
            DA.SetDataList(1, brepLi);
            DA.SetDataList(2, outputVoxelList);  
        }

        public bool idxMatch(List<Idx> indexLi, int a, int b, int c)
        {
            bool t = false;
            for(int i=0; i<indexLi.Count; i++)
            {
                int p = indexLi[i].x;
                int q = indexLi[i].y;
                int r = indexLi[i].z;
                if(p==a && q==b && r == c)
                {
                    t = true;
                    return t;       
                }
            }
            return t;
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.voxelSelector;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("073cf317-6584-443b-ba40-ebc5a8fdce36"); } // updated
        }
    }
}