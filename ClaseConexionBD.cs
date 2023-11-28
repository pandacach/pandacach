using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.SqlServer.Management.Sdk.Sfc.OrderBy;

namespace FacturacionVentasWPF
{
    internal class ClaseConexionBD
    {
        readonly SqlConnectionStringBuilder cxnBDFacturacion = new()
        {
            DataSource = "DESKTOP-M89EO18\\SQLEXPRESSCLAUDI", //indico el nombre del servidor
            InitialCatalog = "BD_Facturacion", //indico el nombre de la BD
            IntegratedSecurity = true //indico para que me deje usar autenticacion de windows sin passwor
        }; //declaro el objeto string builder de conexion

        #region Metodos CRUD Clientes
        //metodo de insercion de datos en la BD
        public string InsertarResgistrosClientes(double idCli, double capacidad_creditoCli, string correo_electronicoCli, string direccionCli, double identificacionCli, string nombre_completoCli, double telefonoCli, string tipo_identificacionCli)
        {
            string resultadoOp;
            try
            {
                var strConexion = cxnBDFacturacion.ConnectionString; //guardo el string de conexion en una variable
                using SqlConnection cxnConBD = new(strConexion);
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                string queryInsert = " insert into clientes(id, capacidad_credito, correo_electronico, direccion, fecha_ingreso, identificacion, nombre_completo, telefono, tipo_identificacion) values (@id, @capacidad_credito, @correo_electronico, @direccion, @fecha_ingreso, @identificacion, @nombre_completo, @telefono, @tipo_identificacion)";
                SqlCommand comando = new(queryInsert, cxnConBD);
                cxnConBD.Open();
                comando.Parameters.AddWithValue("@id", idCli);
                comando.Parameters.AddWithValue("@capacidad_credito", capacidad_creditoCli);
                comando.Parameters.AddWithValue("@correo_electronico", correo_electronicoCli);
                comando.Parameters.AddWithValue("@direccion", direccionCli);
                comando.Parameters.AddWithValue("@fecha_ingreso", myDateTime);
                comando.Parameters.AddWithValue("@identificacion", identificacionCli);
                comando.Parameters.AddWithValue("@nombre_completo", nombre_completoCli);
                comando.Parameters.AddWithValue("@telefono", telefonoCli);
                comando.Parameters.AddWithValue("@tipo_identificacion", tipo_identificacionCli);
                comando.ExecuteNonQuery();
                cxnConBD.Close();

                //
                resultadoOp = "ingresados";
                return resultadoOp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();

            }
        }
        //metodo de busqueda por parametros 
        public static DataTable GetBusquedaCliente(int tipoId, double codId)
        {
            var dt = new System.Data.DataTable();



            SqlConnection conexion = new("server=DESKTOP-M89EO18\\SQLEXPRESSCLAUDI ; database=BD_Facturacion ; integrated security = true");
            conexion.Open();
            string busquedaPor = "";

            if (tipoId == 1)
            {
                busquedaPor = "SELECT * FROM clientes WHERE id=" + codId;
            }
            if (tipoId == 2)
            {
                busquedaPor = "SELECT * FROM clientes WHERE identificacion=" + codId;
            }
            if (tipoId == 3)
            {
                busquedaPor = "SELECT * FROM clientes";
            }

            SqlCommand comando = new(busquedaPor, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            dt.Load(registro);
            conexion.Close();
            return dt;
        }
        //metodo delete
        public bool BorrarRegistro(double identBorrar)
        {
            try
            {
                var strConexion = cxnBDFacturacion.ConnectionString; //guardo el string de conexion en una variable
                using SqlConnection cxnConBD = new(strConexion);
                string queryInsert = "DELETE FROM clientes WHERE identificacion=" + identBorrar;
                SqlCommand comando = new(queryInsert, cxnConBD);
                cxnConBD.Open();
                comando.ExecuteNonQuery();
                cxnConBD.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion

        #region Categorias de Productos
        //metodo de insercion de datos en la BD
        public string InsertarResgistroCategorias(double idCat, string descripcion, string ubicacion)
        {
            string resultadoOp;
            try
            {
                var strConexion = cxnBDFacturacion.ConnectionString; //guardo el string de conexion en una variable
                //bloque para buscar la cantidad de registros 

                using SqlConnection cxnConBD = new(strConexion);
                string queryInsert = " insert into categorias(id, descripcion, ubicacion) values (@idCat, @descripcion, @ubicacion)";
                string queryCountId = "SELECT count(id) FROM categorias";
                SqlCommand cmdContId = new(queryCountId, cxnConBD);

                SqlCommand comando = new(queryInsert, cxnConBD);
                cxnConBD.Open();
                comando.Parameters.AddWithValue("@idCat", idCat);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@ubicacion", ubicacion);
                comando.ExecuteNonQuery();
                int numReg = Convert.ToInt32(cmdContId.ExecuteScalar());
                cxnConBD.Close();
                //
                resultadoOp = "ingresados" + numReg.ToString();
                return resultadoOp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static DataTable GetBusquedaCategorias()
        {
            var dt = new System.Data.DataTable();
            SqlConnection conexion = new("server=DESKTOP-M89EO18\\SQLEXPRESSCLAUDI ; database=BD_Facturacion ; integrated security = true");
            conexion.Open();
            string busquedaPor = "SELECT * FROM categorias";
            SqlCommand comando = new(busquedaPor, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            dt.Load(registro);
            conexion.Close();
            return dt;
        }

        public static DataTable GetBusqIdCatProd()
        {
            var dt = new System.Data.DataTable();
            SqlConnection conexion = new("server=DESKTOP-M89EO18\\SQLEXPRESSCLAUDI; database=BD_Facturacion ; integrated security = true");
            conexion.Open();
            string busquedaPor = "SELECT * FROM categorias";
            SqlCommand comando = new(busquedaPor, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            dt.Load(registro);
            conexion.Close();
            return dt;
        }

        #endregion

        #region Productos

        public string RegistrarNuevosProductos(double idProd, string descripProd, int existProd, decimal precProduct, double idCatProd)
        {
            try
            {
                var strConexion = cxnBDFacturacion.ConnectionString; //guardo el string de conexion en una variable
                using SqlConnection cxnConBD = new(strConexion);
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                string queryInsert = " insert into productos(id, descripcion, disponible, existencia, ultimo_ingreso, precio, categoria_id) values (@id, @descripcion, @disponible, @existencia, @ultimo_ingreso, @precio, @categoria_id)";
                SqlCommand comando = new(queryInsert, cxnConBD);
                cxnConBD.Open();
                comando.Parameters.AddWithValue("@id", idProd);
                comando.Parameters.AddWithValue("@descripcion", descripProd);
                comando.Parameters.AddWithValue("@disponible", Convert.ToByte(1));
                comando.Parameters.AddWithValue("@existencia", existProd);
                comando.Parameters.AddWithValue("@ultimo_ingreso", myDateTime);
                comando.Parameters.AddWithValue("@precio", precProduct);
                comando.Parameters.AddWithValue("@categoria_id", idCatProd+1);

                comando.ExecuteNonQuery();
                cxnConBD.Close();

                //
                string resultadoOp = "Producto Ingresado";
                return resultadoOp;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();

            }
        }
        //codigo para validar id productos existentes
        public static bool ValidarIdProductoIngresar(double claveVerif)
        {
            bool resVerif;
            SqlConnection conexion = new("server=DESKTOP-M89EO18\\SQLEXPRESSCLAUDI ; database=BD_Facturacion ; integrated security = true");
            conexion.Open();
            // string busquedaPor = "";
            string queryCountId = "SELECT * FROM productos WHERE id=" + claveVerif;
            SqlCommand comando = new(queryCountId, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
                resVerif = true;
            else
                resVerif = false;

            conexion.Close();


            return resVerif;
        }
        #endregion



    }
}
