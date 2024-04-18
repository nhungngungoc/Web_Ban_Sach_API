using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.ProductRepo;
using WebBanSachRepository.UserRepo;
using WebBanSachService.Category;
using WebBanSachService.Common;
using WebBanSachService.User;

namespace WebBanSachService.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IProductRepo repository)
        {
            _repository = repository;
            this._mapper = mapper;
        }
        public bool Add(ProductDto dto)
        {
            return this._repository.Add(_mapper.Map<WebBanSachModel.Entity.Product>(dto));
        }

        public bool Delete(Guid id)
        {
            return this._repository.Delete(id);
        }
        public bool softDelete(Guid id)
        {
            return this._repository.softDelete(id);
        }
        public bool Update(ProductDto dto)
        {
            return _repository.Update(_mapper.Map<WebBanSachModel.Entity.Product>(dto));
        }

        public PageListResultBO<ProductDto> GetAll(ProductQuery? query)
        {
            int begin = (query.page * query.limit) - query.limit;
            var list = _mapper.Map<List<ProductDto>>(_repository.GetAll());
            if (query.keyword != string.Empty && query.keyword!=null)
            {
                list = list.Where(x => x.TenSP.Contains(query.keyword.Trim())).ToList();
            }
            if(query.categoryId!=null)
            {
                list = list.Where(x => x.CategoryId.Equals(Guid.Parse(query.categoryId))).ToList();
            }    
            var resulteModel = new PageListResultBO<ProductDto>();
            resulteModel.items = list.Skip(begin).Take(query.limit).ToList();
            resulteModel.totalItems = list.Count();
            return resulteModel;
        }
        public ProductDto GetById(Guid id)
        {
            return _mapper.Map<ProductDto>(_repository.GetbyId(id));
        }

    }
}
