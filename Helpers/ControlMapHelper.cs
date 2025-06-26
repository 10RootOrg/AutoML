using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMLGUI.Helpers
{
    internal class ControlMapHelper
    {
        public static void FillControlMap(Control parent, Dictionary<string, Control> controlMap)
        {
            foreach (Control control in parent.Controls)
            {
                // Exclude Label controls
                if (!string.IsNullOrEmpty(control.Name) && !(control is Label))
                {
                    controlMap[control.Name] = control; // ✅ Store only non-label controls
                }

                // 🔍 Recursively check inside Panels, GroupBoxes, etc.
                if (control.HasChildren)
                {
                    FillControlMap(control, controlMap);
                }
            }
        }

    }
}
