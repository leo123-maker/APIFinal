using APIFinal.Models.Dtos;
using APIFinal.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(_mapper.Map<CategoryDto>(category));
            }
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategories(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if(category == null)
            {
                return NotFound($"La categoria con el id {id} no existe");
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            
            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateCategory([FromBody] CreatecategoruSto createcategoruSto)
        {
            if(createcategoruSto == null)
            {
                return BadRequest(ModelState);
            }
            if (_categoryRepository.CategoryExists(createcategoruSto.Name))
            {
                ModelState.AddModelError("CustomError","La categoria ya existe");
                return BadRequest(ModelState);
            }
            var category = _mapper.Map<Category>(createcategoruSto);
            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("CustomError",$"Algo salio mal al guardar el registro {category.Name}");
                return StatusCode(500,ModelState);
            }
            return CreatedAtRoute("GetCategory",new {id = category.Id}, category);
        }

        [HttpPatch("{id:int}", Name ="UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateCategory(int id,[FromBody] CreatecategoruSto UpdatecategoruSto)
        {

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoria con el id {id} no existe");
            }
            if(UpdatecategoruSto == null)
            {
                return BadRequest(ModelState);
            }
            if (_categoryRepository.CategoryExists(UpdatecategoruSto.Name))
            {
                ModelState.AddModelError("CustomError","La categoria ya existe");
                return BadRequest(ModelState);
            }
            var category = _mapper.Map<Category>(UpdatecategoruSto);
            category.Id = id;

            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("CustomError",$"Algo salio mal al actualizar el registro {category.Name}");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{id:int}", Name ="DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteCategory(int id)
        {

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoria con el id {id} no existe");
            }
            var category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound($"La categoria con el id {id} no existe");
            }

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("CustomError",$"Algo salio mal al eliminar el registro {category.Name}");
                return StatusCode(500,ModelState);
            }
            return NoContent();
        }


    }

}
