using CoffeeBeansApi.Interfaces;
using CoffeeBeansApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBeansApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeansController : ControllerBase
{
    private readonly IAllBeansRepository _allBeansRepository;

    public BeansController(IAllBeansRepository allBeansRepository)
    {
        _allBeansRepository = allBeansRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Beans>>> GetBeans() => Ok(await _allBeansRepository.GetAllAsync());

    [HttpGet("bean-of-the-day")]
    public async Task<ActionResult<Beans>> GetBeanOfTheDay()
    {
        var bean = await _allBeansRepository.GetBeanOfTheDayAsync();
        return bean is not null ? Ok(bean) : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Beans>> GetBean(int id)
    {
        var bean = await _allBeansRepository.GetByIdAsync(id);
        return bean is not null ? Ok(bean) : NotFound();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Beans>>> SearchBeans([FromQuery] string query)
    {
        var beans = await _allBeansRepository.SearchAsync(query);
        return beans.Any() ? Ok(beans) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Beans>> CreateBean(Beans bean)
    {
        await _allBeansRepository.AddAsync(bean);
        return CreatedAtAction(nameof(GetBean), new { id = bean.Id }, bean);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBean(int id, Beans updatedBean)
    {
        var existingBean = await _allBeansRepository.GetByIdAsync(id);
        if (existingBean == null) return NotFound();

        existingBean.Name = updatedBean.Name;
        existingBean.Origin = updatedBean.Origin;
        existingBean.RoastLevel = updatedBean.RoastLevel;
        existingBean.Cost = updatedBean.Cost;
        existingBean.Image = updatedBean.Image;
        existingBean.Colour = updatedBean.Colour;
        existingBean.Description = updatedBean.Description;
        existingBean.Country = updatedBean.Country;
        existingBean.IsBOTD = updatedBean.IsBOTD;

        await _allBeansRepository.UpdateAsync(existingBean);
        return Ok(existingBean);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBean(int id)
    {
        await _allBeansRepository.DeleteAsync(id);
        return NoContent();
    }
}
