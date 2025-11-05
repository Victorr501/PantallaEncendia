using System;
using System.Diagnostics.Metrics;
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

    //Click el cursor
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;

    static bool run = true;

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

        // Hola mundo
        // Hilo para mover el ratón en círculo y hacer clic
        var mouseThread = new Thread(() =>
        {
            int counter = 0;
            while (run) // Mientras no se presione Enter
            {
                int x = centerX + (int)(radius * Math.Cos(angle));
                int y = centerY + (int)(radius * Math.Sin(angle));

                SetCursorPos(x, y);
                angle += step;


                //Cada X ciclos hacer click
                counter++;
                if (counter % 1000 == 0)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                Thread.Sleep(10); // Controla la velocidad
            }
        });

        mouseThread.IsBackground = true;
        mouseThread.Start();

        Console.ReadLine();
        run = false;

        SetThreadExecutionState(ES_CONTINUOUS);
    }

    
}