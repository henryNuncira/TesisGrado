using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCCCactualizado.Dto;
using WebApiCCCactualizado.Helper;
using WebApiCCCactualizado.Models;

namespace WebApiCCCactualizado.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        CCCVentasContext context = new CCCVentasContext();

        //----------------************* login *******------------------
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {

            return context.Usuarios.ToList(); ;

        }

        [HttpPost]
        public Response PostNuevoUsuario(BodyUsuario usuario)
        {
            HashedPassword Password = HashHelper.Hash(usuario.Password);
            try
            {
                // CCCContext context = new CCCContext();
                Usuario usua = new Usuario
                {


                    NombreUsuario = usuario.NombreUsuario,
                    Password = Password.Password,
                    Sal = Password.Salt,
                    Activo = usuario.Activo,
                    Rol = usuario.Rol,
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),

                };
                context.Usuarios.Add(usua);
                context.SaveChanges();
                return new Response { state = 200, message = "Se creo un nuevo usuario correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente

        [HttpPut("{idUsuario}")]
        public ActionResult PutUsuario(int idUsuario, Usuario usuario)
        {


            if (usuario.IdUsuario == idUsuario)
            {
                context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        //  eliminar
        [HttpDelete("{idUsuario}")]
        public ActionResult DeleteUsuario(int idUsuario)
        {
            // var cliente = context.Clientes.Find(idCliente);
            // var usuario = context.Usuario.Find(idUsuario);
            var usuario = context.Usuarios.FirstOrDefault(x => x.IdUsuario == idUsuario);

            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        public string CheckHash(string attemptedPassword, string hash, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: attemptedPassword,
                 salt: Convert.FromBase64String(salt),
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 10000,
                 numBytesRequested: 256 / 8));
            return hashed;
        }

        [HttpPost]
        public Response PostLogin(BodyLogin login)
        {
            var result = context.Usuarios.Where(x => x.NombreUsuario == login.NombreUsuario).ToList();

            if (result.Count != 0)
            {
                //traigo la salt y el password de la base de datos segun el usuario que ingresa el login
                var usuario = context.Usuarios.Where(x => x.NombreUsuario == login.NombreUsuario).ToList();
                var salt = usuario.Select(x => x.Sal).ToList()[0];
                var claveBD = usuario.Select(x => x.Password).ToList()[0];
                // hasheo nuevamente la clave y comparo con la base de datos que sea el mismo y retorna  el hash
                var claveVerificada = CheckHash(login.Password, claveBD, salt).ToString();

                //comparamos que los datos recibidos cumplan el login
                // var usuario = context.Usuarios.Where(x => (x.NombreUsuario == login.NombreUsuario)&&(x.Password == claveVerificada)).ToList();

                if (claveVerificada == claveBD)
                {
                    var acti = usuario.Select(x => x.Activo).ToList()[0];
                    var rolactivo = usuario.Select(x => x.Rol).ToList()[0];
                    var token = usuario.Select(x => x.Token).ToList()[0];
                    if (acti == 1)
                    {

                        return new Response { state = 200, message = "Usuario Activo", rol = (int)rolactivo, token = (string)token };
                    }
                    else
                    {
                        return new Response { state = 400, message = "Existe usuario pero se encuentra inactivo", rol = 0 };
                    }
                }
                else
                {
                    return new Response { state = 400, message = "Clave incorrecta No existe revise sus credenciales", rol = 0 };
                }

            }
            else
            {
                return new Response { state = 400, message = "Usuario No existe revise sus credenciales" };
            }
        }


        //----------------************* CRUD de Clientes *******------------------

        //listar Todos los campos del cliente que existen
        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {

            return context.Clientes.ToList();

        }

        //   listar un cliente a travez de un id
        [HttpGet("{idCliente}")]
        public Cliente GetListarclienteId(int idCliente)
        {
            var rescliente = context.Clientes.FirstOrDefault(x => x.IdCliente == idCliente);
            return rescliente;

        }

        // agregar un nuevo cliente
        [HttpPost]
        public Response PostNuevoBodyCliente(BodyCliente bodycliente)
        {
            try
            {
                // CCCContext context = new CCCContext();
                Cliente clien = new Cliente
                {
                    Nit = bodycliente.Nit,
                    NombreCompleto = bodycliente.NombreCompleto,
                    Telefono = bodycliente.Telefono,
                    Direccion = bodycliente.Direccion,
                    CorreoElectronico = bodycliente.CorreoElectronico
                };
                context.Clientes.Add(clien);
                context.SaveChanges();
                return new Response { state = 200, message = "Se creo un nuevo cliente correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente
        [HttpPut("{idCliente}")]
        public ActionResult PutCliente(int idCliente, Cliente cliente)
        {


            if (cliente.IdCliente == idCliente)
            {
                context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        //  eliminar
        [HttpDelete("{idCliente}")]
        public ActionResult DeleteCliente(int idCliente)
        {
            // var cliente = context.Clientes.Find(idCliente);
            var cliente = context.Clientes.FirstOrDefault(x => x.IdCliente == idCliente);

            if (cliente != null)
            {
                context.Clientes.Remove(cliente);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }



        // //----------------************* CRUD de Vendedores *******------------------

        //listar Todos los campos de todos los vendedores que existen
        [HttpGet]
        public IEnumerable<Vendedor> GetVendedores()
        {
            return context.Vendedors.ToList();
        }

        // agregar un nuevo cliente
        [HttpPost]
        public Response PostNuevoVendedor(BodyVendedore bodyVendedore)
        {
            try
            {
                Vendedor vendedor = new Vendedor
                {
                    Nit = bodyVendedore.NitVendedor,
                    NombreCompleto = bodyVendedore.NombreCompleto,
                    Telefono = bodyVendedore.Telefono,
                    Direccion = bodyVendedore.Direccion,
                    CorreoElectronico = bodyVendedore.CorreoElectronico
                };

                context.Vendedors.Add(vendedor);
                context.SaveChanges();
                return new Response { state = 200, message = "Se creo un nuevo vendedor correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente
        [HttpPut("{idVendedor}")]
        public ActionResult PutVendedor(int idVendedor, Vendedor vendedore)
        {

            if (vendedore.IdVendedor == idVendedor)
            {
                context.Entry(vendedore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // eliminar
        [HttpDelete("{idVendedor}")]
        public ActionResult DeleteVendedor(int idVendedor)
        {
            var vendedor = context.Vendedors.FirstOrDefault(x => x.IdVendedor == idVendedor);

            if (vendedor != null)
            {
                context.Vendedors.Remove(vendedor);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        // //----------------************* CRUD de Proveedores *******------------------

        //listar Todos los campos de todos los Proveedores que existen
        [HttpGet]
        public IEnumerable<Proveedor> GetProveedores()
        {
            return context.Proveedors.ToList();
        }

        // listar un Proveedor a travez de un id
        [HttpGet("{idProveedor}")]
        public Proveedor GetListarProveedorId(int idProveedor)
        {

            var resProveedor = context.Proveedors.FirstOrDefault(x => x.IdProveedor == idProveedor);
            return resProveedor;

        }

        // agregar un nuevo Proveedores
        [HttpPost]
        public Response PostNuevoProveedor(BodyProveedore bodyProveedor)
        {
            try
            {
                Proveedor proveedor = new Proveedor
                {
                    NitProveedor = bodyProveedor.NitProveedor,
                    NombreCompleto = bodyProveedor.NombreCompleto,
                    Telefono = bodyProveedor.Telefono,
                    Direccion = bodyProveedor.Direccion,
                    CorreoElectronico = bodyProveedor.CorreoElectronico
                };
                context.Proveedors.Add(proveedor);
                context.SaveChanges();
                return new Response { state = 200, message = "Se creo un nuevo proveedor correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente
        [HttpPut("{idProveedor}")]
        public ActionResult PutProveedor(int idProveedor, Proveedor proveedore)
        {

            if (proveedore.IdProveedor == idProveedor)
            {
                context.Entry(proveedore).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // eliminar
        [HttpDelete("{idProveedor}")]
        public ActionResult DeleteProveedor(int idProveedor)
        {
            var proveedor = context.Proveedors.FirstOrDefault(x => x.IdProveedor == idProveedor);

            if (proveedor != null)
            {
                context.Proveedors.Remove(proveedor);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        //----------------************* CRUD de Categorias *******------------------

        // listar Todos los campos de las catagorias que existen
        [HttpGet]
        public IEnumerable<Categorium> GetCategorias()
        {
            return context.Categoria.ToList();
        }

        // listar una categoria a travez de un id
        [HttpGet("{idCategoria}")]
        public Categorium GetListarcategoriaId(int idCategoria)
        {

            var rescategoria = context.Categoria.FirstOrDefault(x => x.IdCategoria == idCategoria);
            return rescategoria;

        }

        // agregar una nueva categoria
        [HttpPost]
        public Response PostNuevoBodyCategoria(BodyCategoria bodycategoria)
        {
            try
            {
                CCCVentasContext context = new CCCVentasContext();
                Categorium cate = new Categorium
                {
                    Nombre = bodycategoria.NombreBProducto,
                    Descripcion = bodycategoria.DesBProducto,
                   
                };
                context.Categoria.Add(cate);

                context.SaveChanges();


                return new Response { state = 200, message = "Se creo una nueva categoria correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente
        [HttpPut("{idCategoria}")]
        public ActionResult PutCategoria(int idCategoria, Categorium categoria)
        {

            if (categoria.IdCategoria == idCategoria)
            {
                context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // eliminar
        [HttpDelete("{idCategoria}")]
        public ActionResult DeleteCategoria(int idCategoria)
        {
            var categoria = context.Categoria.FirstOrDefault(x => x.IdCategoria == idCategoria);

            if (categoria != null)
            {
                context.Categoria.Remove(categoria);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        ////----------------************* CRUD de Productos *******------------------

        //listar Todos los campos de las productos que existen
        [HttpGet]
        public IEnumerable<Producto> GetProductos()
        {
            return context.Productos.ToList();
        }

        // listar una categoria a travez de un id
        [HttpGet("{idProducto}")]
        public Producto GetListarProductoId(int idProducto)
        {

            var resproducto = context.Productos.FirstOrDefault(x => x.IdProducto == idProducto);
            return resproducto;

        }

        // listar los productos con stock menor a 10 unidades
        [HttpGet]
        public IEnumerable<Producto> Getstock()
        {

            var produc = context.Productos.Where(x => x.Stock <= 10).ToList();
            return produc;

        }


        // agregar una nuevo producto
        [HttpPost]
        public Response PostNuevoProducto(BodyProducto bodyProducto)
        {
            try
            {
                Producto produc = new Producto
                {
                    NitCodigo = bodyProducto.NitCodigo,
                    Nombre = bodyProducto.NombreCompleto,
                    PrecioVenta = bodyProducto.Precio,
                    Stock = bodyProducto.Stock,
                    Descricion = bodyProducto.Descripcion

                };
                context.Productos.Add(produc);
                context.SaveChanges();
                return new Response { state = 200, message = "Se creo un nuevo producto correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente
        [HttpPut("{idProducto}")]
        public ActionResult PutProducto(int idProducto, Producto producto)
        {

            if (producto.IdProducto == idProducto)
            {
                context.Entry(producto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // eliminar
        [HttpDelete("{idProducto}")]
        public ActionResult DeleteProducto(int idProducto)
        {
            var Producto = context.Productos.FirstOrDefault(x => x.IdProducto == idProducto);

            if (Producto != null)
            {
                context.Productos.Remove(Producto);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        ////----------------************* CRUD de Venta de pedidos *******------------------

        //listar Todos los campos de venta que existen
        [HttpGet]
        public IEnumerable<Ventum> GetVentas()
        {
            // aca se puede listar todas las ventas ordenadas por fecha         

            return context.Venta.ToList();

        }

        //listar 
        [HttpGet]
        public List<Ventum> Getventasforanea()
        {
            var venta = context.Venta.ToList();
            var vendedor = context.Vendedors.ToList();

            return venta;


        }

        // listar una venta a travez de un id
        [HttpGet("{idVenta}")]
        public Ventum GetListarVentaId(int idVenta)
        {

            var resVenta = context.Venta.FirstOrDefault(x => x.IdVenta == idVenta);
            return resVenta;

        }

        // agregar un nueeva venta
        [HttpPost]
        public Response PostNuevoVenta(BodyVenta bodyVenta)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Ventum venta = new Ventum();
                        venta.Fecha = DateTime.Now;
                        venta.TipoComprobante = bodyVenta.TipoComprobante;
                        venta.NumeroComprobante = bodyVenta.NumeroComprobante;
                        venta.Impuesto = bodyVenta.Impuesto;
                        venta.Total = bodyVenta.Detalles.Sum(d => (d.Cantidad * d.Precio) - d.Descuento);
                        venta.IdCliente = bodyVenta.IdCliente;
                        venta.IdVendedor = bodyVenta.IdVendedor;
                        context.Venta.Add(venta);
                        context.SaveChanges();

                        foreach (var bodydetalle in bodyVenta.Detalles)
                        {
                            DetalleVentum detalle = new Models.DetalleVentum();
                            detalle.IdProducto = bodydetalle.idProducto;
                            var producto = GetListarProductoId(Convert.ToInt32(bodydetalle.idProducto));
                            detalle.Cantidad = bodydetalle.Cantidad;
                            detalle.Precio = producto.PrecioVenta;
                            detalle.Descuento = bodydetalle.Descuento;
                            detalle.IdVenta = venta.IdVenta;
                            
                            context.DetalleVenta.Add(detalle);
                            context.SaveChanges();
                        }
                        
                        transaction.Commit();
                        return new Response { state = 200, message = "Se creo un nueva venta correctamente" };
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    return new Response { state = 200, message = "Se creo un nueva venta correctamente" };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente
        [HttpPut("{idVenta}")]
        public ActionResult PutVenta(int idVenta, Ventum venta)
        {

            if (venta.IdVenta == idVenta)
            {
                context.Entry(venta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // eliminar
        [HttpDelete("{idVenta}")]
        public ActionResult DeleteVenta(int idVenta)
        {
            var venta = context.Venta.FirstOrDefault(x => x.IdVenta == idVenta);

            if (venta != null)
            {
                context.Venta.Remove(venta);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        ////----------------************* CRUD de Venta de detalle (concept) *******------------------

        //listar Todos los campos de venta que existen
        [HttpGet]
        public IEnumerable<DetalleVentum> GetDetallesVentas()
        {
            // aca se puede listar todas las ventas ordenadas por fecha         

            return context.DetalleVenta.ToList();

        }

        ////listar 
        //[HttpGet]
        //public List<Ventum> Getventasforanea()
        //{
        //    var venta = context.Venta.ToList();
        //    var product = context.Productos.ToList();

        //    return venta;


        //}

        // listar una venta a travez de un id
        [HttpGet("{idDetalleVenta}")]
        public DetalleVentum GetListarDetalleVentaId(int idDetalleVenta)
        {

            var resVenta = context.DetalleVenta.FirstOrDefault(x => x.IdDetalleVenta == idDetalleVenta);
            return resVenta;

        }

        // agregar un nueeva venta
        [HttpPost]
        public Response PostNuevoDetalleVenta(BodyDetalleVenta bodydetVenta)
        {
            try
            {
                DetalleVentum detaVenta = new DetalleVentum
                {
                    Cantidad = bodydetVenta.Cantidad,
                    Precio = bodydetVenta.Precio,
                    Descuento = bodydetVenta.Descuento
                    
                };
                context.DetalleVenta.Add(detaVenta);
                context.SaveChanges();
                return new Response { state = 200, message = "Se creo un nueva Detalle venta correctamente" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(Convert.ToString(ex));
            }

        }


        // actualizar o modificar uno existente


        // eliminar
       
    }
}
