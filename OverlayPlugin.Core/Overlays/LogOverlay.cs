using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RainbowMage.OverlayPlugin.Overlays
{
    public class LogOverlay : OverlayBase<LogOverlayConfig>
    {
        static DataContractJsonSerializer jsonSerializer =
            new DataContractJsonSerializer(typeof(List<SerializableTimerFrameEntry>));

        public LogOverlay(LogOverlayConfig config)
            : base(config, config.Name)
        {
            ActGlobals.oFormActMain.OnLogLineRead += (isImport, logInfo) =>
            {
                /* logInfo: detectedTime, detectedType, detectedZone, inCombat, logLin */
                var importStr = "false";
                if (isImport)
                {
                    importStr = "true";
                }

                var updateScript = "document.dispatchEvent(new CustomEvent('onOverlayLogUpdate', { detail: {isImport: " + importStr + ", logInfo: {timestamp: '" + logInfo.detectedTime + "', logLine: '" + Util.CreateJsonSafeString(logInfo.logLine) + "'}}}));";

                if (this.Overlay != null &&
                    this.Overlay.Renderer != null &&
                    this.Overlay.Renderer.Browser != null)
                {
                    this.Overlay.Renderer.Browser.GetMainFrame().ExecuteJavaScript(updateScript, null, 1);
                }
            };
        }

        protected override void Update()
        {
           
        }

        private void RemoveExpiredEntries()
        {
           
        }

        internal string CreateJsonData()
        {
            return "";
        }

        private string CreateEventDispatcherScript()
        {
            return "";
        }
    }
}
