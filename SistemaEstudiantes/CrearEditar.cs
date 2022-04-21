using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace SistemaEstudiantes
{
    public partial class CrearEditar : Form
    {
        public DataSet miDataSet = new DataSet();        
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        OleDbCommand sqlComando;

        int row, column;
        public string idSelecionada;

        public bool agregando = false;
        public bool reiniciando = false;
        public bool eliminando = false;

        string nombreUsuario;
        string tipoUsuario;

        public CrearEditar(string usuario, string permisos, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;

            DeshabilitarLimpiar();
            CargarDataGVBaseDatos();
        }

        private void DeshabilitarLimpiar()
        {
            tbxUsuario.Enabled = false;
            tbxContraseña.Enabled = false;
            cbxPermisos.Enabled = false;

            tbxUsuario.Clear();
            tbxContraseña.Clear();
            cbxPermisos.SelectedItem = null;

            btnNuevo.Visible = true;
            btnAgregar.Visible = false;
            btnModificar.Visible = true;
            btnModificarA.Visible = false;
            btnCancelar.Visible = false;
            btnEliminar.Visible = true;

            btnModificar.Enabled = false;
            btnModificar.BackColor = Color.Silver;
            btnEliminar.Enabled = false;
            btnEliminar.BackColor = Color.Silver;            

            agregando = false;
            reiniciando = false;
            eliminando = false;

            dataGridViewUsuarios.Enabled = true;
        }
        private void CargarDataGVBaseDatos() //Metodo para cargar/actualizar dataGVusuarios
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT Usuario, Contraseña, Permisos FROM Usuarios";            

            sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

            miDataAdapter.Fill(miDataTable);

            dataGridViewUsuarios.DataSource = miDataTable;

            lblCantidadUsuarios.Text = Convert.ToString(dataGridViewUsuarios.Rows.Count - 1);

            dataGridViewUsuarios.Columns[1].DefaultCellStyle.Font = new Font("Wingdings", 6);
        }
        private void dataGridViewUsuarios_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //devuelve el valor al que hacemos click en el dataGV

            column = e.ColumnIndex; //columna            
            row = e.RowIndex; //fila

            if ((row != -1) && (e.ColumnIndex != -1) && (reiniciando == false) && (eliminando == false) && (agregando == false)) //ver que pasa con el index
            {
                //Rellena los tbx 

                tbxUsuario.Text = Convert.ToString(dataGridViewUsuarios.Rows[row].Cells[0].Value);
                tbxContraseña.Text = Convert.ToString(dataGridViewUsuarios.Rows[row].Cells[1].Value);
                cbxPermisos.Text = Convert.ToString(dataGridViewUsuarios.Rows[row].Cells[2].Value);            

                btnModificar.Enabled = true;
                btnModificar.BackColor = Color.DodgerBlue;
                btnEliminar.Enabled = true;
                btnEliminar.BackColor = Color.DodgerBlue;

                idSelecionada = Convert.ToString(dataGridViewUsuarios.Rows[row].Cells[0].Value); // Devuelve el usuario seleccionado en el dgView que esta en la posicion 0               
                
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Opciones miOpciones = new Opciones(nombreUsuario, tipoUsuario,true,conexionBaseDatos);
            miOpciones.Visible = true;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            agregando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta agregando

            tbxUsuario.Clear();
            tbxContraseña.Clear();
            cbxPermisos.SelectedItem = 0;

            tbxUsuario.Enabled = true;
            tbxContraseña.Enabled = true;
            cbxPermisos.Enabled = true;

            dataGridViewUsuarios.Enabled = false;

            btnNuevo.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            
            btnAgregar.Visible = true;            
            btnCancelar.Visible = true;
        }
        private void btnReiniciar_Click(object sender, EventArgs e)
        {            
            reiniciando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta modificanco
            

            if (idSelecionada != "Admin")
            {
                tbxContraseña.Enabled = true;
                cbxPermisos.Enabled = true;

                dataGridViewUsuarios.Enabled = false;

                btnNuevo.Visible = false;
                btnModificar.Visible = false;
                btnEliminar.Visible = false;
                
                btnModificarA.Visible = true;                
                btnCancelar.Visible = true;
            }
            else
            {
                MessageBox.Show("El usuario Admin no puede ser modificado.", "Registro Informa");
                CargarDataGVBaseDatos();
                DeshabilitarLimpiar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminando = true;
            dataGridViewUsuarios.Enabled = false;

            if (idSelecionada != "Admin")
            {

                DialogResult preguntaEliminar = MessageBox.Show("Desea eliminar al usuario seleccionado?", "Registro Informa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (preguntaEliminar == DialogResult.OK)
                {
                    string queryEliminar = "DELETE *FROM Usuarios WHERE Usuario = @idEliminar";

                    sqlComando = new OleDbCommand(queryEliminar, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idEliminar", idSelecionada);

                    try
                    {
                        conexionBaseDatos.Open();
                        if (sqlComando.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Se elimino el usuario correctamente.", "Registro Informa");
                        }
                        else
                        {
                            MessageBox.Show("Usuario no eliminado. Se produzco un error.", "Registro Informa");
                        }
                        conexionBaseDatos.Close();
                        CargarDataGVBaseDatos();
                        DeshabilitarLimpiar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex.HResult));
                        conexionBaseDatos.Close();
                    }
                }
                else
                {
                    DeshabilitarLimpiar();
                }
            }
            else
            {
                MessageBox.Show("El usuario Admin no puede ser eliminado.", "Registro Informa");
                DeshabilitarLimpiar();
                CargarDataGVBaseDatos();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarLimpiar();
            CargarDataGVBaseDatos();
        }     
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if ((tbxUsuario.Text == "" || tbxUsuario.Text == "0") || (tbxContraseña.Text == "" || tbxContraseña.Text == "0") || cbxPermisos.Text == ""  || (tbxUsuario.TextLength >= 11))
            {
                MessageBox.Show("Hay campos sin completar, con valor cero y/o el usuario tiene mas de 10 caracteres.");
            }
            else
            {
                string queryAgregar = "INSERT INTO Usuarios VALUES (@usuario, @contraseña, @permisos) ";
                sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);

                sqlComando.Parameters.AddWithValue("@usuario", tbxUsuario.Text);
                sqlComando.Parameters.AddWithValue("@contraseña", tbxContraseña.Text);
                sqlComando.Parameters.AddWithValue("@permisos", cbxPermisos.Text);               

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Se agrego el usuario correctamente", "Registro Informa");
                    }
                    else
                    {
                        MessageBox.Show("Usuario no agregado. Se produzco un error.", "Registro Informa");
                    }
                    conexionBaseDatos.Close();
                    CargarDataGVBaseDatos();
                    DeshabilitarLimpiar();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                    {
                        conexionBaseDatos.Close();

                        DataTable miDataTable = new DataTable();
                        string queryCargarBD = "SELECT Usuario, Contraseña, Permisos FROM Usuarios WHERE Usuario = @idBuscar";
                        sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", tbxUsuario.Text);
                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        dataGridViewUsuarios.DataSource = miDataTable;                       

                        DialogResult accionRealizar = MessageBox.Show("Este Usuario ya existe en la base de datos.\n\nSi - Para realizar un cambio en el usuario que se está ingresando.\n"
                            +"\nNo - Para salir y no realizar ninguna acción.", "Registro Informa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                        if (accionRealizar == DialogResult.Yes)
                        {
                            //no hay nada que hacer. solo dejar seguir el codigo
                        }                        
                        else if (accionRealizar == DialogResult.No)
                        {
                            DeshabilitarLimpiar();
                            CargarDataGVBaseDatos();
                        }
                    }
                }
            }
        }
        private void btnVolver_MouseMove(object sender, MouseEventArgs e)
        {
            btnVolver.BackColor = Color.DimGray;
        }
       
        private void btnVolver_MouseLeave(object sender, EventArgs e)
        {
            btnVolver.BackColor = Color.DodgerBlue;
        }

        private void btnSalir_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalir.BackColor = Color.DimGray;
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = Color.DodgerBlue;
        }

        private void btnNuevo_MouseMove(object sender, MouseEventArgs e)
        {            
            btnNuevo.BackColor = Color.DimGray;
        }

        private void btnNuevo_MouseLeave(object sender, EventArgs e)
        {
            btnNuevo.BackColor = Color.DodgerBlue;
        }

        private void btnModificar_MouseMove(object sender, MouseEventArgs e)
        {
            btnModificar.BackColor = Color.DimGray;
        }

        private void btnModificar_MouseLeave(object sender, EventArgs e)
        {
            btnModificar.BackColor = Color.DodgerBlue;
        }

        private void btnEliminar_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminar.BackColor = Color.DimGray;
        }

        private void btnEliminar_MouseLeave(object sender, EventArgs e)
        {
            btnEliminar.BackColor = Color.DodgerBlue;
        }

        private void btnAgregar_MouseMove(object sender, MouseEventArgs e)
        {
            btnAgregar.BackColor = Color.DimGray;
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e)
        {
            btnAgregar.BackColor = Color.DodgerBlue;
        }

        private void btnModificarA_MouseMove(object sender, MouseEventArgs e)
        {
            btnModificarA.BackColor = Color.DimGray;
        }

        private void btnModificarA_MouseLeave(object sender, EventArgs e)
        {
            btnModificarA.BackColor = Color.DodgerBlue;
        }

        private void btnCancelar_MouseMove(object sender, MouseEventArgs e)
        {
            btnCancelar.BackColor = Color.DimGray;
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.DodgerBlue;
        }

      

        private void btnModificarA_Click_1(object sender, EventArgs e)
        {
            if ((tbxUsuario.Text == "" || tbxUsuario.Text == "0") || (tbxContraseña.Text == "" || tbxContraseña.Text == "0") || cbxPermisos.Text == "" || (tbxUsuario.TextLength >= 11))
            {
                MessageBox.Show("Hay campos sin completar y/o con valor cero ");
            }
            else
            {
                DialogResult preguntaModificar = MessageBox.Show("Desea modificar el usuario seleccionado?", "Registro Informa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (preguntaModificar == DialogResult.OK)
                {
                    string queryReiniciar = "UPDATE Usuarios SET Contraseña = @contraseña, Permisos = @permisos WHERE Usuario = @idReiniciar";

                    sqlComando = new OleDbCommand(queryReiniciar, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@contraseña", tbxContraseña.Text);
                    sqlComando.Parameters.AddWithValue("@permisos", cbxPermisos.Text);
                    sqlComando.Parameters.AddWithValue("@idReiniciar", idSelecionada);

                    try
                    {
                        conexionBaseDatos.Open();
                        if (sqlComando.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Se modifico el usuario correctamente.", "Registro Informa");
                        }
                        else
                        {
                            MessageBox.Show("Usuario no modificado. Se produzco un error.", "Registro Informa");
                        }
                        conexionBaseDatos.Close();
                        CargarDataGVBaseDatos();
                        DeshabilitarLimpiar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex.HResult));
                        conexionBaseDatos.Close();
                    }
                }
                else
                {
                    DeshabilitarLimpiar();
                }
            }
        }  
    }


    
}
