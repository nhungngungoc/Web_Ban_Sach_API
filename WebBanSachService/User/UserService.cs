using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.UserRepo;
using WebBanSachService.Common;

namespace WebBanSachService.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, IUserRepo repository)
        {
            _repository = repository;
            this._mapper = mapper;
        }
        public bool Add(UserDto dto)
        {
            return this._repository.Add(_mapper.Map<WebBanSachModel.Entity.User>(dto));
        }

        public bool Delete(Guid id)
        {
            return this._repository.Delete(id);
        }
        public bool softDelete(Guid id)
        {
            return this._repository.softDelete(id);
        }
        public bool Update(UserDto dto)
        {
            return _repository.Update(_mapper.Map<WebBanSachModel.Entity.User>(dto));
        }

        public PageListResultBO<UserDto> GetAll(UserQuery? query)
        {
            int begin = (query.page * query.limit) - query.limit;
            var list = _mapper.Map<List<UserDto>>(_repository.GetAll());
            if (query.keyword != string.Empty)
            {
                list = list.Where(x => x.Email.Equals(query.keyword)).ToList();
            }
            var resulteModel = new PageListResultBO<UserDto>();
            resulteModel.items = list.Skip(begin).Take(query.limit).ToList();
            resulteModel.totalItems = list.Count();
            return resulteModel;
        }
        public UserDto GetById(Guid id)
        {
            return _mapper.Map<UserDto>(_repository.GetbyId(id));
        }
        public bool checkUser(string username)
        {
            var data = _repository.FindOne(x => x.Email == username);
            if (data != null)
                return true;
            return false;
        }
    }
}
