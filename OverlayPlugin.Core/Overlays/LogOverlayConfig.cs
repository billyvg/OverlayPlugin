using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowMage.OverlayPlugin.Overlays
{
    [Serializable]
    public class LogOverlayConfig : OverlayConfigBase
    {
        public LogOverlayConfig(string name) : base(name)
        {

        }

        // XmlSerializer用
        private LogOverlayConfig() : base(null)
        {

        }

        public override Type OverlayType
        {
            get { return typeof(LogOverlay); }
        }
    }
}
