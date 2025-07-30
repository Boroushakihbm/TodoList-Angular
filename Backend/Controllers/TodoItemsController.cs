using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TodoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemReadDto>>> GetAll()
        {
            var items = await _context.TodoItems.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TodoItemReadDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemReadDto>> GetById(Guid id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();

            return Ok(_mapper.Map<TodoItemReadDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemReadDto>> Create(TodoItemCreateDto dto)
        {
            var todoItem = _mapper.Map<TodoItem>(dto);
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            var readDto = _mapper.Map<TodoItemReadDto>(todoItem);
            return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TodoItemUpdateDto dto)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            _mapper.Map(dto, todoItem);

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
