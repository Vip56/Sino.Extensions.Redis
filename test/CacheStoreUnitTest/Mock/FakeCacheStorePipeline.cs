using Sino.CacheStore.Handler;
using System.Text;
using System.Threading.Tasks;

namespace CacheStoreUnitTest
{
    public class FakeCacheStorePipeline : CacheStorePipeline
    {
        public byte[] Request { get; set; }

        public byte[] Response { get; set; }

        public string ResponseString
        {
            get
            {
                return Encoding.GetString(Response);
            }
        }

        public string RequestString
        {
            get
            {
                return Encoding.GetString(Request);
            }
        }

        public Encoding Encoding { get; set; }

        public FakeCacheStorePipeline(byte[] response)
        {
            Response = response;
        }

        public FakeCacheStorePipeline(string response, Encoding encoding = null)
        {
            Encoding = encoding ?? Encoding.UTF8;
            Response = Encoding.GetBytes(response);
        }

        public override Task<bool> ConnectAsync()
        {
            return Task.FromResult(true);
        }

        public override bool IsConnected => true;

        public override Task<byte[]> SendAsnyc(byte[] write)
        {
            Request = write;
            return Task.FromResult(Response);
        }
    }
}
