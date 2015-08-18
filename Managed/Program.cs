using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Managed
{
    // Uncomment to fix the bug
    // [StructLayout(LayoutKind.Sequential, Pack = 4)]
    struct CALLBACK_OUTPUT
    {
        public IntPtr ErrorCodeArray;
        public ulong NumberOfErrors;
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    delegate int FROB_WIDGET_CALLBACK(int OperationCode, ref CALLBACK_OUTPUT CallbackOutput);

    class NativeMethods
    {
        [DllImport(@"C:\Temp\PInvokeBug\Release\Native.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void FrobWidgetWithCallback(
            string FrobName,
            FROB_WIDGET_CALLBACK Callback
            );
    }

    class Program
    {
        static int Callback(int OperationCode, ref CALLBACK_OUTPUT CallbackOutput)
        {
            Console.WriteLine("Callback invoked with opcode {0}", OperationCode);
            CallbackOutput.ErrorCodeArray = IntPtr.Zero;
            CallbackOutput.NumberOfErrors = 0;
            return 0;
        }

        static void Main(string[] args)
        {
            FROB_WIDGET_CALLBACK callback = new FROB_WIDGET_CALLBACK(Callback);
            NativeMethods.FrobWidgetWithCallback("Simple Frob", callback);
        }
    }
}
