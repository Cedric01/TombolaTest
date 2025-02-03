using CoffeeBeansApi.Helpers;
using CoffeeBeansApi.Interfaces;
using CoffeeBeansApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CoffeeBeansApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeansController : ControllerBase
{
    private readonly IAllBeansRepository _allBeansRepository;
    private readonly IDistributedCache _cache;

    public BeansController(IAllBeansRepository allBeansRepository, IDistributedCache cache)
    {
        _allBeansRepository = allBeansRepository;
        _cache = cache;
    }

    [HttpGet]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Beans>>> GetBeans()
    {
        var cacheKey = "all_beans";
        var cachedBeans = await _cache.GetStringAsync(cacheKey);
        if (cachedBeans != null)
        {
            return Ok(JsonSerializer.Deserialize<IEnumerable<Beans>>(cachedBeans));
        }

        var beans = await _allBeansRepository.GetAllAsync();
        if (beans.Any())
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(beans), CacheHelper.GetCacheOptions());
        }

        return Ok(beans);
    }

    [HttpGet("bean-of-the-day")]
    public async Task<ActionResult<Beans>> GetBeanOfTheDay()
    {
        var cacheKey = "bean_of_the_day";
        var cachedBean = await _cache.GetStringAsync(cacheKey);
        if (cachedBean != null)
        {
            return Ok(JsonSerializer.Deserialize<Beans>(cachedBean));
        }

        var bean = await _allBeansRepository.GetBeanOfTheDayAsync();
        if (bean is not null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(bean), CacheHelper.GetCacheOptions());
            return Ok(bean);
        }

        return NotFound();
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
