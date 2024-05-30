#if WINDOWS
using System.Runtime.InteropServices;
#endif

namespace AppServer.Util
{
    public static class PowerUtilities
    {
#if WINDOWS
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        private static AutoResetEvent _event = new AutoResetEvent(false);

        public static void PreventPowerSave()
        {
            (new TaskFactory()).StartNew(() =>
            {
                SetThreadExecutionState(
                    EXECUTION_STATE.ES_CONTINUOUS
                    | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                _event.WaitOne();

            },
                TaskCreationOptions.LongRunning);
        }

        public static void Shutdown()
        {
            _event.Set();
        }
#endif
    }
}
