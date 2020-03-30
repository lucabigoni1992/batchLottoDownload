using System;
using libraryLotto;
namespace batchLottoDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            batchDownloadData lottoData = new batchDownloadData();
            lottoData.downloadAllLotto();
        }
    }
}
