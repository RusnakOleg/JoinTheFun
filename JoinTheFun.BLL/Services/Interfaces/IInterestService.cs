﻿using JoinTheFun.BLL.DTO.Interests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services.Interfaces
{
    public interface IInterestService
    {
        Task<IEnumerable<InterestDto>> GetAllAsync();
        Task CreateAsync(CreateInterestDto dto);
    }
}
