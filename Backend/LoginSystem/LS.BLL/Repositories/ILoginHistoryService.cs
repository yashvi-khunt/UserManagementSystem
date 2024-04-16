using Alms.DAL.ViewModels;
using LS.DAL.ViewModels;


namespace LS.BLL.Repositories
{
    public interface ILoginHistoryService
    {
         Task<VMGetLoginHistories> GetAllLoginHistories(GetLoginHistoryInputModel getLoginHistoryInputModel);
        Task<VMAddLoginHistoryResponse> AddLoginHistory(VMAddLoginHistory vMAddLoginHistory);
    }
}
