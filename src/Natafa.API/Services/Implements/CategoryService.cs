using AutoMapper;
using Natafa.Api.Helper;
using Natafa.Api.Models.CategoryModel;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;
using Natafa.Domain.Paginate;
using Natafa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.ViewModels;

namespace Natafa.Api.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<MethodResult<CategoryResponse>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _uow.GetRepository<Category>().SingleOrDefaultAsync(
                    predicate: c => c.CategoryId == id
                );

                if (category == null)
                {
                    return new MethodResult<CategoryResponse>.Failure("Category not found", StatusCodes.Status404NotFound);
                }

                var result = _mapper.Map<CategoryResponse>(category);
                return new MethodResult<CategoryResponse>.Success(result);
            }
            catch (Exception ex)
            {
                return new MethodResult<CategoryResponse>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<IEnumerable<CategoryResponse>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _uow.GetRepository<Category>().GetListAsync(                    
                    selector: c => _mapper.Map<CategoryResponse>(c),
                    orderBy: q => q.OrderBy(c => c.CategoryName),
                    include: i => i.Include(c => c.Subcategories)
                );

                return new MethodResult<IEnumerable<CategoryResponse>>.Success(categories);
            }
            catch (Exception ex)
            {
                return new MethodResult<IEnumerable<CategoryResponse>>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> CreateCategoryAsync(CategoryCreateRequest request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);
                await _uow.GetRepository<Category>().InsertAsync(category);
                await _uow.CommitAsync();
                return new MethodResult<string>.Success("Create category successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> UpdateCategoryAsync(int id, CategoryUpdateRequest request)
        {
            try
            {
                var category = await _uow.GetRepository<Category>().SingleOrDefaultAsync(
                    predicate: c => c.CategoryId == id,
                    include: i => i.Include(c => c.Subcategories)
                );

                if (category == null)
                {
                    return new MethodResult<string>.Failure("Category not found", StatusCodes.Status404NotFound);
                }

                var requestSubcategoryIds = request.Subcategories.Select(s => s.SubcategoryId).ToHashSet();
                var toRemove = category.Subcategories.Where(x => !requestSubcategoryIds.Contains(x.SubcategoryId)).ToList();
                foreach (var sc in toRemove)
                {
                    _uow.GetRepository<Subcategory>().DeleteAsync(sc);
                }

                _mapper.Map(request, category);                
                _uow.GetRepository<Category>().UpdateAsync(category);

                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Update category successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<MethodResult<string>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _uow.GetRepository<Category>().SingleOrDefaultAsync(
                    predicate: c => c.CategoryId == id,
                    include: i => i.Include(c => c.Subcategories).ThenInclude(sc => sc.Products)
                );

                if (category == null)
                {
                    return new MethodResult<string>.Failure("Category not found", StatusCodes.Status404NotFound);
                }

                // Check if category has packages
                if (category.Subcategories.Any(sc => sc.Products.Count() != 0))
                {
                    return new MethodResult<string>.Failure("Cannot delete category with associated products", StatusCodes.Status400BadRequest);
                }

                _uow.GetRepository<Subcategory>().DeleteRangeAsync(category.Subcategories);
                _uow.GetRepository<Category>().DeleteAsync(category);
                await _uow.CommitAsync();

                return new MethodResult<string>.Success("Delete category successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}