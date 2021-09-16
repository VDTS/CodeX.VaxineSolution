using System.IO;
using System.Threading.Tasks;

namespace VaxineApp.AndroidNativeApi
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
