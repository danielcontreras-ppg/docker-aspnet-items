using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using ItemApi.Models;
using Microsoft.AspNetCore.Cors;
using System.Data;
namespace ItemApi.Controllers;

[ApiController]
[Route("api/[controller]/{action}")]

//[DisableCors]
public class ItemsController : ControllerBase
{
    private readonly ItemApiContext _ctx;
    public ItemsController(ItemApiContext ctx)
    {
        _ctx = ctx;
    }

    [EnableCors("TestPolicy")]

    [HttpGet]
    public IQueryable<Item> GetItems()
    {
        var items = _ctx.Set<Item>();
        return items;
    }

    [EnableCors("TestPolicy")]
    [HttpPut]
    public IActionResult PutItems([FromBody] Item i)
    {
        var item = _ctx.Items.Where(itm => itm.itemcode == i.itemcode).FirstOrDefault();
        if(item == null)
        {
            return NotFound();
        }
        item.technicalgroup = i.technicalgroup;

        _ctx.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        bool a =_ctx.ChangeTracker.HasChanges(); 
        _ctx.SaveChanges();

        return new NoContentResult();
    }
}