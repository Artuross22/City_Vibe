using WrapperAPI.APIs.CryptoAPI.Models;


namespace WrapperAPI.APIs.CryptoAPI.Interfaces
{
    public interface ICryptoApiAdapter
    {
        Task<CryptoCoinAPI> CryptoHandlerAPIsd();
    }
}
