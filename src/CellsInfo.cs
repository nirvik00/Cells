using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Cells
{
    public class CellsInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Cells";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override string Description
        {
            get
            {
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("385eb2f5-25df-42bd-8ccb-bd24ace188c1");
            }
        }

        public override string AuthorName
        {
            get
            {
                return "nirvik saha, dennis shelden";
            }
        }
        public override string AuthorContact
        {
            get
            {
                return "sahan@rpi.edu, sheldd@rpi.edu";
            }
        }
    }
}
