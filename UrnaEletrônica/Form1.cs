using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using System.Media;

namespace UrnaEletrônica
{
    public partial class UrnaEletrônica : Form

    {
        private bool votoEmBranco = false;
        private SoundPlayer player1;
        private SoundPlayer player2;
        private Dictionary<string, int> votosPrefeito = new Dictionary<string, int>();
        private Dictionary<string, int> votosVereador = new Dictionary<string, int>();
        private int votosNulosPrefeito = 0;
        private int votosNulosVereador = 0;
        private int votosBrancosPrefeito = 0;
        private int votosBrancosVereador = 0;

        Dictionary<string, (string nome, string partido, string cargo, string vice, Image foto, Image fotoVice)> candidatos = new Dictionary<string, (string nome, string partido, string cargo, string vice, Image foto, Image fotoVice)>()
{
            { "13", ("Saci Pererê", "Saltadores do Folclore", "Prefeito", "Curupira", Properties.Resources.Saci, Properties.Resources.Curupira) },
            { "22", ("Mula Sem Cabeça", "Sem Direção", "Prefeito", "Boitatá", Properties.Resources.Mula_sem_cabeca, Properties.Resources.Boitatata) },
            { "31", ("Lobisomem", "Lua Cheia", "Prefeito", "Boto Cor-de-Rosa", Properties.Resources.Lobisomen, Properties.Resources.Boto_cor_de_rosa) },
            { "44", ("Caipora", "Montaria", "Prefeito", "Negrinho do Pastoreio", Properties.Resources.Caipora, Properties.Resources.Negrinho) },
            { "13001", ("Matinta Pereira", "Saltadores do Folclore", "Vereador", "", Properties.Resources.Matinta_perera, Properties.Resources.imagem_branca) },
            { "13002", ("Boibumbá", "Saltadores do Folclore", "Vereador", "", Properties.Resources.Boi_bumba, Properties.Resources.imagem_branca) },
            { "22001", ("Bicho-Papão", "Sem Direção", "Vereador", "", Properties.Resources.Bicho_papao, Properties.Resources.imagem_branca) },
            { "22002", ("Comadre Fulozinha", "Sem Direção", "Vereador", "", Properties.Resources.Comadre_Fulozinha, Properties.Resources.imagem_branca) },
            { "31001", ("Cuca", "Lua Cheia", "Vereador", "", Properties.Resources.Cuca, Properties.Resources.imagem_branca) },
            { "31002", ("Iara-Seria", "Lua Cheia", "Vereador", "", Properties.Resources.Iara_sereia, Properties.Resources.imagem_branca) },
            { "44001", ("Mani-Mandioca", "Montaria", "Vereador", "", Properties.Resources.Mani_mandioca, Properties.Resources.imagem_branca) },
            { "44002", ("Vitoria-Regia", "Montaria", "Vereador", "", Properties.Resources.Ritoria_Regia, Properties.Resources.imagem_branca) },
        };
        public UrnaEletrônica()
        {
            InitializeComponent();
            timer1.Interval = 1000; // Intervalo de 1 segundo
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            painelCandidatos();
            painel_vereador.Visible = true;
            painel_prefeito.Visible = false;
            painel_fim.Visible = false;
            painel_resultado.Visible = false;
            player1 = new SoundPlayer(Properties.Resources.confima_1);
            player2 = new SoundPlayer(Properties.Resources.confirma_final);

        }
        private void PlayConfirma1()
        {
            // Acessa o recurso incorporado e reproduz o som
            SoundPlayer player = new SoundPlayer(Properties.Resources.confima_1);
            player.Play();
        }
        private void PlayConfirmaFinal()
        {
            // Acessa o recurso incorporado e reproduz o som
            SoundPlayer player = new SoundPlayer(Properties.Resources.confirma_final);
            player.Play();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Atualiza o Label com a data e hora atuais
            lbl_data.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializa a data e hora quando o formulário é carregado
            lbl_data.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        private void ContabilizarVoto(string cargo, string numeroCandidato)
        {
            if (cargo == "Prefeito")
            {
                if (numeroCandidato == "Branco")
                {
                    votosBrancosPrefeito++;
                }
                else if (candidatos.ContainsKey(numeroCandidato) && candidatos[numeroCandidato].cargo == "Prefeito")
                {
                    if (votosPrefeito.ContainsKey(numeroCandidato))
                    {
                        votosPrefeito[numeroCandidato]++;
                    }
                    else
                    {
                        votosPrefeito[numeroCandidato] = 1;
                    }
                }
                else
                {
                    votosNulosPrefeito++;
                }
            }
            else if (cargo == "Vereador")
            {
                if (numeroCandidato == "Branco")
                {
                    votosBrancosVereador++;
                }
                else if (candidatos.ContainsKey(numeroCandidato) && candidatos[numeroCandidato].cargo == "Vereador")
                {
                    if (votosVereador.ContainsKey(numeroCandidato))
                    {
                        votosVereador[numeroCandidato]++;
                    }
                    else
                    {
                        votosVereador[numeroCandidato] = 1;
                    }
                }
                else
                {
                    votosNulosVereador++;
                }
            }
        }

        private string votoVereador = "";
        private string votoPrefeito = "";
        private void panel_teclado_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btn_1_Click(object sender, EventArgs e) { InserirNumero("1"); }
        private void btn_2_Click(object sender, EventArgs e) { InserirNumero("2"); }
        private void btn_3_Click(object sender, EventArgs e) { InserirNumero("3"); }
        private void btn_4_Click(object sender, EventArgs e) { InserirNumero("4"); }
        private void btn_5_Click(object sender, EventArgs e) { InserirNumero("5"); }
        private void btn_6_Click(object sender, EventArgs e) { InserirNumero("6"); }
        private void btn_7_Click(object sender, EventArgs e) { InserirNumero("7"); }
        private void btn_8_Click(object sender, EventArgs e) { InserirNumero("8"); }
        private void btn_9_Click(object sender, EventArgs e) { InserirNumero("9"); }
        private void btn_0_Click(object sender, EventArgs e) { InserirNumero("0"); }
        private void InserirNumero(string numero)
        {
            if (!votoEmBranco)
            {
                if (painel_vereador.Visible)
                {
                    if (text_n1.Text == "") text_n1.Text = numero;
                    else if (text_n2.Text == "") text_n2.Text = numero;
                    else if (text_n3.Text == "") text_n3.Text = numero;
                    else if (text_n4.Text == "") text_n4.Text = numero;
                    else if (text_n5.Text == "") text_n5.Text = numero;

                    if (text_n5.Text != "")
                    {
                        string codigoVereador = text_n1.Text + text_n2.Text + text_n3.Text + text_n4.Text + text_n5.Text;
                        VerifCandVereador(codigoVereador, "Vereador");
                    }
                }

                else if (painel_prefeito.Visible)
                {
                    if (text_p_n1.Text == "") text_p_n1.Text = numero;
                    else if (text_p_n2.Text == "") text_p_n2.Text = numero;

                    if (text_p_n2.Text != "")
                    {
                        string codigoPrefeito = text_p_n1.Text + text_p_n2.Text;
                        VerifCandPrefeito(codigoPrefeito, "Prefeito");
                    }
                }
            }
        }
        private void VerifCandVereador(string codigo, string cargoEsperado)
        {
            if (candidatos.ContainsKey(codigo))
            {
                var candidato = candidatos[codigo];

                if (cargoEsperado == "Vereador" && candidato.cargo == "Vereador")
                {
                    text_nome.Text = candidato.nome;
                    text_partido.Text = candidato.partido;
                    //text_candidato.Text = candidato.cargo;

                    if (candidato.foto != null)
                    {
                        pic_vereador.Image = candidato.foto;
                    }
                    else
                    {
                        DefinirVotoNulo();
                    }
                }
            }
            else
            {
                DefinirVotoNulo();
            }
        }
        private void VerifCandPrefeito(string codigo, string cargoEsperado)
        {
            if (candidatos.ContainsKey(codigo))
            {
                var candidato = candidatos[codigo];

                if (cargoEsperado == "Prefeito" && candidato.cargo == "Prefeito")
                {
                    text_p_nome.Text = candidato.nome;
                    text_p_partido.Text = candidato.partido;
                    //text_p_candidato.Text = candidato.cargo;
                    text_pic_p_nome.Text = candidato.cargo;
                    pic_prefeito.Image = candidato.foto ?? Properties.Resources.imagem_branca;
                    text_v_nome.Text = candidato.vice;
                    text_pic_v_nome.Text = "Vice-Prefeito";
                    pic_vice.Image = candidato.fotoVice ?? Properties.Resources.imagem_branca;
                }
                else
                {
                    DefinirVotoNuloPrefeito();
                }
            }
            else
            {
                DefinirVotoNuloPrefeito();
            }
        }
        private void DefinirVotoNulo()
        {
            text_nome.Text = "VOTO NULO";
            text_partido.Text = "";
            //text_candidato.Text = "Vereador";
            pic_vereador.Image = Properties.Resources.imagem_branca;
        }
        private void DefinirVotoNuloPrefeito()
        {
            text_p_nome.Text = "VOTO NULO";
            text_p_partido.Text = "";
            //text_p_candidato.Text = "Prefeito";
            text_v_nome.Text = "";
            pic_prefeito.Image = Properties.Resources.imagem_branca;
            pic_vice.Image = Properties.Resources.imagem_branca;
        }
        private void btn_corrige_Click(object sender, EventArgs e)
        {
            if (painel_prefeito.Visible)
            {
                ResetarVoto();
                AparecerVotoPrefeito();
                votoEmBranco = false;
            }
            else if (painel_vereador.Visible)
            {
                ResetarVoto();
                AparecerVotoVereador();
                votoEmBranco = false;
            }
        }
        private void btn_branco_Click(object sender, EventArgs e)
        {
            if (painel_vereador.Visible)
            {
                ResetarVoto();
                text_nome.Text = "VOTO EM BRANCO";
                text_n1.Visible = false;
                text_n2.Visible = false;
                text_n3.Visible = false;
                text_n4.Visible = false;
                text_n5.Visible = false;
                lbl_2.Visible = false;
                lbl_3.Visible = false;
                lbl_4.Visible = false;
                votoEmBranco = true;
            }
            else if (painel_prefeito.Visible)
            {
                ResetarVoto();
                text_p_nome.Text = "VOTO EM BRANCO";
                text_p_n1.Clear();
                text_p_n2.Clear();
                text_p_partido.Clear();
                text_v_nome.Clear();
                pic_prefeito.Image = Properties.Resources.imagem_branca;
                pic_vice.Image = Properties.Resources.imagem_branca;
                text_p_n1.Visible = false;
                text_p_n2.Visible = false;
                lbl_p_2.Visible = false;
                lbl_p_3.Visible = false;
                lbl_p_4.Visible = false;
                lbl_p_5.Visible = false;
                votoEmBranco = true;
            }
        }
        private void AparecerVotoVereador()
        {
            text_n1.Visible = true;
            text_n2.Visible = true;
            text_n3.Visible = true;
            text_n4.Visible = true;
            text_n5.Visible = true;
            lbl_2.Visible = true;
            lbl_3.Visible = true;
            lbl_4.Visible = true;
            votoEmBranco = false;
        }
        private void AparecerVotoPrefeito()
        {
            text_p_n1.Visible = true;
            text_p_n2.Visible = true;
            lbl_p_2.Visible = true;
            lbl_p_3.Visible = true;
            lbl_p_4.Visible = true;
            lbl_p_5.Visible = true;
            text_pic_p_nome.Visible = true;
        }
        private void btn_confirma_Click(object sender, EventArgs e)
        {
            if (painel_vereador.Visible)
            {
                string numeroCandidato = text_n1.Text + text_n2.Text + text_n3.Text + text_n4.Text + text_n5.Text;
                ContabilizarVoto("Vereador", string.IsNullOrEmpty(numeroCandidato) ? "Branco" : numeroCandidato);

                if (!string.IsNullOrEmpty(text_nome.Text))
                {
                    PlayConfirma1();
                    Thread.Sleep(500);
                    votoVereador = text_n1.Text + text_n2.Text + text_n3.Text + text_n4.Text + text_n5.Text;
                    painel_vereador.Visible = false;
                    painel_prefeito.Visible = true;
                    text_nome.Clear();
                    //text_candidato.Clear();
                    text_partido.Clear();
                    pic_prefeito.Image = Properties.Resources.imagem_branca;
                    pic_vereador.Image = Properties.Resources.imagem_branca;
                    text_p_nome.Clear();
                    //text_p_candidato.Clear();
                    text_p_partido.Clear();
                    votoEmBranco = false;
                }
                else
                {
                    MessageBox.Show("Para CONFIRMAR é necessário digitar pelo meno o números do partido ou vatr em BRANCO");
                }
            }
            else if (painel_prefeito.Visible)
            {
                string numeroCandidato = text_p_n1.Text + text_p_n2.Text;
                ContabilizarVoto("Prefeito", string.IsNullOrEmpty(numeroCandidato) ? "Branco" : numeroCandidato);

                if (!string.IsNullOrEmpty(text_p_nome.Text))
                {
                    PlayConfirmaFinal();
                    Thread.Sleep(500);
                    votoPrefeito = text_p_n1.Text + text_p_n2.Text;
                    painel_prefeito.Visible = false;
                    painel_fim.Visible = true;
                    text_nome.Clear();
                    //text_candidato.Clear();
                    text_partido.Clear();
                    pic_prefeito.Image = Properties.Resources.imagem_branca;
                    pic_vereador.Image = Properties.Resources.imagem_branca;
                    text_p_nome.Clear();
                    //text_p_candidato.Clear();
                    text_p_partido.Clear();
                    votoEmBranco = false;
                }
                else
                {
                    MessageBox.Show("Para CONFIRMAR é necessário digitar pelo meno o números do partido ou vatr em BRANCO");
                }
            }
        }
        private void ResetarVoto()
        {
            text_n1.Clear();
            text_n2.Clear();
            text_n3.Clear();
            text_n4.Clear();
            text_n5.Clear();
            text_nome.Clear();
            text_partido.Clear();
            pic_vereador.Image = Properties.Resources.imagem_branca;
            text_p_n1.Clear();
            text_p_n2.Clear();
            text_p_nome.Clear();
            text_p_partido.Clear();
            text_v_nome.Clear();
            pic_prefeito.Image = Properties.Resources.imagem_branca;
            pic_vice.Image = Properties.Resources.imagem_branca;
            text_pic_p_nome.Clear();
            text_pic_v_nome.Clear();
            text_n1.Visible = true;
            text_n2.Visible = true;
            text_n3.Visible = true;
            text_n4.Visible = true;
            text_n5.Visible = true;
            text_p_n1.Visible = true;
            text_p_n2.Visible = true;
            lbl_2.Visible = true;
            lbl_3.Visible = true;
            lbl_4.Visible = true;
            lbl_p_2.Visible = true;
            lbl_p_3.Visible = true;
            lbl_p_4.Visible = true;
            lbl_p_5.Visible = true;
        }
        private void text_nome_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_n1_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_n2_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_n3_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_n4_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_n5_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_partido_TextChanged(object sender, EventArgs e)
        {

        }
        private void pic_vereador_Click(object sender, EventArgs e)
        {

        }
        private void text_cand_foto1_TextChanged(object sender, EventArgs e)
        {

        }
        private void pic_vice_Click(object sender, EventArgs e)
        {

        }
        private void text_cand_foto2_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel_vereador_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel_prefeito_Paint(object sender, PaintEventArgs e)
        {

        }
        private void text_p_candidato_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_p_n1_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_p_n2_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_p_nome_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_p_partido_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_v_nome_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_pic_p_nome_TextChanged(object sender, EventArgs e)
        {

        }
        private void text_pic_v_nome_TextChanged(object sender, EventArgs e)
        {

        }
        private void painel_voto_branco_Paint(object sender, PaintEventArgs e)
        {

        }
        private void text_b_candidato_TextChanged(object sender, EventArgs e)
        {

        }
        private void lbl_data_Click(object sender, EventArgs e)
        {

        }
        private void painelCandidatos()
        {
            painel_cand.Visible = true;
            painel_13.Visible = false;
            painel_22.Visible = false;
            painel_31.Visible = false;
            painel_44.Visible = false;
        }
        private void btn_13_Click(object sender, EventArgs e)
        {
            painel_cand.Visible = false;
            painel_13.Visible = true;
            painel_22.Visible = false;
        }
        private void btn_22_Click(object sender, EventArgs e)
        {
            painel_cand.Visible = false;
            painel_13.Visible = false;
            painel_22.Visible = true;
        }
        private void btn_volta13_Click_1(object sender, EventArgs e)
        {
            painelCandidatos();
        }
        private void btn_volta22_Click(object sender, EventArgs e)
        {
            painelCandidatos();
        }
        private void btn_31_Click(object sender, EventArgs e)
        {
            painel_31.Visible = true;
            painel_22.Visible = false;
            painel_13.Visible = false;
            painel_cand.Visible = false;

        }
        private void btn_volta31_Click(object sender, EventArgs e)
        {
            painelCandidatos();
        }
        private void btn_volta44_Click(object sender, EventArgs e)
        {
            painelCandidatos();
        }
        private void btn_44_Click(object sender, EventArgs e)
        {
            painel_44.Visible = true;
            painel_31.Visible = false;
            painel_22.Visible = false;
            painel_13.Visible = false;
            painel_cand.Visible = false;
        }
        private void btn_reiniciar_Click(object sender, EventArgs e)
        {
            ResetarVoto();
            painel_vereador.Visible = true;
            painel_prefeito.Visible = false;
            painel_fim.Visible = false;
            painel_resultado.Visible = false;
        }
        private void btn_exibirResultados_Click(object sender, EventArgs e)
        {
            // Configuração dos painéis
            painel_resultado.Visible = true;
            painel_vereador.Visible = false;
            painel_prefeito.Visible = false;
            painel_fim.Visible = false;

            // Limpa as listas antes de exibir os resultados
            listResultadosPrefeito.Items.Clear();
            listResultadosVereador.Items.Clear();
            listResultadosBranco.Items.Clear();
            listResultadosNulo.Items.Clear();
            listPrefeitoEleito.Items.Clear();
            listVereadorEleito.Items.Clear();

            // Variáveis para armazenar os mais votados
            string maisVotadoPrefeito = "Nenhum";
            int votosMaisVotadoPrefeito = 0;
            Image imagemPrefeito = null;
            string maisVotadoVice = "Nenhum";
            Image imagemVice = null;

            string maisVotadoVereador = "Nenhum";
            int votosMaisVotadoVereador = 0;
            Image imagemVerador = null;

            // Resultados para Prefeito
            listResultadosPrefeito.Items.Add("PREFEITO");
            foreach (var candidato in votosPrefeito)
            {
                var candidatoInfo = candidatos[candidato.Key];
                listResultadosPrefeito.Items.Add($"{candidatoInfo.nome} ({candidatoInfo.partido}): {candidato.Value} voto(s)");

                if (candidato.Value > votosMaisVotadoPrefeito)
                {
                    maisVotadoPrefeito = candidatoInfo.nome;
                    votosMaisVotadoPrefeito = candidato.Value;
                    imagemPrefeito = candidatoInfo.foto;
                    maisVotadoVice = candidatoInfo.vice;
                }
            }
            // Adiciona destaque para o mais votado
            listPrefeitoEleito.Items.Add($"{maisVotadoPrefeito}");
            listPrefeitoEleito.Items.Add($"{votosMaisVotadoPrefeito} votos");
            picPrefeitoEleito.Image = imagemPrefeito;
            listViceEleito.Items.Add($"{maisVotadoVice} - Vice");
            /*if (imagemPrefeito != null)
            {
                picPrefeitoEleito.Image = imagemPrefeito;
            }
            else
            {
                MessageBox.Show($"Imagem para o prefeito {maisVotadoPrefeito} não encontrada nos recursos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
            // Votos nulos e em branco para Prefeito
            listResultadosNulo.Items.Add($"Votos Nulos para Prefeito: {votosNulosPrefeito}");
            listResultadosNulo.Items.Add($"Votos em Branco para Prefeito: {votosBrancosPrefeito}");

            // Resultados para Vereador
            listResultadosVereador.Items.Add("VEREADOR");
            foreach (var candidato in votosVereador)
            {
                var candidatoInfo = candidatos[candidato.Key];
                listResultadosVereador.Items.Add($"{candidatoInfo.nome} ({candidatoInfo.partido}): {candidato.Value} voto(s)");

                if (candidato.Value > votosMaisVotadoVereador)
                {
                    maisVotadoVereador = candidatoInfo.nome;
                    votosMaisVotadoVereador = candidato.Value;
                    imagemVerador = candidatoInfo.foto;
                }
            }

            // Adiciona destaque para o mais votado
            listVereadorEleito.Items.Add($"{maisVotadoVereador}");
            listVereadorEleito.Items.Add($"{votosMaisVotadoVereador} votos");
            picVereadorEleito.Image = imagemVerador;

            //Define a imagem do vereado
            /*if (imagemVerador != null)
            {
                picVereadorEleito.Image = imagemVerador;
            }
            else
            {
                MessageBox.Show($"imagemVerador para vereador{maisVotadoVereador} não encontrado no recursos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            // Votos nulos e em branco para Vereador
            listResultadosBranco.Items.Add($"Votos Nulos para Vereador: {votosNulosVereador}");
            listResultadosBranco.Items.Add($"Votos em Branco para Vereador: {votosBrancosVereador}");
        }

        private void btn_voltarFim_Click(object sender, EventArgs e)
        {
            painel_resultado.Visible = false;
            painel_fim.Visible = true;
            listResultadosPrefeito.Items.Clear();
            listResultadosVereador.Items.Clear();
            listResultadosBranco.Items.Clear();
            listResultadosNulo.Items.Clear();
            listPrefeitoEleito.Items.Clear();
            listVereadorEleito.Items.Clear();
        }
    }
}
