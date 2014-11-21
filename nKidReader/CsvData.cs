using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nKidReader
{
    class CsvData
    {
        public string nfcId { get; set; }
        public string mrsId { get; set; }

        public CsvData()
        {

        }

        public CsvData(string mrs, string nfc)
        {
            this.nfcId = nfc;
            this.mrsId = mrs;
        }
    }
}
