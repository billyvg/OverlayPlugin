using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowMage.OverlayPlugin.Overlays
{
    class LogOverlayAddon : IOverlayAddon
    {
        public string Name
        {
            get { return "Log"; }
        }

        public string Description
        {
            get { return ""; }
        }

        public Type OverlayType
        {
            get { return typeof(LogOverlay); }
        }

        public Type OverlayConfigType
        {
            get { return typeof(LogOverlayConfig); }
        }

        public Type OverlayConfigControlType
        {
            get { return typeof(LogConfigPanel); }
        }

        public IOverlay CreateOverlayInstance(IOverlayConfig config)
        {
            return new LogOverlay((LogOverlayConfig)config);
        }

        public IOverlayConfig CreateOverlayConfigInstance(string name)
        {
            return new LogOverlayConfig(name);
        }

        public System.Windows.Forms.Control CreateOverlayConfigControlInstance(IOverlay overlay)
        {
            return new LogConfigPanel((LogOverlay)overlay);
        }

        public void Dispose()
        {

        }
    }
}
