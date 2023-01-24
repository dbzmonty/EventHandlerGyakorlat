using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHandlerGyakorlat
{
    public delegate void EventHandlerValueChanged(int value);
    public delegate void EventHandlerValueReached();

    public class Persely
    {
        private int egyenleg;

        public event EventHandlerValueChanged valueChanged;
        public event EventHandlerValueReached valueReached;

        public Persely()
        {
            egyenleg = 0;
        }

        public int Egyenleg
        {
            set
            {
                this.egyenleg = value;

                if (500 <= egyenleg) this.valueReached();

                this.valueChanged(egyenleg);
            }
            get
            {
                return this.egyenleg;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Persely obj = new Persely();
            obj.valueChanged += new EventHandlerValueChanged(obj_valueChanged);
            obj.valueReached += new EventHandlerValueReached(obj_valueReached);

            string str;

            do
            {
                Console.WriteLine("Mennyit teszel be a perselybe? ");
                str = Console.ReadLine();
                if (!str.Equals("exit"))
                {
                    int ertek;
                    bool ervenyes = int.TryParse(str, out ertek);

                    if (ervenyes)
                    {
                        int eddigiEgyenleg = obj.Egyenleg;
                        obj.Egyenleg = eddigiEgyenleg + ertek;
                    }
                    else
                    {
                        Console.WriteLine("Hülye vagy?");
                    }
                }
            }
            while (!str.Equals("exit"));
            Console.WriteLine("Program vege!");
            Console.ReadLine();
        }

        private static void obj_valueChanged(int value)
        {
            Console.WriteLine("Az uj egyenleg: " + value.ToString());
        }

        private static void obj_valueReached()
        {
            Console.WriteLine("Elerted a kituzott celt! :)");
        }
    }
}
