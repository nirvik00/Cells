using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

namespace Cells
{
    class Voxel
    {
        public Point3d CenterPoint;
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public Brep Mass { get; set; }
        public PolylineCurve Poly { get; set; }
        public bool selected { get; set; }
        public Voxel(Point3d p, Brep m, PolylineCurve poly, int x_, int y_, int z_)
        {
            this.CenterPoint = p;
            this.x = x_;
            this.y = y_;
            this.z = z_;
            this.Mass = m;
            this.Poly = poly;
            this.selected = false;
        }
        public List<int> getIndexAsList()
        {
            List<int> I = new List<int> { this.x, this.y, this.z };
            return I;
        }
    }
}
