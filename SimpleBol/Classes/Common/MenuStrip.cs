namespace SimpleBol.Classes.Common
{
    public class MenuSettings
    {
        public static void EnableToolstripItem(string menuName)
        {
            if (TryGetMenuStrip(out var menuStrip) && menuStrip.Items[menuName] is ToolStripItem item)
            {
                item.Enabled = true;
            }

            Application.DoEvents();
        }

        public static void EnableMenuItem(string menuName, string menuItemName)
        {
            if (TryGetMenuItem(menuName, out var menuItem) && menuItem.DropDownItems[menuItemName] is ToolStripItem item)
            {
                item.Enabled = true;
            }

            Application.DoEvents();
        }

        public static void EnableMenuItem(string l1MenuItem, string l2MenuItem, string menuItemName)
        {
            if (TryGetMenuItem(l1MenuItem, out var menuItem) &&
                menuItem.DropDownItems[l2MenuItem] is ToolStripMenuItem childMenuItem &&
                childMenuItem.DropDownItems[menuItemName] is ToolStripItem item)
            {
                item.Enabled = true;
            }
        }

        public static void DisableToolStripItem(string menuName)
        {
            if (TryGetMenuStrip(out var menuStrip) && menuStrip.Items[menuName] is ToolStripItem item)
            {
                item.Enabled = false;
            }

            Application.DoEvents();
        }

        public static void DisableMenuItem(string menuName, string menuItemName)
        {
            if (TryGetMenuItem(menuName, out var menuItem) && menuItem.DropDownItems[menuItemName] is ToolStripItem item)
            {
                item.Enabled = false;
            }
        }

        private static bool TryGetMenuStrip(out MenuStrip menuStrip)
        {
            menuStrip = null!;

            if (Application.OpenForms["MainForm"] is not Form form ||
                form.Controls["MenuStrip1"] is not MenuStrip foundMenuStrip)
            {
                return false;
            }

            menuStrip = foundMenuStrip;
            return true;
        }

        private static bool TryGetMenuItem(string menuName, out ToolStripMenuItem menuItem)
        {
            menuItem = null!;

            if (!TryGetMenuStrip(out var menuStrip) || menuStrip.Items[menuName] is not ToolStripMenuItem foundMenuItem)
            {
                return false;
            }

            menuItem = foundMenuItem;
            return true;
        }
    }
}
