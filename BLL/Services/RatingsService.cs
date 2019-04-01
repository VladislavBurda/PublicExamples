using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class RatingsService : IRatingsService
    {
        IUnitOfWork _unitOfWork;
        public RatingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddRateToUser(RaitingDTO raitingDTO)
        {
            _unitOfWork.Raitings.Add(new Raitings
            {
                CourseId = raitingDTO.CourseId,
                UserId = raitingDTO.UserId,
                Grade = raitingDTO.Grade
            });
            _unitOfWork.Save();
        }

        public IEnumerable<RaitingDTO> GetAllRaitingsForUser(string userId)
        {
            return _unitOfWork.Raitings.Get(x => x.UserId == userId)
                .Select(z => new RaitingDTO
                {
                    CourseId = z.CourseId,
                    Grade = z.Grade,
                    UserId = z.UserId
                });
        }

        public IEnumerable<RaitingDTO> GetRaitingsForUser(string userId, int courseId)
        {
            return _unitOfWork.Raitings.Get(x => x.UserId == userId && x.CourseId == courseId)
                .Select(z => new RaitingDTO
                {
                    CourseId = z.CourseId,
                    Grade = z.Grade,
                    UserId = z.UserId
                });
        }
    }
}
