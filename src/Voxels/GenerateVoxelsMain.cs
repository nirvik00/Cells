using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Cells.src.Voxels
{
    public class GenerateVoxelsMain : GH_Component
    {
        public GenerateVoxelsMain()
          : base("3d-Grid", "3d-Grid",
              "generates 3d grid",
              "Cells", "Voxels")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // 0. X-number
            pManager.AddIntegerParameter("X-Number", "x-num", "Number of Cells in X-dir", GH_ParamAccess.item, 10);
            // 1. Y-number
            pManager.AddIntegerParameter("Y-Number", "y-num", "Number of Cells in Y-dir", GH_ParamAccess.item, 10);
            // 2. Z-number
            pManager.AddIntegerParameter("Z-Number", "z-num", "Number of Cells in Z-dir", GH_ParamAccess.item, 10);
            // 3. X-dimension
            pManager.AddNumberParameter("X-Dimension", "x-dim", "Dimension of Cells in X-dir", GH_ParamAccess.item, 10.0);
            // 4. Y-dimension
            pManager.AddNumberParameter("Y-Dimension", "y-dim", "Dimension of Cells in Y-dir", GH_ParamAccess.item, 10.0);
            // 5. Z-dimension
            pManager.AddNumberParameter("Z-Dimension", "z-dim", "Dimension of Cells in Z-dir", GH_ParamAccess.item, 10.0);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Point-List", "pts", "list of output points- center of 3d grid", GH_ParamAccess.list);
            pManager.AddBrepParameter("Voxel-List", "voxels", "list of output box (as brep)", GH_ParamAccess.list);
            pManager.AddGenericParameter("Array - Grid of Voxel Object (with Data Structure)", "voxel object grid", "Array to represent Voxel Object Grid with internal Data Structure for operations", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int numX = 10;
            int numY = 10;
            int numZ = 10;
            double dimX = 10.0;
            double dimY = 10.0;
            double dimZ = 10.0;
            if (!DA.GetData(0, ref numX)) return;
            if (!DA.GetData(1, ref numY)) return;
            if (!DA.GetData(2, ref numZ)) return;
            if (!DA.GetData(3, ref dimX)) return;
            if (!DA.GetData(4, ref dimY)) return;
            if (!DA.GetData(5, ref dimZ)) return;

            List<Point3d> centerPtList = new List<Point3d>();
            List<Brep> brepLi = new List<Brep>();
            List<Voxel> voxelLi = new List<Voxel>();
            int I = 0;
            for (double i=0; i<numX*dimX; i+=dimX)
            {
                int J = 0;
                for(double j=0; j<numY*dimY; j+=dimY)
                {
                    int K = 0;
                    for(double k=0; k<numZ*dimZ; k+=dimZ)
                    {
                        double x = i;
                        double y = j;
                        double z = k;
                        Point3d c = new Point3d(x + dimX / 2, y + dimY / 2, z + dimZ / 2);
                        centerPtList.Add(c);
                        Point3d p = new Point3d(i, j, k);
                        Point3d q = new Point3d(i+dimX, j, k);
                        Point3d r = new Point3d(i+dimX, j+dimY, k);
                        Point3d s = new Point3d(i, j+dimY, k);
                        List<Point3d> ptList = new List<Point3d> { p, q, r, s, p };
                        PolylineCurve poly = new PolylineCurve(ptList);
                        Extrusion mass = Extrusion.Create(poly, dimZ, true);
                        Brep brep = mass.ToBrep();
                        brepLi.Add(brep);
                        Voxel v = new Voxel(c, brep, poly, I, J, K);
                        voxelLi.Add(v);
                        K++;
                    }
                    J++;
                }
                I++;
            }

            DA.SetDataList(0, centerPtList);
            DA.SetDataList(1, brepLi);
            DA.SetDataList(2, voxelLi);
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
            get { return new Guid("84d5178f-5828-4770-b392-edc8c37c2e57"); } // updated
        }
    }
}