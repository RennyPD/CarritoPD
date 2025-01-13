using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {

        public List<Reporte> Ventas (string fechaInicio, string fechaFin, string idTransaccion)
        {

                List<Reporte> lista = new List<Reporte>();

                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                    {

                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechaInicio);
                    cmd.Parameters.AddWithValue("fechafin", fechaFin);
                    cmd.Parameters.AddWithValue("idtransaccion", idTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Reporte()
                            {
                                FechaVenta = dr["FechaVenta"].ToString(),
                                Cliente = dr["Cliente"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("en-US")),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                Total = Convert.ToDecimal(dr["Total"], new CultureInfo("en-US")),
                                IdTransaccion = dr["IdTransaccion"].ToString()
                            });
                        }

                        }
                    }
                }
                catch
                {

                    lista = new List<Reporte>();
                }

                return lista; 
        }


        public Dashboard VerDasboard()
        {

            Dashboard objeto = new Dashboard();

            try
            {
                using (SqlConnection oconnexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconnexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconnexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            objeto = new Dashboard()
                            {
                                TotalClientes = Convert.ToInt32(dr["totalClientes"]),
                                TotalVentas = Convert.ToInt32(dr["totalVentas"]),
                                TotalProductos = Convert.ToInt32(dr["totalProductos"])
                            };
                        }
                    }
                }
            }
            catch
            {
               objeto = new Dashboard();
            }

            return objeto;

        }
    }
}
