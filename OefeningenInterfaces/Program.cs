using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OefeningenInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Rechthoek[] Feguren = new Rechthoek[10];
            Feguren[0] = new Rechthoek(2, 9);
            Feguren[1] = new Rechthoek(2, 7);
            Feguren[2] = new Rechthoek(2, 5);
            Feguren[3] = new Rechthoek(2, 10);
            Feguren[4] = new Rechthoek(2, 6);
            Feguren[5] = new Rechthoek(2, 2);
            Feguren[6] = new Rechthoek(2, 8);
            Feguren[7] = new Rechthoek(2, 4);
            Feguren[8] = new Rechthoek(2, 3);
            Feguren[9] = new Rechthoek(2, 1);
            foreach (var item in Feguren)
            {
                Console.WriteLine(item.ToonOppervlakte()); 
            }
            Console.WriteLine("-------------");
            Array.Sort(Feguren);
            foreach (var item in Feguren)
            {
                Console.WriteLine(item.ToonOppervlakte());
            }
            Console.ReadLine();
        }
    }
    class Rechthoek:IComparable
    {
        private int lengte;

        public int Lengte
        {
            get { return lengte; }
            set
            {
                if (value >= 1)
                    lengte = value;
                else
                    lengte = -1;
            }
        }
        private int breedte;

        public int Breedte
        {
            get { return breedte; }
            set
            {
                if (value >= 1)
                    breedte = value;
                else
                    breedte = -1;
            }
        }

        public Rechthoek(int lengte, int breedte)
        {
            this.lengte = lengte;
            this.breedte = breedte;
        }

        public int  ToonOppervlakte()
        {
            if (lengte >= 1 && breedte >= 1)
                return(lengte * breedte);
            return -1;
        }

        public int CompareTo(object obj)
        {
            Rechthoek temp = obj as Rechthoek;
            return ToonOppervlakte().CompareTo(temp.ToonOppervlakte());
        }
    }
}
