using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string remover = "remover";
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Preencha os campos Nome e Email.");
                return;
            }

            dgvUsers.Rows.Add(nome, email,remover);
            txtName.Clear();
            txtEmail.Clear();

        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvUsers.Columns["ColunaRemover"].Index)
            {
                // Confirmação de remoção
                var confirmResult = MessageBox.Show("Deseja realmente remover este usuário?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Remove a linha selecionada
                    dgvUsers.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }
}
