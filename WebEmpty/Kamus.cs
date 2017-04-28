using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEmpty
{
    public class Kamus
    {
        private static ArrayList kamus = new ArrayList();
        
        public static void addKamus()
        {
            //Baca file eksternal
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\AZIS\Documents\Visual Studio 2017\Projects\WebEmpty\WebEmpty\db_kamus.txt");
            foreach (var line in lines)
            {
                kamus.Add(line);
            }
        }

        public static ArrayList getIsiKamus()
        {
            return kamus;
        }

        public static bool isOnKamus(string key)
        {
            int id = kamus.BinarySearch(key);
            return (id < 0) ? false : true ;
        }
    }
}