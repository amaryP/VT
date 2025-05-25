using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSymbolsTools
{
    class ServiceSymbol
    {
        public  ServiceSymbol()
        {
            //
        }

        public  ServiceSymbol(String ServiceNameIn)
        {
            //
            if (ServiceNameIn=="ListeTotale")
                {
                ListeSymbols();
                 }
           
        }
        public List<string>  ListeSymbols()
        {
            string[] symbols =

                {
"BTC",
"ETH",
"BCH",
"XRP",
"EOS",
"LTC",
"TRX",
"ETC",
"LINK",
"XLM",
"ADA",
"XMR",
"DASH",
"ZEC",
"XTZ",
"BNB",
"ATOM",
"ONT",
"IOTA",
"BAT",
"VET",
"NEO",
"QTUM",
"IOST",
"THETA",
"ALGO",
"ZIL",
"KNC",
"ZRX",
"COMP",
"OMG",
"DOGE",
"SXP",
"KAVA",
"BAND",
"RLC",
"WAVES",
"MKR",
"SNX",
"DOT",
//"DEFI",
"YFI",
"BAL",
"CRV",
"TRB",
"YFII",
"RUNE",
"SUSHI",
"SRM",
"BZRX",
"EGLD",
"SOL",
"ICX",
"STORJ",
"BLZ",
"UNI",
"AVAX",
"FTM",
"HNT",
"ENJ",
"FLM",
"TOMO",
"REN",
"KSM",
"NEAR",
"AAVE",
"FIL",
"RSR",
"LRC",
"MATIC",
"OCEAN",
"CVC",
"BEL",
"CTK",
"AXS",
"ALPHA",
"ZEN",
"SKL",
"GRT",
"1INCH",
"AKRO",
"CHZ",
"SAND",
"ANKR",
"LUNA",
"BTS",
"LIT",
"UNFI",
"DODO",
"REEF",
"RVN",
"SFP",
"XEM",
"COTI",
"CHR",
"MANA",
"ALICE",
"HBAR",
"ONE",
"LINA",
"STMX",
"DENT",
"CELR",
"HOT",
"MTL",
"OGN",
"BTT",
"NKN",
"SC",
"DGB",
"SHIB",
"ICP"
            };
            /*
                {
                "BTC",
                "ETH",
                //"BNB"
                "ADA",
                "DOGE",
                //"USDT",
                "XRP",
                "DOT",
                "ICP",
                "BCH",
                "LTC",
                "UNI",
                "LINK",
                "XLM",
                "SOL",
                "USDC",
                "MATIC",
                "VET",
                "ETC",
                "THETA",
                "EOS",
                "TRX",
                "AAVE",
                //"WBTC",
                //"BUSD",
                "FIL",
                //"SHIB",==> valeur trop faible pour un affichage decimal...
                "XMR",
                "LUNA",
                //"BSV",
                //"KLAY" ,
                //"MIOTA",
                "XTZ",
                "FTT",
                //"HT",
                //"DAI",
                "MKR",
                "KSM",
                "RUNE",
                "ATOM",                //==> non environ les 40 premieres capitalisations 
                "SUSHI"

            };
            */
            //charge les symbols
            List<string> Listsymbols = new List<string>();
            Listsymbols.AddRange(symbols);
            return Listsymbols;
        }
        //private string
    }

}