using Diplomna.Common;
using Diplomna.Common.Dtos;

namespace Diplomna.Services.Interfaces
{
    public interface IBarcodeService
    {
        public Task<Result<bool>> LogScanInfo(BarcodeDto scanInfo);
    }
}
