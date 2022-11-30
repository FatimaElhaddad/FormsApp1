using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        static string chaine = @"Data Source=FATIMA\SQLEXPRESS;Initial Catalog=FORM;Integrated Security=True";
        static SqlConnection cnx = new SqlConnection(chaine);
        static SqlCommand cmd = new SqlCommand();
        static SqlDataAdapter adapter = new SqlDataAdapter(cmd);



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void afficher()
        {
            comboBox1.Items.Clear();
            cmd.Connection = cnx;
            cmd.CommandText = "select * from FORM";
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            cnx.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {

                comboBox1.Items.Add("Name : " + dr["name"]);
                comboBox1.Items.Add("Id   : " + dr["id"].ToString());

            }

        }

        private void Initialisation()
        {

            btnAjout.Enabled = true;
            btnSelect.Enabled = false;
            btnModifier.Enabled = false;
            btnSupp.Enabled = true;
            btnAnnuler.Enabled = true;
            comboBox.Enabled = true;
            ID.Enabled = false;
            Nom.Enabled = false;

        }
        private void ajout()
        {
            this.Text = "L'ajout";
            ID.Clear();
            Nom.Clear();
            afficher();
            btnAjout.Enabled = false;
            btnSelect.Enabled = true;
            btnModifier.Enabled = false;
            btnSupp.Enabled = true;
            btnAnnuler.Enabled = false;
            comboBox.Enabled = false;
            ID.Enabled = true;
            Nom.Enabled = true;
        }

        private void Selectionner()
        {
            if (comboBox.SelectedItem == null)
            {
                this.Text = "La case est deja vide";
            }
            else
            {
                this.Text = "Selectionner une case";
            }

            this.index = comboBox.SelectedIndex;

            btnAjout.Enabled = true;
            btnSelect.Enabled = false;
            btnModifier.Enabled = true;
            btnSupp.Enabled = true;
            btnAnnuler.Enabled = false;
            comboBox.Enabled = true;
            ID.Enabled = false;
            Nom.Enabled = false;


        }

        private void modifier()
        {

            this.Text = "Modifier";
            btnAjout.Enabled = false;
            btnSelect.Enabled = true;
            btnModifier.Enabled = false;
            btnSupp.Enabled = false;
            btnAnnuler.Enabled = true;
            comboBox.Enabled = true;
            ID.Enabled = true;
            Nom.Enabled = true;

        }
        private void Supprimer()
        {
            this.Text = "Supprimer";
            comboBox.Enabled = false;
            ID.Enabled = false;
            Nom.Enabled = false;
            btnAnnuler.Enabled = true;
            btnSelect.Enabled = true;
            btnSupp.Enabled = false;
            btnAjout.Enabled = false;
            btnModifier.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialisation();
        }


        private void btnAjout_Click(object sender, EventArgs e)
        {
            ajout();

        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            modifier();
            afficher();
        }

        private void btnSupp_Click(object sender, EventArgs e)
        {
            string msg = "confirmer la suppression";

            string title = "confiramtion";



            if (MessageBox.Show(msg, title, MessageBoxButtons.YesNo) == DialogResult.Yes)

            {

                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandText = "delete from id_livre where id='" + ID.Text + "' ";
                cmd.ExecuteNonQuery();
                cnx.Close();
                ID.Clear();
                Nom.Clear();

                Initialisation();
            }
            else
            {
                Selectionner();
            }

            afficher();
            Supprimer();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            cmd.Connection = cnx;
            cnx.Open();
            if (this.Text.Equals("L'ajout"))
            {
                cmd.CommandText = "INSERT INTO name_id (id ,nom ) values('" + ID.Text + "','" + Nom.Text + "')";
                cmd.ExecuteNonQuery();
            }
            else if (this.Text.Equals("Modifier"))
            {
                cmd.CommandText = "update Livre set name ='" + Nom.Text + "' where id='" + ID.Text + "' ";
                cmd.ExecuteNonQuery();
            }
            cnx.Close();
        }


        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            if (this.Text.Equals("L'ajout"))
            {
                Initialisation();
            }
            else if (this.Text.Equals("Modifier") || this.Text.Equals("Supprimer"))
            {
                Selectionner();

            }
        }
    }
}
