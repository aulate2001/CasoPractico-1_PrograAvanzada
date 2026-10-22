using CasoPractico1_PrograAvanzada.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasoPractico1_PrograAvanzada.Controllers;

public partial class ProductosController
{
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var producto = await _servicio.ObtenerAsync(id);

        if (producto == null)
        {
            TempData["Error"] = "No se encontró el producto.";
            return RedirectToAction(nameof(Index));
        }

        return View(producto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Producto producto)
    {
        if (id != producto.Id)
            return BadRequest();

        if (ModelState.IsValid &&
            await _servicio.ExisteCodigoAsync(producto.Codigo, producto.Id))
        {
            ModelState.AddModelError(nameof(producto.Codigo), "Ya existe un producto con ese código.");
        }

        if (!ModelState.IsValid)
            return View(producto);

        await _servicio.ActualizarAsync(producto);
        TempData["Exito"] = "El producto se actualizó correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
