using Wireframe.Backend.Models;

namespace Wireframe.Backend.Repositories
{
    public interface IWireFrameRepository
    {
        Task<IEnumerable<WireframeModel>> GetAll();

        Task<WireframeModel> Post(WireframeModel wireframeModel);

        Task<WireframeModel> GetById(string id);

        Task<Device> Put(string id, Device device);

        Task<Device> ModifyDevice(string id, Device device);

    }
}
