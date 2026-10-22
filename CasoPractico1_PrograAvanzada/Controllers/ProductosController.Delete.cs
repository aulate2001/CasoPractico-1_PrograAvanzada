using Microsoft.AspNetCore.Mvc;

namespace CasoPractico1_PrograAvanzada.Controllers;

public partial class ProductosController
{
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
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
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var eliminado = await _servicio.EliminarAsync(id);

        if (!eliminado)
        {
            TempData["Error"] = "No se encontró el producto para eliminar.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Exito"] = "El producto se eliminó correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
