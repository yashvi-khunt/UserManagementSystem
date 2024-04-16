using Alms.DAL.ViewModels;
using AutoMapper;
using LS.BLL.Repositories;
using LS.BLL.SQLRepository;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BLL.Services
{
    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IProcedureManager _procedureManager;
        private readonly IMapper _mapper;

        public LoginHistoryService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService, IProcedureManager procedureManager, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
            _procedureManager = procedureManager;
            _mapper = mapper;
        }

        public Task<VMGetLoginHistories> GetAllLoginHistories(GetLoginHistoryInputModel getLoginHistoryInputModel)
        {
            var spParameters = new GetLoginHistoryInputModel
            {

                Page = getLoginHistoryInputModel.Page,
                PageSize = getLoginHistoryInputModel.PageSize,
                Field = getLoginHistoryInputModel.Field,
                Sort = getLoginHistoryInputModel.Sort,
                Text = getLoginHistoryInputModel.Text ?? "",
                ToDate = getLoginHistoryInputModel.ToDate ?? "",
                FromDate = getLoginHistoryInputModel.FromDate ?? "",
                UserIds = getLoginHistoryInputModel.UserIds ?? "",

            };

            var loginHistories = _procedureManager.ExecStoreProcedureMulResults<StoredProcedureCommonModel, VMSpGetLoginHistories>(StoredProcedure.GetLoginHistory, spParameters);

            var countData = loginHistories.Item1[0].Count;
            var loginHistoriesData = loginHistories.Item2;


            VMGetLoginHistories getEmployees = new VMGetLoginHistories
            {
                Count = (int)countData,
                LoginHistories = loginHistoriesData
            };

            return Task.FromResult(getEmployees);
        }


        public Task<VMAddLoginHistoryResponse> AddLoginHistory(VMAddLoginHistory vMAddLoginHistory)
        {
            var loginHistory = _procedureManager.ExecStoreProcedure<VMAddLoginHistoryResponse>(StoredProcedure.AddLoginHistory, vMAddLoginHistory);

            return Task.FromResult(loginHistory[0]);
        }
    }
}
