using System.Threading.Tasks;

namespace Walt.TestMicroServoces.Webapi
{
    public interface IOrderService
    {
        Task<string> GetOrder();
    }
}