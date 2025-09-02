namespace WinFormsApp1
{
    partial class TelaInicial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPageCadastro = new TabPage();
            dgvUsers = new DataGridView();
            btnRegister = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtConfirmPassword = new TextBox();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            txtName = new TextBox();
            txtUser = new TextBox();
            tabPage2 = new TabPage();
            ColunaNome = new DataGridViewTextBoxColumn();
            ColunaEmail = new DataGridViewTextBoxColumn();
            ColunaRemover = new DataGridViewButtonColumn();
            tabControl1.SuspendLayout();
            tabPageCadastro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageCadastro);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(1, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1342, 646);
            tabControl1.TabIndex = 0;
            // 
            // tabPageCadastro
            // 
            tabPageCadastro.Controls.Add(dgvUsers);
            tabPageCadastro.Controls.Add(btnRegister);
            tabPageCadastro.Controls.Add(label5);
            tabPageCadastro.Controls.Add(label4);
            tabPageCadastro.Controls.Add(label3);
            tabPageCadastro.Controls.Add(label2);
            tabPageCadastro.Controls.Add(label1);
            tabPageCadastro.Controls.Add(txtConfirmPassword);
            tabPageCadastro.Controls.Add(txtPassword);
            tabPageCadastro.Controls.Add(txtEmail);
            tabPageCadastro.Controls.Add(txtName);
            tabPageCadastro.Controls.Add(txtUser);
            tabPageCadastro.Location = new Point(4, 24);
            tabPageCadastro.Name = "tabPageCadastro";
            tabPageCadastro.Padding = new Padding(3);
            tabPageCadastro.Size = new Size(1334, 618);
            tabPageCadastro.TabIndex = 0;
            tabPageCadastro.Text = "Cadastro de Usuários";
            tabPageCadastro.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { ColunaNome, ColunaEmail, ColunaRemover });
            dgvUsers.Location = new Point(566, 16);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(741, 492);
            dgvUsers.TabIndex = 11;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(25, 364);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(300, 31);
            btnRegister.TabIndex = 10;
            btnRegister.Text = "Cadastrar";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 290);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 9;
            label5.Text = "Confirmar senha";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 226);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 8;
            label4.Text = "Senha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 159);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 7;
            label3.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 100);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 6;
            label2.Text = "Nome";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 39);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 5;
            label1.Text = "Nome de Usuário";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(25, 308);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(300, 23);
            txtConfirmPassword.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(25, 244);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 23);
            txtPassword.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(25, 177);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 23);
            txtEmail.TabIndex = 2;
            // 
            // txtName
            // 
            txtName.Location = new Point(25, 118);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 1;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(25, 57);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(300, 23);
            txtUser.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1334, 618);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Relatórios";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ColunaNome
            // 
            ColunaNome.HeaderText = "Nome";
            ColunaNome.Name = "ColunaNome";
            // 
            // ColunaEmail
            // 
            ColunaEmail.HeaderText = "Email";
            ColunaEmail.Name = "ColunaEmail";
            // 
            // ColunaRemover
            // 
            ColunaRemover.HeaderText = "";
            ColunaRemover.Name = "ColunaRemover";
            // 
            // TelaInicial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1355, 640);
            Controls.Add(tabControl1);
            Name = "TelaInicial";
            Text = "TelaInicial";
            tabControl1.ResumeLayout(false);
            tabPageCadastro.ResumeLayout(false);
            tabPageCadastro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageCadastro;
        private TabPage tabPage2;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtConfirmPassword;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private TextBox txtName;
        private TextBox txtUser;
        private Button btnRegister;
        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn ColunaNome;
        private DataGridViewTextBoxColumn ColunaEmail;
        private DataGridViewButtonColumn ColunaRemover;
    }
}