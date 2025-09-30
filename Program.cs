using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    // Funcion de windows
    [DllImport("kernel32.dll")]
    static extern uint SetThreadExecutionState(uint esFlags);

    //Flags que vamos a usar
    const uint ES_CONTINUOUS = 0x80000000;
    const uint ES_SYSTEM_REQUIRED = 0x00000002;

    static void Main()
    {
        SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
        Console.WriteLine("El sistema no entrará en suspensión mientras esta app esta en ejecución.");
        Console.WriteLine("Presiona Enter para salir...");

        Console.ReadLine();

        SetThreadExecutionState(ES_CONTINUOUS);
    }
}