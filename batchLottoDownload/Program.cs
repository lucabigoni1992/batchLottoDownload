using System;
using libraryLotto;
namespace batchLottoDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n --- batchLottoDownload");
            batchDownloadData lottoData = new batchDownloadData();
            lottoData.downloadAllLotto();
        }
    }
}
