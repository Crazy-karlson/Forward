using System;
using System.IO;
namespace Forward
{
    class Program
    {
        static void Main(string[] args)
        {
            double To=0; // Т среды
            double I = 10; // Момент инерции
            double[] Vk = new double[6] { 0, 75, 150, 200, 250, 300 }; // Скорость колена
            double[] M = new double[6] { 20, 75, 100, 105, 75, 0 }; // Крутящий момент
            double Tp = 110; // Tперегрева
            double Hm = 0.01;  // Коэффициент зависимости скорости нагрева от крутящего момента
            double Hv = 0.0001;  // Коэффициент зависимости скорости нагрева от скорости вращения коленвала
            double C = 0.1; // Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
            Clear();
            Console.WriteLine("VVwdite To");
            try
            {
                To = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(One(To,Vk, I, M, Tp, Hm, Hv, C));
            }
            catch
            {
                Console.WriteLine("Vu vveli ne chislo");
            }

            
            Console.ReadKey();
        }

        public static string One(double To, double[] Vk, double I, double[] M, double Tp, double Hm, double Hv, double C) // симуляция процесса
        {
            double Vh = 0;
            double a = 0;
            double V = 0;
            double Vc = 0;
            double Td = To;
            int y=0;
            double t=0;
            double[] z=new double[Vk.Length] ;
            
            //t[i] = (I * (Vk[i] - Vk[i - 1])) / M[i];
            for(int i=0; i<z.Length; i++) // находим промежутки времени цикл
            {
                if (i!= 0)
                    z[i] = z[i-1]+(I * (Vk[i] - Vk[i - 1])) / M[i - 1]; 
                else
                    z[i] = 0;
                

            }

            while (Td < Tp)// посекундный расчет температуры
            {
                if (t < z[1])
                    y = 0;
                if ((t < z[2]) & (t > z[1]))
                    y = 1;
                if ((t < z[3]) & (t > z[2]))
                    y = 2;
                if ((t < z[4]) & (t > z[3]))
                    y = 3;
                if ((t < z[5]) & (t > z[4]))
                    y = 4;
                

                a = M[y] / I;
                V = Vk[y] + a * (t - z[y]);

                Vh = M[y] * Hm + V * V * Hv;
                
                Vc = C * (To - Td);
                Td = Td + (Vh-Vc);
                
                t++;

                Write($"{Td}\t{V}");     // запись логов        
            }
            string p = t + "";
            return p; 

        }
        public static void Write(string text) //запись логов
        {
            string writePath = @"C:\Users\Игорь\source\repos\Forward\logi\iogi.txt";

            try
            {
                

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

            }
            catch
            {

            }
        } 
        public static void Clear()  //очищение предыдущей записи
        {
            string writePath = @"C:\Users\Игорь\source\repos\Forward\logi\iogi.txt";

            
            try
            {
                

                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    
                }

            }
            catch
            {

            }
        }

  



    }
}
