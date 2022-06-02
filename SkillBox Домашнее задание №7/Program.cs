using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillBox_Домашнее_задание__7
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"data.txt";
            
            Repository rep = new Repository(path);
            
            

            Console.ReadKey();
        }
    }
}
