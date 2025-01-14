using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodyFrontToBack.DAL.Context;
using WoodyFrontToBack.DTOs.Positions;
using WoodyFrontToBack.Models;

namespace WoodyFrontToBack.Areas.Manage.Controllers;
[Area("Manage")]
public class PositionController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PositionController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Position> positions = await _context.Positions
            .Include(x => x.Agents)
            .ToListAsync();
        return View(positions);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePositionDto position)
    {
        if (_context.Positions.Any(x => x.Name == position.Name))
        {
            ModelState.AddModelError("Name", "This position already exists");
            return View(position);
        }

        var mapper = _mapper.Map<Position>(position);

        await _context.Positions.AddAsync(mapper);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id == null) return NotFound();

        var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        if (position == null) return NotFound();

        return View(position);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdatePositionDto newPosition)
    {
        if (!ModelState.IsValid) return View(newPosition);

        var oldPosition = await _context.Positions.FirstOrDefaultAsync(x => x.Id == newPosition.Id);
        if (oldPosition == null) return NotFound();

        _mapper.Map(newPosition, oldPosition);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();  

        var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        if (position == null) return NotFound();

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
