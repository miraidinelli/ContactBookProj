namespace AgendaProj
{
    public partial class frmAgenda : Form
    {
        private OperacaoEnum acao;
        public frmAgenda()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)ltbContatos.Items[ltbContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbNumeroTelefone.Text = contato.NumeroTelefone;
        }

        private void frmAgenda_Shown(object sender, EventArgs e)
        {
            AlterarExibicaoSalvarCancelar(false);
            AlterarExibicaoIncluirAlterarExcluir(true);
            ltbContatos.Items.AddRange(ManipuladordeArquivo.LerArquivo().ToArray());
            AlterarCampos(false);
        }

        private void AlterarExibicaoSalvarCancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            btnCancela.Enabled = estado;
        }

        private void AlterarExibicaoIncluirAlterarExcluir(bool estado)
        {
            btnAlterar.Enabled = estado;
            btnIncluir.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AlterarExibicaoIncluirAlterarExcluir(false);
            AlterarExibicaoSalvarCancelar(true);
            AlterarCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarExibicaoIncluirAlterarExcluir(false);
            AlterarExibicaoSalvarCancelar(true);
            AlterarCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            AlterarExibicaoIncluirAlterarExcluir(true);
            AlterarExibicaoSalvarCancelar(false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == OperacaoEnum.INCLUIR)
            {
                Contato contato = new Contato
                {
                    Nome = txbNome.Text,
                    Email = txbEmail.Text,
                    NumeroTelefone = txbNumeroTelefone.Text
                };
                List<Contato> contatosList = new List<Contato>();
                foreach (Contato contatodaLista in ltbContatos.Items)
                {
                    contatosList.Add(contatodaLista);
                }
                contatosList.Add(contato);
                ManipuladordeArquivo.EscreverArquivo(contatosList);
            }
            else 
            {
                Contato contato = new Contato
                {
                    Nome = txbNome.Text,
                    Email = txbEmail.Text,
                    NumeroTelefone = txbNumeroTelefone.Text
                };

                List<Contato> contatosList = new List<Contato>();
                foreach (Contato contatodaLista in ltbContatos.Items)
                {
                    contatosList.Add(contatodaLista);
                }

                int indice = ltbContatos.SelectedIndex;
                contatosList.RemoveAt(indice);
                contatosList.Insert(indice, contato);
                ManipuladordeArquivo.EscreverArquivo(contatosList);


            }
            CarregarListaContatos();
            AlterarExibicaoIncluirAlterarExcluir(true);
            AlterarExibicaoSalvarCancelar(false);
            LimparCampo();
            AlterarCampos(false);
        }
        
        private void CarregarListaContatos()
        {
            ltbContatos.Items.Clear();
            ltbContatos.Items.AddRange(ManipuladordeArquivo.LerArquivo().ToArray());
        }

        private void LimparCampo()
        {
            txbEmail.Text = "";
            txbNome.Text = "";
            txbNumeroTelefone.Text = "";
        }

        private void AlterarCampos(bool estado)
        {
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
            txbNumeroTelefone.Enabled = estado;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Tem Certeza", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes) 
            {
                int indiceExcluido = ltbContatos.SelectedIndex;
                ltbContatos.SelectedIndex = 0;
                ltbContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatosList = new List<Contato>();
                foreach (Contato contato in ltbContatos.Items)
                {
                    contatosList.Add(contato);
                }
                ManipuladordeArquivo.EscreverArquivo(contatosList);
                CarregarListaContatos();
                LimparCampo();

            }
        }
    }
}