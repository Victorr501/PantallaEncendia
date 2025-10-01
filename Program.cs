using System;
using System.Drawing;
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

    //Mover el cursor
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    static void Main()
    {
        SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
        Console.WriteLine("El sistema no entrará en suspensión mientras esta app esta en ejecución.");
        Console.WriteLine("Presiona Enter para salir...");

        // Parámetros del círculo
        int centerX = 800;  // Ajusta según tu pantalla
        int centerY = 450;
        int radius = 100;
        double angle = 0;
        double step = 0.05; // Velocidad del movimiento

        // Hilo para mover el ratón en círculo
        var mouseThread = new Thread(() =>
        {
            while (!Console.KeyAvailable) // Mientras no se presione Enter
            {
                int x = centerX + (int)(radius * Math.Cos(angle));
                int y = centerY + (int)(radius * Math.Sin(angle));

                SetCursorPos(x, y);
                angle += step;

                Thread.Sleep(10); // Controla la velocidad
            }
        });

        mouseThread.IsBackground = true;
        mouseThread.Start();

        Console.ReadLine();

        SetThreadExecutionState(ES_CONTINUOUS);
    }

    
}