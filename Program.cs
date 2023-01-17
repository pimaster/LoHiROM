using System;
using System.Collections.Generic;
using System.IO;

namespace LoHiROMConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool lowToHigh = true;
                Console.WriteLine("LoHiROMConvert - Convert SNES Games from LoROM to HiROM");
                if(args[0].StartsWith("-")){
                    switch(args[0]){
                        case "-l2h": lowToHigh = true; break;
                        case "-h2l": lowToHigh = false; break;
                    }
                }
                string infile = args[args.Length-2];
                string outfile = args[args.Length-1];

                byte[] lFile = File.ReadAllBytes(infile);

                byte[] oFile = null;
                if(lowToHigh){
                    Console.WriteLine("Converting Low to High");
                    oFile = ProcessLowToHigh(lFile);
                }else{
                    Console.WriteLine("Converting High to Low");
                    oFile = ProcessHighToLow(lFile);
                }

                File.WriteAllBytes(outfile, oFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Usage: LoHiROMConvert <-l2h|-h2l> [InFilePath] [OutFilePath]");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static byte[] ProcessLowToHigh(byte[] source){
            byte[] result = new byte[source.Length * 2];

            int div = 0x8000;
            int pcs = (source.Length / div);

            int hirompos = 0;
            int pcpos = 0;

            for (int c = 0; c < pcs; c++)
            {
                for (int d = 0; d < div; d++)
                {
                    pcpos = d + (c * div);
                    hirompos = d + (c * 0x10000);
                    result[hirompos] = ((c == 0) ? (byte)0xFF : source[pcpos]);
                    result[hirompos + div] = source[pcpos];
                }
            }
            return result;
        }
        
        static byte[] ProcessHighToLow(byte[] source){
            byte[] result = new byte[source.Length / 2];

            int div = 0x10000;
            int pcs = (source.Length / div);

            int lorompos = 0;
            int pcpos = 0;

            for (int c = 0; c < pcs; c++)
            {
                for (int d = 0; d < 0x8000; d++)
                {
                    pcpos = d + (c * div);
                    if(c==0)
                        pcpos+= 0x8000;
                    lorompos = d + (c * 0x8000);
                    //result[lorompos] = ((c == 0) ? (byte)0xFF : source[pcpos]);
                    result[lorompos] = source[pcpos];
                }
            }

            return result;
        }
    }
}
