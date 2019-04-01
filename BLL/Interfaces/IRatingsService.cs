using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IRatingsService
    {
        IEnumerable<RaitingDTO> GetAllRaitingsForUser(string userId);
        IEnumerable<RaitingDTO> GetRaitingsForUser(string userId, int courseId);
        void AddRateToUser(RaitingDTO raitingDTO);
    }
}
