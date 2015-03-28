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

        IList<SerializableTimerFrameEntry> activatedTimers;

        public LogOverlay(LogOverlayConfig config)
            : base(config, config.Name)
        {
            this.activatedTimers = new List<SerializableTimerFrameEntry>();

            ActGlobals.oFormActMain.OnLogLineRead += (test, test2) =>
                {

                };
            ActGlobals.oFormSpellTimers.OnSpellTimerNotify += (t) =>
            {
                /*
                lock (this.activatedTimers)
                {
                    var timerFrame = activatedTimers.Where(x => x.Original == t).FirstOrDefault();
                    if (timerFrame == null)
                    {
                        timerFrame = new SerializableTimerFrameEntry(t);
                        this.activatedTimers.Add(timerFrame);
                    }
                    else
                    {
                        timerFrame.Update(t);
                    }
                    foreach (var Log in t.Logs)
                    {
                        var timer = timerFrame.Logs.Where(x => x.Original == Log).FirstOrDefault();
                        if (timer == null)
                        {
                            timer = new SerializableLogEntry(Log);
                            timerFrame.Logs.Add(timer);
                        }
                    }
                }
                 */
            };
        }

        protected override void Update()
        {
            try
            {
                var updateScript = CreateEventDispatcherScript();

                if (this.Overlay != null &&
                    this.Overlay.Renderer != null &&
                    this.Overlay.Renderer.Browser != null)
                {
                    this.Overlay.Renderer.Browser.GetMainFrame().ExecuteJavaScript(updateScript, null, 0);
                }

            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, "Update: {1}", this.Name, ex);
            }
        }

        private void RemoveExpiredEntries()
        {
           
        }

        internal string CreateJsonData()
        {
            lock (this.activatedTimers)
            {
                RemoveExpiredEntries();
            }

            using (var ms = new MemoryStream())
            {
                lock (this.activatedTimers)
                {
                    RemoveExpiredEntries();
                    jsonSerializer.WriteObject(ms, activatedTimers);
                }

                var result = Encoding.UTF8.GetString(ms.ToArray());

                if (!string.IsNullOrWhiteSpace(result))
                {
                    return string.Format("{0}{1}{2}", "{ timerFrames: ", result, "}");
                }
                else
                {
                    return "";
                }
            }
        }

        private string CreateEventDispatcherScript()
        {
            return "var ActXiv = " + this.CreateJsonData() + ";\n" +
                   "document.dispatchEvent(new CustomEvent('onOverlayDataUpdate', ActXiv));";
        }
    }
}
