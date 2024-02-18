using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBank.Dependencies.GenericRepository.Interfaces;
using ABCBank.Domain.Models;


namespace ABCBank.Dependencies.GenericRepository.Interfaces
{
    public interface IUserTokenRepository:IGenericRepository<UserToken>
    {
        
    }
}