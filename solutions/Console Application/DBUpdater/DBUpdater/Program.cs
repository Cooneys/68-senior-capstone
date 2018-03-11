using System;
using System.Collections.Generic;

namespace DBUpdater
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            DBConnect dbConnection = new DBConnect();
            List<string> storedTickers;



            storedTickers = dbConnection.Select("Investments", "tickersymbol");
            //Console.WriteLine(storedTickers[0]);
        }
    }
}
