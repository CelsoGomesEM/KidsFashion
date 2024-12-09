using iTextSharp.text.pdf;
using iTextSharp.text;
using KidsFashion.Servicos.CadastrosBasicos;
using KidsFashion.Servicos.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.Relatorios.Estoques
{
    public class RelatorioEstoqueGenerator
    {
        public async Task<MemoryStream> GerarRelatorioPDF()
        {
            var servico = new ServicoEstoque();

            var estoques = servico.ObterTodosCompletoRastreamento();

            //Criar documento
            Document doc = UtilitariosPDF.CreateDoc();

            // Memória para armazenar o PDF
            MemoryStream memoryStream = new MemoryStream();

            // Criação do escritor PDF
            var writer = PdfWriter.GetInstance(doc, memoryStream);

            // Abertura do documento para edição
            doc.Open();

            var titulo = UtilitariosPDF.CriarTitulo("Relatório de Estoques");

            doc.Add(titulo);

            // Adicionar tabela ao documento
            PdfPTable tabela = CriarTabela();

            foreach (var item in estoques.Result)
            {
                // Adiciona o Nome (alinhado à esquerda)
                PdfPCell nomeCell = new PdfPCell(new Phrase(item.Produto.Nome));
                nomeCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                tabela.AddCell(nomeCell);

                // Adiciona o Quantidade (alinhado ao centro)
                PdfPCell cpfCell = new PdfPCell(new Phrase(item.Quantidade.ToString()));
                cpfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabela.AddCell(cpfCell);
            }

            // Certifique-se de que a tabela preenche o restante da página corretamente
            tabela.KeepTogether = true;

            doc.Add(tabela);

            doc.Add(new Paragraph(" ")); // Isso adicionará um espaço em branco entre as linhas

            doc.Close();

            return memoryStream;
        }

        private PdfPTable CriarTabela()
        {
            // Criando uma tabela com três colunas
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100; // Preenche a largura da página
            table.SpacingBefore = 10f;   // Espaço antes da tabela
            table.SpacingAfter = 10f;    // Espaço depois da tabela

            // Definindo largura para cada coluna (Nome maior)
            float[] columnWidths = { 80f, 20f };
            table.SetWidths(columnWidths);

            // Fonte em negrito para o cabeçalho
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

            // Cabeçalho: Nome
            PdfPCell cell0 = new PdfPCell(new Phrase("PRODUTO", boldFont));
            cell0.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell0);

            // Cabeçalho: CPF
            PdfPCell cell1 = new PdfPCell(new Phrase("QUANTIDADE", boldFont));
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            table.AddCell(cell1);

            return table;
        }
    }
}
