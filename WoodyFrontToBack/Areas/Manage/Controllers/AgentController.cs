using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodyFrontToBack.DAL.Context;
using WoodyFrontToBack.DTOs.Agents;
using WoodyFrontToBack.Helpers;
using WoodyFrontToBack.Models;

namespace WoodyFrontToBack.Areas.Manage.Controllers;
[Area("Manage")]
public class AgentController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AgentController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<Agent> agents = await _context.Agents
            .Include(x => x.Position)
            .ToListAsync();
        return View(agents);

    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAgentDto agentDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (_context.Agents.Any(x => x.Name == agentDto.Name))
        {
            ModelState.AddModelError("Name", "This agent already exists");
            return View(agentDto);
        }

        if (agentDto.File == null || !agentDto.File.ContentType.Contains("image"))
        {
            ModelState.AddModelError("File", "Please upload a valid image file");
            return View();
        }

        string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "agent");
        agentDto.ImageUrl = await FileHelper.SaveFileAsync(uploadPath, agentDto.File);

        var agent = _mapper.Map<Agent>(agentDto);

        await _context.Agents.AddAsync(agent);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id == null) return NotFound();

        var agent = await _context.Agents.FirstOrDefaultAsync(x => x.Id == id);
        if (agent == null) return NotFound();

        return View(agent);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAgentDto newAgentDto)
    {
        if (!ModelState.IsValid)
        {
            return View(newAgentDto);
        }

        var oldAgent = await _context.Agents.FirstOrDefaultAsync(c => c.Id == newAgentDto.Id);

        if (oldAgent == null)
        {
            return NotFound();
        }

        if (_context.Agents.Any(x => x.Name == newAgentDto.Name && x.Id != newAgentDto.Id))
        {
            ModelState.AddModelError("Name", "This agent name is already taken");
            return View(newAgentDto);
        }

        if (newAgentDto.File != null)
        {
            if (!newAgentDto.File.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Please upload a valid image file");
                return View(newAgentDto);
            }

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", "agent");

            if (!string.IsNullOrEmpty(oldAgent.ImageUrl))
            {
                string existingFilePath = Path.Combine(uploadPath, Path.GetFileName(oldAgent.ImageUrl));
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            newAgentDto.ImageUrl = await FileHelper.SaveFileAsync(uploadPath, newAgentDto.File);
        }
        else
        {
            newAgentDto.ImageUrl = oldAgent.ImageUrl;
        }

        _mapper.Map(newAgentDto, oldAgent);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
