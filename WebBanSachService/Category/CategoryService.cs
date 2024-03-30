using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.CategogyRepo;
using WebBanSachRepository.common;
using WebBanSachRepository.UserRepo;
using WebBanSachService.Common;
using WebBanSachService.User;

namespace WebBanSachService.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategogyRepo _repository;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, ICategogyRepo repository)
        {
            _repository = repository;
            this._mapper = mapper;
        }
        public bool Add(CategoryDto dto)
        {
            return this._repository.Add(_mapper.Map<WebBanSachModel.Entity.Category>(dto));
        }

        public bool Delete(Guid id)
        {
            return this._repository.Delete(id);
        }
        public bool softDelete(Guid id)
        {
            return this._repository.softDelete(id);
        }
        public bool Update(CategoryDto dto)
        {
            return _repository.Update(_mapper.Map<WebBanSachModel.Entity.Category>(dto));
        }

        public PageListResultBO<CategoryDto> GetAll(CategoryQuery? query)
        {
            int begin = (query.page * query.limit) - query.limit;
            var list = _mapper.Map<List<CategoryDto>>(_repository.GetAll());
            if (query.keyword != string.Empty)
            {
                list = list.Where(x => x.Name.Equals(query.keyword)).ToList();
            }
            var resulteModel = new PageListResultBO<CategoryDto>();
            resulteModel.items = list.Skip(begin).Take(query.limit).ToList();
            resulteModel.totalItems = list.Count();
            return resulteModel;
        }
        public CategoryDto GetById(Guid id)
        {
            return _mapper.Map<CategoryDto>(_repository.GetbyId(id));
        }

        public List<SelectListItem> GetDropdown(string displayMember, string valueMember, object selected = null)
        {
            return _repository.GetDropdown(displayMember, valueMember, selected);
        }
    }
}
